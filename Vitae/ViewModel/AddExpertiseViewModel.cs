namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddExpertiseViewModel : ViewModelBase, IAddExpertiseViewModel
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
        public string Category 
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
            }
        }
        public string Expertise 
        {
            get
            {
                if (entity != null) return entity.Expertise;
                else return string.Empty;
            }
            set
            {
                entity.Expertise = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        public event EventHandler ExpertiseAdded;

		// Public Methods

        public AddExpertiseViewModel(IExpertiseRepository repository) 
        {
            repos = repository;
            using (var ioc = new VitaeNinjectKernel())
            {
                entity = ioc.Get<IExpertiseEntity>();
            }
            setUpRelayCommands();
        }

		// Private Methods

        private void addExpertiseToRepository() 
        {
            repos.Add(entity);

            ExpertiseAdded?.Invoke(this, new EventArgs());
        }

        private void reset() 
        {
            repos = null;
            entity = null;
		}

        private void setUpRelayCommands() 
        {
            AddButtonCmd = new RelayCommand(
                T => { addExpertiseToRepository(); reset(); },
                T => true);
            CancelButtonCmd = new RelayCommand(
                T => { reset(); },
                T => true);
        }

    }
}