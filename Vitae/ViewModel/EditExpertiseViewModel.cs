namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditExpertiseViewModel : ViewModelBase, IEditExpertiseViewModel
    {
        private IExpertiseRepository repos;

        private UIState formState;
        private IExpertiseEntity entity;
        private List<IExpertiseEntity> allExpertiseEntities;

        public UIState FormState 
        {
            get { return formState; }
            set
            {
                formState = value;
                notifyPropertyChanged();
            }
        }
        public Guid ID 
        {
            get
            {
                if (entity != null) return entity.ID;
                else return Guid.Empty;
            }
            set
            {
                entity.ID = value;
                notifyPropertyChanged();
            }
        }
        public ObservableCollection<string> Categories 
        {
            get { return new ObservableCollection<string>(allExpertiseEntities
                                                          .Select(T => T.Category)
                                                          .Distinct()); }
        }
        public string SelectedCategory 
        {
            get
            {
                if (entity != null) return entity.Category;
                else return string.Empty;
            }
            set
            {
                entity.Category = value;
                notifyPropertyChanged();
                notifyPropertyChanged(nameof(ExpertiseItems));
            }
        }
        public ObservableCollection<string> ExpertiseItems 
        {
            get {return new ObservableCollection<string>(allExpertiseEntities
                                                         .Where(T => T.Category == SelectedCategory)
                                                         .Select(T => T.Expertise)); }
        }
        public string SelectedExpertiseItem 
        {
            get
            {
                if (entity != null) return entity.Expertise;
                else return string.Empty;
            }
            set
            {
                if (FormState == UIState.View)
                {
                    entity = allExpertiseEntities.First(T => T.Category == SelectedCategory && T.Expertise == value);
                    FormState = UIState.Edit;
                }
                else entity.Expertise = value;
                notifyPropertyChanged();
                notifyPropertyChanged(nameof(ID));
            }
        }

        public ICommand EditButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        // Public Methods

        public EditExpertiseViewModel(IExpertiseRepository repository) 
        {
            repos = repository;
            using (var ioc = new VitaeNinjectKernel())
            {
                entity = ioc.Get<IExpertiseEntity>();
            }

            loadExpertiseItems();
            setUpRelayCommands();

            FormState = UIState.View;
        }

        // Private Methods

        private void loadExpertiseItems() 
        {
            allExpertiseEntities = repos.GetAll().ToList();
            notifyPropertyChanged(nameof(Categories));
        }

        private void updateExpertise() 
        {
            repos.Update(entity.ID, entity);
        }

        private void reset() 
        {
            repos = null;
            entity = null;
            allExpertiseEntities = null;
        }

        private void setUpRelayCommands() 
        {
            EditButtonCmd = new RelayCommand(
                T => { updateExpertise(); reset(); },
                T => true);
            CancelButtonCmd = new RelayCommand(
                T => { reset(); },
                T => true);
        }

    }
}