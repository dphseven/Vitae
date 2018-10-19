namespace Vitae.ViewModel
{
    using System;
    using System.Windows.Input;

    public interface IEditJobViewModel
    {
        Guid ID { get; set; }
        string Employer { get; set; }
        string JobTitles { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }

        ICommand EditCmd { get; set; }

        event EventHandler JobEdited;
    }
}