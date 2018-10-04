namespace Vitae.Model
{
    using System;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public interface IPublicationsSection : IResumeSection
    {
        new Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc);
        new Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc);
    }
}