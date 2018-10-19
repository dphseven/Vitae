namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class AddJobView : UserControl
    {
        public AddJobView(IAddJobViewModel viewModel) 
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            Visibility = Visibility.Collapsed;
        }
    }
}