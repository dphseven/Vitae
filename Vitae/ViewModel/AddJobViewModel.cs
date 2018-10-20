namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddJobViewModel : ViewModelBase, IAddJobViewModel
    {
        private readonly IKernel _kernel;
        private IExperienceRepository repos;

        private string employer = "";
        private string jobTitle = "";
        private string startDate = "";
        private string endDate = "";

        public string Employer 
        {
            get { return employer; }
            set
            {
                employer = value;
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
        public string StartDate 
        {
            get { return startDate; }
            set
            {
                startDate = value;
                NotifyPropertyChanged();
            }
        }
        public string EndDate 
        {
            get { return endDate; }
            set
            {
                endDate = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand AddCmd { get; set; }

        public event EventHandler JobAdded;

        public AddJobViewModel(IExperienceRepository repository, IKernel kernel) 
        {
            _kernel = kernel;
            repos = repository;
            SetUpRelayCommands();
        }

        private void AddJob() 
        {
            var ent = _kernel.Get<IExperienceEntity>();
            ent.Employer = Employer;
            ent.Titles.Add(JobTitle);
            ent.StartDate = StartDate;
            ent.EndDate = EndDate;

            repos.Add(ent);

            JobAdded?.Invoke(this, new EventArgs());
        }

        private void SetUpRelayCommands() 
        {
            AddCmd = new RelayCommand(
                T => AddJob(),
                T => true );
        }
    }
}