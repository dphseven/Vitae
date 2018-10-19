namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeletePublicationViewModel : ViewModelBase, IDeletePublicationViewModel
    {
        private IPublicationsRepository repos;

        private IPublicationEntity selectedPublication;

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
            get { return selectedPublication; }
            set
            {
                selectedPublication = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ID));
            }
        }

        public ICommand DeleteCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler PublicationDeleted;

        public DeletePublicationViewModel(IPublicationsRepository repository) 
        {
            repos = repository;
            LoadPublications();
            SetUpRelayCommands();
        }

        private void LoadPublications() 
        {
            Publications = new ObservableCollection<IPublicationEntity>(repos.GetAll());
        }

        private void DeletePublication() 
        {
            repos.Remove(SelectedPublication.ID);
            PublicationDeleted?.Invoke(this, new EventArgs());
        }

        private void Reset() 
        {
            repos = null;
            selectedPublication = null;
        }

        private void SetUpRelayCommands() 
        {
            DeleteCmd = new RelayCommand(
                T => { DeletePublication(); Reset(); },
                T => SelectedPublication != null );
            CancelCmd = new RelayCommand(
                T => { Reset(); },
                T => true);
        }
    }
}