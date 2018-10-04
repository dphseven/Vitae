using Word = Microsoft.Office.Interop.Word;
using System;
using Win = System.Windows.Documents;

namespace Vitae.Model
{
    public interface IFullNameSection : IResumeSection
    {
        new Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc);
        new Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc);
    }
}