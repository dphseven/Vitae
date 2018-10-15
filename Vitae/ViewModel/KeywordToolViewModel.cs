namespace Vitae.ViewModel
{
    using mshtml;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class KeywordToolViewModel : ViewModelBase, INotifyPropertyChanged, IKeywordToolViewModel
    {
        IKeywordService ks;

        private string messageDisplayed;
        private string textForAnalysis;
        private int minimumKeywordLength = 0;
        private string url;

        public string MessageDisplayed 
        {
            get { return messageDisplayed; }
            set
            {
                messageDisplayed = value;
                notifyPropertyChanged();
            }
        }
        public string TextForAnalysis 
        {
            get { return textForAnalysis; }
            set
            {
                textForAnalysis = value;
                notifyPropertyChanged();
            }
        }
        public int MinimumKeywordLength 
        {
            get { return minimumKeywordLength; }
            set
            {
                minimumKeywordLength = value;
                notifyPropertyChanged();
            }
        }
        public HTMLDocument BrowserPage { get; set; }
        public string Url 
        {
            get { return url; }
            set
            {
                url = value;
                notifyPropertyChanged();
            }
        }

        public KeywordToolViewModel(IKeywordService iks) 
        {
            ks = iks;
        }

        public void FindKeyWords() 
        {
            MessageDisplayed = string.Empty;

            var keywords = ks.GetKeywordsforZipRecruiterJobDescription(BrowserPage, Convert.ToInt32(minimumKeywordLength)).ToList();
            if (keywords.Count == 0) return;

            foreach (string word in keywords)
            {
                MessageDisplayed += word;
                MessageDisplayed += ", ";
            }

            MessageDisplayed = MessageDisplayed.Remove(MessageDisplayed.Length - 2);
        }

    }
}