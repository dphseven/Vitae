namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddJobTitleViewModel : IAddJobTitleViewModel
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
                notifyPropertyChanged();
            }
        }
        public ObservableCollection<IExperienceEntity> Employers { get; set; }
        public IExperienceEntity SelectedEmployer 
        {
            get { return selectedEmployer; }
            set
            {
                selectedEmployer = value;
                notifyPropertyChanged();
            }
        }
        public string JobTitle 
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddJobTitleCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
