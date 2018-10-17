namespace Vitae.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IAddExperienceViewModel : INotifyPropertyChanged
    {
        string Employer { get; set; }
        string Experience { get; set; }

        ICommand AddCmd { get; set; }
        ICommand CancelCmd { get; set; }

        event EventHandler ExperienceAdded;
    }
}