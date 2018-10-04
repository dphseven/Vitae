using mshtml;

namespace Vitae.ViewModel
{
    public interface IKeywordToolViewModel
    {
        string MessageDisplayed { get; set; }
        string TextForAnalysis { get; set; }
        int MinimumKeywordLength { get; set; }
        HTMLDocument BrowserPage { get; set; }

        void FindKeyWords();
    }
}