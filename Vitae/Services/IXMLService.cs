namespace Vitae.Services
{
    using System;
    using System.Collections.Generic;
    using Vitae.Model;

    public interface IXMLService 
    {
        // General Info
        IGeneralInfoEntity GetGeneralInformation(Guid guid);
        Guid Insert(IGeneralInfoEntity entity);
        void DeleteGeneralInfo(Guid guid);
        void Update(Guid g, IGeneralInfoEntity t);

        // Education
        IList<IEducationEntity> GetAllEducations();
        IEducationEntity GetEducation(Guid id);
        Guid Insert(IEducationEntity ee);
        void Delete(Guid g);
        void Update(Guid g, IEducationEntity ee);

        // Publications
        IList<IPublicationEntity> GetPublications();
        Guid Insert(IPublicationEntity pe);
        IPublicationEntity GetPublication(Guid guid);
        void DeletePublication(Guid guid);
        void Update(Guid guid, IPublicationEntity pub);

        // Expertise
        IList<IExpertiseEntity> GetExpertise();
        Guid Insert(IExpertiseEntity entity);
        IExpertiseEntity GetExpertiseItem(Guid guid);
        void DeleteExpertise(Guid g);
        void Update(Guid guid, IExpertiseEntity entity);

        // Experience
        Guid Insert(IExperienceEntity entity);
        IExperienceEntity GetExperience(Guid guid);
        void DeleteExperience(Guid g);
        void Update(Guid guid, IExperienceEntity entity);
        IList<IExperienceEntity> GetAllExperiences();
    }
}