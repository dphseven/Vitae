namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class EditJobView : UserControl
    {
        public EditJobView(IEditJobViewModel viewModel) 
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