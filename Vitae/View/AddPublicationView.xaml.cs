namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class AddPublicationView : UserControl
    {
        public AddPublicationView(IAddPublicationViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
