namespace Vitae.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IAddExpertiseViewModel : INotifyPropertyChanged
    {
        Guid ID { get; set; }
        string Category { get; set; }
        string Expertise { get; set; }

        UIState FormState { get; set; }

        ICommand AddButtonCmd { get; set; }
        ICommand CancelButtonCmd { get; set; }
    }
}