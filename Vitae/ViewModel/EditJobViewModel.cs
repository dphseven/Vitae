namespace Vitae.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditJobViewModel : ViewModelBase, IEditJobViewModel
    {
        private readonly IExperienceRepository repos;

        private Guid id;
        private string employer;
        private string jobTitles;
        private string startDate;
        private string endDate;

        public Guid ID 
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
        public string Employer 
        {
            get { return employer; }
            set
            {
                employer = value;
                NotifyPropertyChanged();
            }
        }
        public string JobTitles 
        {
            get { return jobTitles; }
            set
            {
                jobTitles = value;
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

        public ICommand EditCmd { get; set; }

        public event EventHandler JobEdited;

        public EditJobViewModel(IExperienceRepository repository, Guid jobId) 
        {
            repos = repository;
            
            SetUpRelayCommands();

            LoadJob(jobId);
        }

        private void EditJob() 
        {
            var job = repos.Get(ID);

            job.Employer = Employer;

            var listOfTitles = new List<string>(JobTitles.Split(';'));
            job.Titles.Clear();
            foreach (var item in listOfTitles)
                if (!string.IsNullOrWhiteSpace(item.Trim()))
                    job.Titles.Add(item.Trim());

            job.StartDate = StartDate;
            job.EndDate = EndDate;

            repos.Update(job.ID, job);

            JobEdited?.Invoke(this, new EventArgs());
        }

        private void LoadJob(Guid jobId) 
        {
            var ent = repos.Get(jobId);
            ID = ent.ID;
            Employer = ent.Employer;

            foreach (var title in ent.Titles)
            {
                JobTitles += title + "; ";
            }
            JobTitles = JobTitles.Remove(JobTitles.Length - 2);

            StartDate = ent.StartDate;
            EndDate = ent.EndDate;
        }

        private void SetUpRelayCommands() 
        {
            EditCmd = new RelayCommand(
                T => EditJob(),
                T => true);
        }

    }
}