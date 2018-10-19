namespace Vitae.ViewModel
{
    using System;
    using System.Windows.Input;

    public interface IAddJobViewModel
    {
        string Employer { get; set; }
        string JobTitle { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }

        ICommand AddCmd { get; set; }

        event EventHandler JobAdded;
    }
}