namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditPublicationViewModel : ViewModelBase, IEditPublicationViewModel
    {
        private IPublicationsRepository repos;

        private UIState formState = UIState.View;
        private IPublicationEntity selectedPublication;
        private string updatedPublication;

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
                if (SelectedPublication != null) return SelectedPublication.ID;
                else return Guid.Empty;
            }
        }
        public ObservableCollection<IPublicationEntity> Publications { get; set; }
        public IPublicationEntity SelectedPublication 
        {
            get
            {
                return selectedPublication;
            }
            set
            {
                selectedPublication = value;
                UpdatedPublication = SelectedPublication.Publication;
                FormState = UIState.Edit;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ID));
            }
        }
        public string UpdatedPublication 
        {
            get { return updatedPublication; }
            set
            {
                updatedPublication = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand EditCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler PublicationEdited;

        public EditPublicationViewModel(IPublicationsRepository repository) 
        {
            repos = repository;
            LoadPublications();
            SetUpRelayCommands();
        }

        private void LoadPublications() 
        {
            Publications = new ObservableCollection<IPublicationEntity>(repos.GetAll());
        }

        private void EditPublication() 
        {
            SelectedPublication.Publication = UpdatedPublication;
            repos.Update(SelectedPublication.ID, SelectedPublication);
            PublicationEdited?.Invoke(this, new EventArgs());
        }

        private void Reset() 
        {
            repos = null;
            selectedPublication = null;
            updatedPublication = null;
        }

        private void SetUpRelayCommands() 
        {
            EditCmd = new RelayCommand(
                T => { EditPublication(); Reset(); },
                T => !string.IsNullOrWhiteSpace(UpdatedPublication));
            CancelCmd = new RelayCommand(
                T => { Reset(); },
                T => true);
        }

    }
}