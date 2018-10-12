namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IDeleteJobTitleViewModel : INotifyPropertyChanged
    {
        UIState FormState { get; set; }

        Guid EmployerID { get; set; }
        ObservableCollection<IExperienceEntity> Employers { get; set; }
        IExperienceEntity SelectedEmployer { get; set; }
        string SelectedJobTitle {get;set;}

        ICommand DeleteJobTitleCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}