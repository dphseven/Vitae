namespace Vitae.Model
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// Wrapper around ExperienceEntity that provides properties for item selection, etc.
    /// </summary>
    public class DecoratedExperienceEntity : ExperienceEntity, IDecoratedExperienceEntity, INotifyPropertyChanged
    {
        private string selectedJobTitle = "";
        public string SelectedJobTitle 
        {
            get { return selectedJobTitle; }
            set
            {
                selectedJobTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedJobTitle)));
            }
        }

        public new ObservableCollection<string> Details { get; set; }
        public ObservableCollection<string> SelectedDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DecoratedExperienceEntity() : base() 
        {
            Details = new ObservableCollection<string>();
            SelectedDetails = new ObservableCollection<string>();
        }

    }
}