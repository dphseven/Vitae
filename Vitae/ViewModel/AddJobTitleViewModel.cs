namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddJobTitleViewModel : ViewModelBase, IAddJobTitleViewModel
    {
        IExperienceRepository repos;

        private UIState formState;
        private IExperienceEntity selectedEmployer;
        private string jobTitle;

        public UIState FormState 
        {
            get { return formState; }
            set
            {
                formState = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<IExperienceEntity> Employers { get; set; }
        public IExperienceEntity SelectedEmployer 
        {
            get { return selectedEmployer; }
            set
            {
                selectedEmployer = value;
                NotifyPropertyChanged();
            }
        }
        public string JobTitle 
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand AddJobTitleCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler JobTitleAdded;

        // Public Methods

        public AddJobTitleViewModel(IExperienceRepository repository) 
        {
            repos = repository;

            Employers = new ObservableCollection<IExperienceEntity>();
            loadEmployers();

            setUpRelayCommands();
        }

        // Private Methods

        private void addJobTitle() 
        {
            if (SelectedEmployer.ID == null ||
                SelectedEmployer.ID == Guid.Empty ||
                string.IsNullOrWhiteSpace(JobTitle)) return;

            SelectedEmployer.Titles.Add(JobTitle);
            repos.Update(SelectedEmployer.ID, SelectedEmployer);

            JobTitleAdded?.Invoke(this, new EventArgs());
        }

        private void loadEmployers() 
        {
            Employers = new ObservableCollection<IExperienceEntity>(repos.GetAll());
        }

        private void setUpRelayCommands() 
        {
            AddJobTitleCmd = new RelayCommand(
                T => addJobTitle(),
                T => true);
            CancelCmd = new RelayCommand(
                T => { },
                T => true);
        }

    }
}
