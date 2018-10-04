namespace Vitae.Services
{
    using mshtml;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class KeywordService : IKeywordService
    {
        private string zipRecruiterJobDescriptionHtmlClass = "jobDescriptionSection";

        public IEnumerable<string> GetKeywords(string inputText, int minimumLength) 
        {
            if (string.IsNullOrEmpty(inputText)) return new List<string>();

            string cleanText = Regex.Replace(inputText, "[^a-zA-Z ]", " ");

            List<string> words = new List<string>(cleanText.Split(' '));
            List<string> keywords = new List<string>();

            foreach (string word in words)
            {
                if (word.Length >= minimumLength && !keywords.Contains(word.ToLower()))
                    keywords.Add(word.ToLower().Trim());
            }

            return keywords;
        }

        public IEnumerable<string> GetKeywordsforZipRecruiterJobDescription(HTMLDocument doc, int minimumLength) 
        {
            List<string> keywords = new List<string>();
            if (doc == null) return new List<string>();

            List<IHTMLElement> jobDescElements = getElementsByClass(doc, zipRecruiterJobDescriptionHtmlClass).ToList();
            if (jobDescElements.Count == 0) return keywords;

            string bodyText = jobDescElements.FirstOrDefault().innerText;

            string cleanText = Regex.Replace(bodyText, "[^a-zA-Z ]", " ");

            List<string> words = new List<string>(cleanText.Split(' '));

            foreach (string word in words)
            {
                if (word.Length >= minimumLength && !keywords.Contains(word.ToLower()))
                    keywords.Add(word.ToLower());
            }

            return keywords;
        }

        private IEnumerable<IHTMLElement> getElementsByClass(HTMLDocument doc, string className) 
        {
            var list = new List<IHTMLElement>();

            foreach (IHTMLElement e in doc.all)
            {
                if (e.className == className) list.Add(e);
            }

            return list;
        }
    }
}