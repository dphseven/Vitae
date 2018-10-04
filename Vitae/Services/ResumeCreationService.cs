namespace Vitae.Services
{
    using Microsoft.Office.Interop.Word;
    using Model;

    public class ResumeCreationService : IResumeCreationService
    {
        public void CreateResumeAsWordFile(
            IResumeDataObject rdo,
            IResumeFormatObject rfo,
            IResumeStructureObject rso,
            string filePathAndName) 
        {
            Application wordApp = new Application(); 
            var wordDocument = wordApp.Documents.Add();

            foreach (var item in rso.ResumeSections)
                item.AddToWordDocument(rdo, rfo, wordDocument).DynamicInvoke();

            wordDocument.SaveAs2(FileName: filePathAndName);

            wordDocument.Close();
            wordDocument = null;
            wordApp.Quit();
            wordApp = null;
        }

        public void CreateResumeAsPdfFile(
            IResumeDataObject rdo,
            IResumeFormatObject rfo,
            IResumeStructureObject rso,
            string filePathAndName) 
        {
            Application wordApp = new Application();
            var wordDocument = wordApp.Documents.Add();

            foreach (var item in rso.ResumeSections)
                item.AddToWordDocument(rdo, rfo, wordDocument).DynamicInvoke();

            wordDocument.ExportAsFixedFormat(filePathAndName, WdExportFormat.wdExportFormatPDF);

            wordDocument.Close(SaveChanges: false);
            wordDocument = null;
            wordApp.Quit();
            wordApp = null;
        }
    }
}