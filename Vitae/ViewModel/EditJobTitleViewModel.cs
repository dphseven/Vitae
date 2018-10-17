namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Vitae.Model;

    public class EditJobTitleViewModel : ViewModelBase, IEditJobTitleViewModel
    {
        private IExperienceRepository repos;

        private UIState formState = UIState.View;
        private IExperienceEntity selectedEmployer;
        private string selectedJobTitle;
        private string newJobTitle;

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
        public ObservableCollection<string> JobTitles { get; set; }
        public string SelectedJobTitle 
        {
            get { return selectedJobTitle; }
            set
            {
                if (value == null) return;
                else
                {
                    selectedJobTitle = value;
                    NewJobTitle = SelectedJobTitle;
                    FormState = UIState.Edit;
                }
                notifyPropertyChanged();
            }
        }
        public string NewJobTitle 
        {
            get { return newJobTitle; }
            set
            {
                newJobTitle = value;
                notifyPropertyChanged();
            }
        }

        public ICommand EditJobTitleCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler JobTitleEdited;

        // Public Methods

        public EditJobTitleViewModel(IExperienceRepository repository) 
        {
            repos = repository;

            Employers = new ObservableCollection<IExperienceEntity>();
            loadEmployers();

            setUpRelayCommands();
        }

        // Private Methods

        private void editJobTitle() 
        {
            var oldTitleIndex = SelectedEmployer.Titles.IndexOf(SelectedJobTitle);

            SelectedEmployer.Titles.Insert(oldTitleIndex, NewJobTitle);
            SelectedEmployer.Titles.Remove(SelectedJobTitle);

            repos.Update(SelectedEmployer.ID, SelectedEmployer);

            JobTitleEdited?.Invoke(this, new EventArgs());
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
            newJobTitle = null;
        }

        private void setUpRelayCommands() 
        {
            EditJobTitleCmd = new RelayCommand(
                T => { editJobTitle(); resetForm(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => resetForm(),
                T => true);
        }

    }
}