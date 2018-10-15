namespace Vitae.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeleteExpertiseViewModel : IDeleteExpertiseViewModel
    {
        private IExpertiseRepository repos;

        private ObservableCollection<IExpertiseEntity> allExpertises;
        private string selectedCategory;

        public Guid ExpertiseID 
        {
            get
            {
                if (SelectedExpertise != null) return SelectedExpertise.ID;
                else return Guid.Empty;
            }
        }
        public ObservableCollection<string> Categories 
        {
            get
            {
                if (allExpertises != null) return new ObservableCollection<string>
                    (allExpertises.Select(T => T.Category).Distinct().ToList());
                else return new ObservableCollection<string>();
            }
        }
        public string SelectedCategory 
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                notifyPropertyChanged();
                notifyPropertyChanged(nameof(Expertises));
            }
        }
        public ObservableCollection<IExpertiseEntity> Expertises 
        {
            get
            {
                if (allExpertises != null)
                    return new ObservableCollection<IExpertiseEntity>(
                    allExpertises.Where(T => T.Category == SelectedCategory));
                else return new ObservableCollection<IExpertiseEntity>();
            }
        }
        public IExpertiseEntity SelectedExpertise { get; set; }

        public ICommand DeleteCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged; 

        // Public Methods

        public DeleteExpertiseViewModel(IExpertiseRepository repository) 
        {
            repos = repository;
            setUpRelayCommands();
            loadAllExpertises();
        }

        // Private Methods

        private void deleteExpertise() 
        {
            repos.Remove(SelectedExpertise.ID);
        }

        private void loadAllExpertises() 
        {
            allExpertises = new ObservableCollection<IExpertiseEntity>(repos.GetAll());
            notifyPropertyChanged(nameof(ExpertiseID));
            notifyPropertyChanged(nameof(Categories));
            notifyPropertyChanged(nameof(Expertises));
        }

        private void reset() 
        {

        }

        private void setUpRelayCommands() 
        {
            DeleteCmd = new RelayCommand(
                T => { deleteExpertise(); reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => { reset(); },
                T => true);

        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}