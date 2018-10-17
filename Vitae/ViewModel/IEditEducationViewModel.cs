namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IEditEducationViewModel 
    {
        UIState FormState { get; set; }
        Guid ID { get; set; }
        ObservableCollection<IEducationEntity> Institutions { get; set; }
        IEducationEntity SelectedInstitution { get; set; }
        ObservableCollection<string> Credentials { get; set; }
        string SelectedCredential { get; set; }
        string UpdatedCredential { get; set; }

        ICommand EditCmd { get; set; }
        ICommand CancelCmd { get; set; }

        event EventHandler EducationEdited;
    }
}