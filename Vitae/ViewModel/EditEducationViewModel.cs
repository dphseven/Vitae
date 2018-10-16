namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditEducationViewModel : ViewModelBase, IEditEducationViewModel
    {
        private IEducationRepository repos;

        private UIState formState = UIState.View;
        private Guid id;
        private IEducationEntity selectedInstitution;
        private string selectedCredential;
        private string updatedCredential;

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
                if (SelectedInstitution != null) return SelectedInstitution.ID;
                else return Guid.Empty;
            }
            set
            {
                id = value;
                notifyPropertyChanged();
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
                notifyPropertyChanged();
                notifyPropertyChanged(nameof(Credentials));
            }
        }
        public ObservableCollection<string> Credentials { get; set; }
        public string SelectedCredential 
        {
            get { return selectedCredential; }
            set
            {
                selectedCredential = value;
                FormState = UIState.Edit;
                UpdatedCredential = value;
                notifyPropertyChanged();
            }
        }
        public string UpdatedCredential 
        {
            get { return updatedCredential; }
            set
            {
                updatedCredential = value;
                notifyPropertyChanged();
            }
        }

        public ICommand EditCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public EditEducationViewModel(IEducationRepository repository) 
        {
            repos = repository;
            LoadEducationItems();
            SetUpRelayCommands();
        }

        private void LoadEducationItems() 
        {
            Institutions = new ObservableCollection<IEducationEntity>(repos.GetAll());
        }

        private void EditEducation() 
        {
            SelectedInstitution.Credential = UpdatedCredential;
            repos.Update(SelectedInstitution.ID, SelectedInstitution);
        }

        private void Reset() 
        {
            repos = null;
            id = Guid.Empty;
            selectedInstitution = null;
            selectedCredential = null;
            updatedCredential = null;
        }

        private void SetUpRelayCommands() 
        {
            EditCmd = new RelayCommand(
                T => { EditEducation(); Reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => { Reset(); },
                T => true);
        }
    }
}