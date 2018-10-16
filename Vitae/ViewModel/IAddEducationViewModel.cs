using System.Windows.Input;

namespace Vitae.ViewModel
{
    public interface IAddEducationViewModel
    {
        string Institution { get; set; }
        string Credential { get; set; }

        ICommand AddCmd { get; set; }
        ICommand CancelCmd { get; set; }
    }
}