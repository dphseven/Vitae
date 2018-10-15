namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IDeleteExperienceViewModel : INotifyPropertyChanged 
    {
        Guid EmployerID { get; }
        ObservableCollection<IExperienceEntity> Employers { get; }
        IExperienceEntity SelectedEmployer { get; set; }
        ObservableCollection<string> Experiences { get; }
        string SelectedExperience { get; set; }

        ICommand DeleteCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}