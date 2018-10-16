namespace Vitae.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IDeletePublicationViewModel
    {
        Guid ID { get; }
        ObservableCollection<IPublicationEntity> Publications { get; set; }
        IPublicationEntity SelectedPublication { get; set; }

        ICommand DeleteCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}