namespace Vitae.View
{
    using Ninject;
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class ContainerView : Window
    {
        private IContainerViewModel vm;
        private IKernel kernel;

        public ContainerView(IContainerViewModel viewModel, IKernel kernel) 
        {
            this.kernel = kernel;
            DataContext = vm = viewModel;

            InitializeComponent();

            UserControl uc = null;

            ResumeCreatorTab.Content = kernel.Get<ResumeCreatorView>();
            uc = ResumeCreatorTab.Content as UserControl;
            uc.Style = (Style)FindResource("TabContent");

            KeywordToolTab.Content = kernel.Get<KeywordToolView>();
            uc = KeywordToolTab.Content as UserControl;
            uc.Style = (Style)FindResource("TabContent");
        }

    }
}
