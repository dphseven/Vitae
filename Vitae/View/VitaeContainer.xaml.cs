namespace Vitae.View
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for VitaeContainer.xaml
    /// </summary>
    public partial class VitaeContainer : Window
    {
        public VitaeContainer() 
        {
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
