namespace Vitae.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ContainerViewModel : IContainerViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ContainerViewModel() 
        {

        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}