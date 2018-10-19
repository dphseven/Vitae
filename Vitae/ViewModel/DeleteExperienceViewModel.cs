namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class DeleteExperienceViewModel : ViewModelBase, IDeleteExperienceViewModel
    {
        private IExperienceRepository repos;

        private IExperienceEntity selectedEmployer;
        private string selectedExperience;

        public Guid EmployerID 
        {
            get
            {
                if (SelectedEmployer != null) return selectedEmployer.ID;
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
                NotifyPropertyChanged(nameof(Experiences));
            }
        }
        public ObservableCollection<string> Experiences 
        {
            get
            {
                if (SelectedEmployer != null) return new ObservableCollection<string>(SelectedEmployer.Details);
                else return new ObservableCollection<string>();
            }
        }
        public string SelectedExperience 
        {
            get { return selectedExperience; }
            set
            {
                selectedExperience = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand DeleteCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler ExperienceDeleted;

        // Public Methods

        public DeleteExperienceViewModel(IExperienceRepository repository) 
        {
            repos = repository;
            loadEmployers();
            setUpRelayCommands();
        }

        // Private Methods

        private void loadEmployers() 
        {
            Employers = new ObservableCollection<IExperienceEntity>(repos.GetAll());
        }

        private void deleteExperience() 
        {
            SelectedEmployer.Details.Remove(selectedExperience);
            repos.Update(SelectedEmployer.ID, SelectedEmployer);

            ExperienceDeleted?.Invoke(this, new EventArgs());
        }

        private void reset() 
        {
            repos = null;
            selectedExperience = null;
            selectedEmployer = null;
        }

        private void setUpRelayCommands() 
        {
            DeleteCmd = new RelayCommand(
                T => { deleteExperience(); reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => { reset(); },
                T => true);
        }

    }
}