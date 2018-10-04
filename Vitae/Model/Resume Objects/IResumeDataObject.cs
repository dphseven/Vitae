using System.Collections.Generic;

namespace Vitae.Model
{
    public interface IResumeDataObject
    {
        string FullName { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string TagLine { get; set; }
        IList<IExpertiseEntity> ExpertiseEntities { get; set; }
        IList<IExperienceEntity> ExperienceEntities { get; set; }
        IList<IEducationEntity> EducationEntities { get; set; }
        IList<IPublicationEntity> PublicationEntities { get; set; }
    }
}