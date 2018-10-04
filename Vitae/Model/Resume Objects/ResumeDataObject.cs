namespace Vitae.Model
{
    using System.Collections.Generic;

    public class ResumeDataObject : IResumeDataObject
    {
        public string FullName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TagLine { get; set; }
        public IList<IExpertiseEntity> ExpertiseEntities { get; set; }
        public IList<IExperienceEntity> ExperienceEntities { get; set; }
        public IList<IEducationEntity> EducationEntities { get; set; }
        public IList<IPublicationEntity> PublicationEntities { get; set; }

        public ResumeDataObject() 
        {
            FullName = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            TagLine = string.Empty;

            ExpertiseEntities = new List<IExpertiseEntity>();
            ExperienceEntities = new List<IExperienceEntity>();
            EducationEntities = new List<IEducationEntity>();
            PublicationEntities = new List<IPublicationEntity>();
        }
    }
}