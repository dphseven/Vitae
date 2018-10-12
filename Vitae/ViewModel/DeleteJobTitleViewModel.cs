﻿namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeleteJobTitleViewModel : IDeleteJobTitleViewModel
    {
        private IExperienceRepository repos;

        private UIState formState;
        private Guid employerID;
        private IExperienceEntity selectedEmployer;
        private string selectedJobTitle;

        public UIState FormState 
        {
            get { return formState; }
            set
            {
                formState = value;
                notifyPropertyChanged();
            }
        }
        public Guid EmployerID 
        {
            get { return employerID; }
            set
            {
                employerID = value;
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
        public string SelectedJobTitle 
        {
            get { return selectedJobTitle; }
            set
            {
                selectedJobTitle = value;
                notifyPropertyChanged();
            }
        }

        public ICommand DeleteJobTitleCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}