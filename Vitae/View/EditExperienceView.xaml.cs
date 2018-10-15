namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;
    using ViewModel;

    public partial class EditExperienceView : UserControl
    {
        IEditExperienceViewModel vm;

        public EditExperienceView(IEditExperienceViewModel viewModel) 
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