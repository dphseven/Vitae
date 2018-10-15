﻿namespace Vitae.ViewModel
{
    using Ninject;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddExperienceViewModel : IAddExperienceViewModel
    {
        private IExperienceRepository repos;

        private string employer;
        private string experience;

        public string Employer 
        {
            get { return employer; }
            set
            {
                employer = value;
                notifyPropertyChanged();
            }
        }
        public string Experience 
        {
            get { return experience; }
            set
            {
                experience = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Public Methods

        public AddExperienceViewModel(IExperienceRepository repository) 
        {
            repos = repository;
            setUpRelayCommands();
        }

        // Private Methods

        private void addExperience() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var job = repos.GetAll().FirstOrDefault(T => T.Employer == Employer);
                if (job != null)
                {
                    job.Details.Add(Experience);
                    repos.Update(job.ID, job);
                }
                else
                {
                    job = ioc.Get<IExperienceEntity>();
                    job.Details.Add(Experience);
                    repos.Add(job);
                }
            }
        }

        private void reset() 
        {
            repos = null;
            employer = null;
            experience = null;
        }

        private void setUpRelayCommands() 
        {
            AddCmd = new RelayCommand(
                T => { addExperience(); reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => { reset(); },
                T => true);
        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}