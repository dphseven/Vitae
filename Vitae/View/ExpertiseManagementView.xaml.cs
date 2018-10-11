namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class ExpertiseManagementView : UserControl
    {
        IExpertiseManagementViewModel vm;

        public ExpertiseManagementView(IExpertiseManagementViewModel viewModel) 
        {
            this.DataContext = vm = viewModel; 

            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}