namespace Vitae.ViewModel
{
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public interface IExpertiseManagementViewModel
    {
        Guid ID { get; set; }
        string Category { get; set; }
        string Expertise { get; set; }
        UIState FormState { get; set; }

        ICommand AddButtonCmd { get; set; }
        ICommand EditButtonCmd { get; set; }
        ICommand CancelButtonCmd { get; set; }

        void InjectExpertiseEntity(IExpertiseEntity ee);
    }
}