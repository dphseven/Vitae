using Vitae.Model;

namespace Vitae.Services
{
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