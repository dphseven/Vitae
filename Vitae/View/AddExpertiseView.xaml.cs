namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class AddExpertiseView : UserControl
    {
        IAddExpertiseViewModel vm;

        public AddExpertiseView(IAddExpertiseViewModel viewModel) 
        {
            DataContext = vm = viewModel;

            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}