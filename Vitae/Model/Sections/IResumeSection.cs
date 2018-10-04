namespace Vitae.Model
{
    using System;
    using System.Windows.Documents;
    using Microsoft.Office.Interop.Word;

    public interface IResumeSection
    {
        Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, FlowDocument flowDoc);

        Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Document wordDoc);
    }
}