namespace Vitae
{
    using Ninject;
    using Ninject.Modules;
    using Model;
    using Services;
    using ViewModel;
    using System;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            // MODEL

            Bind<IEducationEntity>().To<EducationEntity>();
            Bind<IExperienceEntity>().To<ExperienceEntity>();
            Bind<IExpertiseEntity>().To<ExpertiseEntity>();
            Bind<IPublicationEntity>().To<PublicationEntity>();
            Bind<IGeneralInfoEntity>().To<GeneralInfoEntity>();

            Bind<IBasicInfoSection>().To<BasicInfoSection>();
            Bind<IEducationSection>().To<EducationSection>();
            Bind<IExperienceSection>().To<ExperienceSection>();
            Bind<IExpertiseSection>().To<ExpertiseSection>();
            Bind<IFullNameSection>().To<FullNameSection>();
            Bind<IPublicationsSection>().To<PublicationsSection>();
            Bind<ITagLineSection>().To<TagLineSection>();

            Bind<IResumeDataObject>().To<ResumeDataObject>();
            Bind<IResumeFormatObject>().To<ResumeFormatObject>();
            Bind<IResumeStructureObject>().To<ResumeStructureObject>();

            Bind<IEducationRepository>().To<EducationRepository>();
            Bind<IExperienceRepository>().To<ExperienceRepository>();
            Bind<IExpertiseRepository>().To<ExpertiseRepository>();
            Bind<IPublicationsRepository>().To<PublicationsRepository>();
            Bind<IGeneralInfoRepository>().To<GeneralInfoRepository>();

            // SERVICES

            Bind<IExperienceItem>().To<ExperienceItem>();
            Bind<IJobTitle>().To<JobTitle>();
            Bind<IKeywordService>().To<KeywordService>();
            Bind<ILoggingService>().To<LoggingService>();
            Bind<IResumeCreationService>().To<ResumeCreationService>();
            Bind<IXMLService>().To<XMLService>();

            // VIEWMODEL

            Bind<IKeywordToolViewModel>().To<KeywordToolViewModel>();
            Bind<IResumeCreatorViewModel>().To<ResumeCreatorViewModel>()
                .WithConstructorArgument("giGuid", Guid.Empty);
        }
    }
}
