﻿namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeleteJobTitleViewModel : ViewModelBase, IDeleteJobTitleViewModel
    {
        private IExperienceRepository repos;

        private IExperienceEntity selectedEmployer;
        private string selectedJobTitle;

        public Guid EmployerID 
        {
            get
            {
                if (selectedEmployer != null) return selectedEmployer.ID;
                else return Guid.Empty;
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
                NotifyPropertyChanged(nameof(EmployerID));
            }
        }
        public string SelectedJobTitle 
        {
            get { return selectedJobTitle; }
            set
            {
                selectedJobTitle = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand DeleteJobTitleCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler JobTitleDeleted;

        // Public Methods

        public DeleteJobTitleViewModel(IExperienceRepository repository) 
        {
            repos = repository;

            loadEmployers();
            
            setUpRelayCommands();
        }

        // Private Methods

        private void deleteJobTitle() 
        {
            SelectedEmployer.Titles.Remove(SelectedJobTitle);
            repos.Update(SelectedEmployer.ID, SelectedEmployer);

            JobTitleDeleted?.Invoke(this, new EventArgs());
        }

        private void loadEmployers() 
        {
            Employers = new ObservableCollection<IExperienceEntity>(repos.GetAll());
        }

        private void resetForm() 
        {
            repos = null;
            Employers = null;
            selectedEmployer = null;
            selectedJobTitle = null;
        }

        private void setUpRelayCommands() 
        {
            DeleteJobTitleCmd = new RelayCommand(
                T => deleteJobTitle(),
                T => true);
            CancelCmd = new RelayCommand(
                T => { },
                T => true);
        }

    }
}