namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IAddJobTitleViewModel : INotifyPropertyChanged
    {
        UIState FormState { get; set; }

        ObservableCollection<IExperienceEntity> Employers { get; set; }
        IExperienceEntity SelectedEmployer { get; set; }
        Guid EmployerID { get; set; }
        string JobTitle { get; set; }

        ICommand AddJobTitleCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}