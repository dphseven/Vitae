namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class ExpertiseManagementViewModel : IExpertiseManagementViewModel, INotifyPropertyChanged
    {
        private IExpertiseRepository repos;
        private IExpertiseEntity ee;

        public Guid ID 
        {
            get { return ee.ID; }
            set
            {
                ee.ID = value;
                notifyPropertyChanged();
            }
        }
        public string Category 
        {
            get { return ee.Category; }
            set
            {
                ee.Category = value;
                notifyPropertyChanged();
            }
        }
        public string Expertise 
        {
            get { return ee.Expertise; }
            set
            {
                ee.Expertise = value;
                notifyPropertyChanged();
            }
        }

        public UIState FormState { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddButtonCmd { get; set; }
		public ICommand EditButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        public ExpertiseManagementViewModel(IExpertiseRepository repository, IExpertiseEntity entity = null) 
        {
            repos = repository;
            if (entity != null && entity.ID != Guid.Empty)
            {
                ee = entity;
                FormState = UIState.Edit;
            }
            else FormState = UIState.View;

            setUpRelayCommands();
        }

		[Inject]
		public void InjectExpertiseEntity(IExpertiseEntity entity) 
        {
            ee = entity;

            if (ee != null && ee.ID != Guid.Empty) FormState = UIState.Edit;
            else FormState = UIState.View;

            notifyPropertyChanged(nameof(ID));
            notifyPropertyChanged(nameof(Category));
            notifyPropertyChanged(nameof(Expertise));
        }

        private void addExpertiseToRepository() 
        {
			ID = repos.Add(ee);
            reset();
        }
        private void editExpertiseInRepository() 
        {
            repos.Update(ee.ID, ee);
            reset();
        }
        private void reset() 
        {
            ID = Guid.Empty;
            Category = string.Empty;
            Expertise = string.Empty;
        }
		
        private void setUpRelayCommands() 
        {
            AddButtonCmd = new RelayCommand(
                T => addExpertiseToRepository(),
                T => true);
			EditButtonCmd = new RelayCommand(
                T => editExpertiseInRepository(),
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