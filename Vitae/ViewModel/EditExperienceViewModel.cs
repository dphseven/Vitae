namespace Vitae.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditExperienceViewModel : IEditExperienceViewModel
    {
        private IExperienceRepository repos;

        private UIState formState = UIState.View;
        private IExperienceEntity selectedEmployer;
        private string initialExperienceItem;
        private string updatedExperienceItem;        

        public UIState FormState 
        {
            get { return formState; }
            set
            {
                formState = value;
                notifyPropertyChanged();
            }
        }
        public IExperienceEntity SelectedEmployer 
        {
            get { return selectedEmployer; }
            set
            {
                selectedEmployer = value;
                notifyPropertyChanged();
                notifyPropertyChanged(nameof(ExperienceItems));
            }
        }
        public ObservableCollection<IExperienceEntity> Employers { get; set; }
        public ObservableCollection<string> ExperienceItems 
        {
            get
            {
                if (SelectedEmployer != null)
                {
                    return new ObservableCollection<string>(SelectedEmployer.Details);
                }
                else return new ObservableCollection<string>();
            }
        }
        public string InitialExperienceItem 
        {
            get { return initialExperienceItem; }
            set
            {
                initialExperienceItem = value;
                UpdatedExperienceItem = value;
                FormState = UIState.Edit;
                notifyPropertyChanged();
            }
        }
        public string UpdatedExperienceItem 
        {
            get { return updatedExperienceItem; }
            set
            {
                updatedExperienceItem = value;
                notifyPropertyChanged();
            }
        }

        public ICommand EditCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Public Methods

        public EditExperienceViewModel(IExperienceRepository repository) 
        {
            repos = repository;
            setUpRelayCommands();
            loadExperiences();
        }

        // Private Methods

        private void editExperience() 
        {
            var index = SelectedEmployer.Details.IndexOf(initialExperienceItem);
            if (index >= 0) SelectedEmployer.Details[index] = updatedExperienceItem;
            repos.Update(SelectedEmployer.ID, SelectedEmployer);
        }

        private void loadExperiences() 
        {
            Employers = new ObservableCollection<IExperienceEntity>(repos.GetAll());
        }

        private void reset() 
        {
            repos = null;
            formState = UIState.View;
            selectedEmployer = null;
            initialExperienceItem = null;
            updatedExperienceItem = null;
        }

        private void setUpRelayCommands() 
        {
            EditCmd = new RelayCommand(
                T => { editExperience(); reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => reset(),
                T => true);
        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}