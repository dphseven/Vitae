namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IEditPublicationViewModel
    {
        UIState FormState { get; set; }
        Guid ID { get; }
        ObservableCollection<IPublicationEntity> Publications { get; set; }
        IPublicationEntity SelectedPublication { get; set; }
        string UpdatedPublication { get; set; }

        ICommand EditCmd { get; set; }
        ICommand CancelCmd { get; set; }

        event EventHandler PublicationEdited;
    }
}