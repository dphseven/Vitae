namespace Vitae.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddExpertiseViewModel : IAddExpertiseViewModel
    {
        private IExpertiseRepository repos;

        private UIState formState;
        private IExpertiseEntity entity;

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
            get { return entity.ID; }
            set
            {
                entity.ID = value;
                notifyPropertyChanged();
            }
        }
        public string Category 
        {
            get { return entity.Category; }
            set
            {
                entity.Category = value;
                notifyPropertyChanged();
            }
        }
        public string Expertise 
        {
            get { return entity.Expertise; }
            set
            {
                entity.Expertise = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

		// Public Methods

        public AddExpertiseViewModel(IExpertiseRepository repository) 
        {
            repos = repository;
        }

		// Private Methods

        private void addExpertiseToRepository() 
        {
            repos.Add(entity);
        }

        private void reset() 
        {
            repos = null;
			id = Guid.Empty;
			category = null;
			expertise = null;
		}

        private void setUpRelayCommands() 
        {
            AddButtonCmd = new RelayCommand(
                T => { addExpertiseToRepository(); reset(); },
                T => true);
            CancelButtonCmd = new RelayCommand(
                T => reset(),
                T => true);
        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}