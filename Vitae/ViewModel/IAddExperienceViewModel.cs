namespace Vitae.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IAddExperienceViewModel : INotifyPropertyChanged
    {
        string Employer { get; set; }
        string Experience { get; set; }

        ICommand AddCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}