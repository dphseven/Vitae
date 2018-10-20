namespace Vitae.Services
{
    using Vitae.Model;

    public interface IResumeCreationService
    {
        void CreateResumeAsWordFile(
            IResumeDataObject rdo,
            IResumeFormatObject rfo,
            IResumeStructureObject rso,
            string filePathAndName);
        void CreateResumeAsPdfFile(
            IResumeDataObject rdo,
            IResumeFormatObject rfo,
            IResumeStructureObject rso,
            string filePathAndName);
    }
}