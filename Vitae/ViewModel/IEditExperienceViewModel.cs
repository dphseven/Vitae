namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IEditExperienceViewModel : INotifyPropertyChanged
    {
        UIState FormState { get; set; }
        IExperienceEntity SelectedEmployer { get; set; }
        ObservableCollection<IExperienceEntity> Employers { get; set; }
        ObservableCollection<string> ExperienceItems { get; }
        string InitialExperienceItem { get; set; }
        string UpdatedExperienceItem { get; set; }

        ICommand EditCmd { get; set; }
        ICommand CancelCmd { get; set; }

        event EventHandler ExperienceEdited;
    }
}