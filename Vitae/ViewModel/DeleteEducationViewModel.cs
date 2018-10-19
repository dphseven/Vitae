namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeleteEducationViewModel : ViewModelBase, IDeleteEducationViewModel
    {
        private IEducationRepository repos;

        private IEducationEntity selectedInstitution;
        private string selectedCredential;

        public Guid ID 
        {
            get
            {
                if (SelectedInstitution != null) return SelectedInstitution.ID;
                else return Guid.Empty;
            }
        }
        public ObservableCollection<IEducationEntity> Institutions { get; set; }
        public IEducationEntity SelectedInstitution 
        {
            get { return selectedInstitution; }
            set
            {
                selectedInstitution = value;
                Credentials = new ObservableCollection<string>(Institutions.Where(T => T == value).Select(T => T.Credential));
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Credentials));
            }
        }
        public ObservableCollection<string> Credentials { get; set; }
        public string SelectedCredential 
        {
            get { return selectedCredential; }
            set
            {
                selectedCredential = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ID));
            } 
        } 

        public ICommand DeleteCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler EducationDeleted;

        public DeleteEducationViewModel(IEducationRepository repository) 
        {
            repos = repository;
            LoadEducationItems();
            SetUpRelayCommands();
        }

        private void LoadEducationItems() 
        {
            Institutions = new ObservableCollection<IEducationEntity>(repos.GetAll());
        }

        private void DeleteEducation() 
        {
            repos.Remove(ID);
            EducationDeleted?.Invoke(this, new EventArgs());
        }

        private void Reset() 
        {
            repos = null;
            selectedInstitution = null;
            selectedCredential = null;
        }

        private void SetUpRelayCommands() 
        {
            DeleteCmd = new RelayCommand(
                T => { DeleteEducation(); Reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => { Reset(); },
                T => true);
        }

    }
}