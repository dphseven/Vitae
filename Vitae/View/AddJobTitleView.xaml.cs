namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using ViewModel;

    public partial class AddJobTitleView : UserControl 
    {
        private IAddJobTitleViewModel vm;

        public AddJobTitleView(IAddJobTitleViewModel viewModel) 
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