namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IDeleteEducationViewModel
    {
        Guid ID { get; }
        ObservableCollection<IEducationEntity> Institutions { get; set; }
        IEducationEntity SelectedInstitution { get; set; }
        ObservableCollection<string> Credentials { get; set; }
        string SelectedCredential { get; set; }

        ICommand DeleteCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}