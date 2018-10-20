namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class DeleteExperienceView : UserControl
    {
        public DeleteExperienceView(IDeleteExperienceViewModel viewModel) 
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}