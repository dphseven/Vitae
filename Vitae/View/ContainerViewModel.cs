namespace Vitae.View
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Vitae.ViewModel;

    public class ContainerViewModel : IContainerViewModel
    {
        private string statusBarMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StatusBarMessage 
        {
            get { return statusBarMessage; }
            set
            {
                statusBarMessage = value;
                notifyPropertyChanged();
            }
        }

        public ContainerViewModel() 
        {

        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}