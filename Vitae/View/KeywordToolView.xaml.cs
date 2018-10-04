namespace Vitae.View
{
    using mshtml;
    using Ninject;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModel;

    public partial class KeywordToolView : UserControl
    {
        private IKeywordToolViewModel vm;

        public KeywordToolView() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                this.DataContext = vm = ioc.Get<IKeywordToolViewModel>();
                InitializeComponent();
            }
        }

        private void FindKeywordsButton_Click(object sender, RoutedEventArgs e) 
        {
            vm.BrowserPage = browser.Document as HTMLDocument;
            if (vm.BrowserPage != null) vm.FindKeyWords();
        }

        private void GoToUrlButton_Click(object sender, RoutedEventArgs e) 
        {
            string text = TextInputBlock.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
                {
                    browser.Navigate(new Uri(text));
                }
                else
                {
                    browser.NavigateToString("<html><h3>Invalid URL.</h3></html>");
                }
            }
        }
    }
}