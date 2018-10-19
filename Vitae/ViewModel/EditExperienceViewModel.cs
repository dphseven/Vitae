namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditExperienceViewModel : ViewModelBase, IEditExperienceViewModel
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
                NotifyPropertyChanged();
            }
        }
        public IExperienceEntity SelectedEmployer 
        {
            get { return selectedEmployer; }
            set
            {
                selectedEmployer = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ExperienceItems));
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
                NotifyPropertyChanged();
            }
        }
        public string UpdatedExperienceItem 
        {
            get { return updatedExperienceItem; }
            set
            {
                updatedExperienceItem = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand EditCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler ExperienceEdited;

        // Public Methods

        public EditExperienceViewModel(IExperienceRepository repository) 
        {
            repos = repository;
            SetUpRelayCommands();
            LoadExperiences();
        }

        // Private Methods

        private void EditExperience() 
        {
            var index = SelectedEmployer.Details.IndexOf(initialExperienceItem);
            if (index >= 0) SelectedEmployer.Details[index] = updatedExperienceItem;
            repos.Update(SelectedEmployer.ID, SelectedEmployer);

            ExperienceEdited?.Invoke(this, new EventArgs());
        }

        private void LoadExperiences() 
        {
            Employers = new ObservableCollection<IExperienceEntity>(repos.GetAll());
        }

        private void Reset() 
        {
            repos = null;
            formState = UIState.View;
            selectedEmployer = null;
            initialExperienceItem = null;
            updatedExperienceItem = null;
        }

        private void SetUpRelayCommands() 
        {
            EditCmd = new RelayCommand(
                T => { EditExperience(); Reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => Reset(),
                T => true);
        }

    }
}