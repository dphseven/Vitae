namespace Vitae.Services
{
    using Microsoft.Office.Interop.Word;
    using Model;

    public class ResumeCreationService : IResumeCreationService 
    {
        private readonly IResumeDataObject rdo;
        private readonly IResumeFormatObject rfo;
        private readonly IResumeStructureObject rso;
        private readonly IPersistenceService persister;

        public ResumeCreationService(IPersistenceService persistenceService) 
        {
            persister = persistenceService;
        }

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

            persister.Persist(wordDocument, filePathAndName, DocumentPersistenceFileType.Word);

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

            persister.Persist(wordDocument, filePathAndName, DocumentPersistenceFileType.PDF);

            wordDocument.Close(SaveChanges: false);
            wordDocument = null;
            wordApp.Quit();
            wordApp = null;
        }
    }
}