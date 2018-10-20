namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class EditPublicationView : UserControl
    {
        public EditPublicationView(IEditPublicationViewModel viewModel) 
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}