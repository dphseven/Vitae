namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IEditExpertiseViewModel : INotifyPropertyChanged
    {
        UIState FormState { get; set; }

        Guid ID { get; set; }
        ObservableCollection<string> Categories { get; }
        string SelectedCategory { get; set; }
        ObservableCollection<string> ExpertiseItems { get; }
        string SelectedExpertiseItem { get; set; }
        //string Expertise { get; set; }

        ICommand EditButtonCmd { get; set; }
        ICommand CancelButtonCmd { get; set; }
    }
}