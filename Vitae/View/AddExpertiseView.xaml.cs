namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class AddExpertiseView : UserControl
    {
        public AddExpertiseView(IAddExpertiseViewModel viewModel) 
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}