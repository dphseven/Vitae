namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IDeleteExpertiseViewModel : INotifyPropertyChanged
    {
        Guid ExpertiseID { get; }
        ObservableCollection<string> Categories { get; }
        string SelectedCategory { get; set; }
        ObservableCollection<IExpertiseEntity> Expertises { get; }
        IExpertiseEntity SelectedExpertise { get; set; }

        ICommand DeleteCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}