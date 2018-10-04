using System;
using Win = System.Windows.Documents;
using Word = Microsoft.Office.Interop.Word;

namespace Vitae.Model
{
    public interface IBasicInfoSection : IResumeSection
    {
        new Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc);
        new Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc);
    }
}