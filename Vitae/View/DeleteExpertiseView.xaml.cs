

namespace Vitae.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Vitae.ViewModel;

    public partial class DeleteExpertiseView : UserControl
    {
        IDeleteExpertiseViewModel vm;

        public DeleteExpertiseView(IDeleteExpertiseViewModel viewModel)
        {
            DataContext = vm = viewModel;

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
