namespace Vitae.View
{
    using Ninject;
    using System.Windows;
    using System.Windows.Controls;
    using Vitae.ViewModel;

    public partial class VitaeContainer : Window
    {
        private IContainerViewModel vm;

        public VitaeContainer() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                DataContext = vm = ioc.Get<IContainerViewModel>();
            }

            InitializeComponent();

            UserControl uc = null;
            ResumeCreatorTab.Content = new ResumeCreatorView();
            uc = ResumeCreatorTab.Content as UserControl;
            uc.Style = (Style)FindResource("TabContent");
            KeywordToolTab.Content = new KeywordToolView();
            uc = KeywordToolTab.Content as UserControl;
            uc.Style = (Style)FindResource("TabContent");
        }

    }
}
