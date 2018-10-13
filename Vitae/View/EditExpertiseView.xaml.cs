namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class EditExpertiseView : UserControl
    {
        IEditExpertiseViewModel vm;

        public EditExpertiseView(IEditExpertiseViewModel viewModel) 
        {
            DataContext = vm = viewModel;

            InitializeComponent();
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