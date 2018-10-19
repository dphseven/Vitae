namespace Vitae.Model
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public interface IDecoratedExperienceEntity : IExperienceEntity, INotifyPropertyChanged
    {
        string SelectedJobTitle { get; set; }
        ObservableCollection<string> SelectedDetails { get; set; }
        new ObservableCollection<string> Details { get; set; }
    }
}