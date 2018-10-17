namespace Vitae.ViewModel
{
    using System;
    using System.Windows.Input;

    public interface IAddPublicationViewModel
    {
        string Publication { get; set; }

        ICommand AddCmd { get; set; }
        ICommand CancelCmd { get; set; }

        event EventHandler PublicationAdded;
    }
}