namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ExpertiseItems));
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
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ID));
            }
        }

        public ICommand EditButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        public event EventHandler ExpertiseEdited;

        // Public Methods

        public EditExpertiseViewModel(IExpertiseRepository repository, IKernel kernel) 
        {
            repos = repository;

            entity = kernel.Get<IExpertiseEntity>();

            loadExpertiseItems();
            setUpRelayCommands();

            FormState = UIState.View;
        }

        // Private Methods

        private void loadExpertiseItems() 
        {
            allExpertiseEntities = repos.GetAll().ToList();
            NotifyPropertyChanged(nameof(Categories));
        }

        private void updateExpertise() 
        {
            repos.Update(entity.ID, entity);
            ExpertiseEdited?.Invoke(this, new EventArgs());
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