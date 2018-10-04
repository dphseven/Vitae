using mshtml;
using System.Collections.Generic;

namespace Vitae.Services
{
    public interface IKeywordService
    {
        IEnumerable<string> GetKeywords(string inputText, int minimumLength);
        IEnumerable<string> GetKeywordsforZipRecruiterJobDescription(HTMLDocument doc, int minimumLength);
    }
}