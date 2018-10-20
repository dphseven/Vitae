namespace Vitae
{
    using Model;
    using Ninject;
    using Ninject.Modules;
    using Services;
    using ViewModel;
    using Vitae.View;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            // VIEW

            Bind<AddEducationView>().ToSelf();
            Bind<AddExperienceView>().ToSelf();
            Bind<AddExpertiseView>().ToSelf();
            Bind<AddJobTitleView>().ToSelf();
            Bind<AddJobView>().ToSelf();
            Bind<AddPublicationView>().ToSelf();
            Bind<ContainerView>().ToSelf();
            Bind<DeleteEducationView>().ToSelf();
            Bind<DeleteExperienceView>().ToSelf();
            Bind<DeleteExpertiseView>().ToSelf();
            Bind<DeleteJobTitleView>().ToSelf();
            Bind<DeletePublicationView>().ToSelf();
            Bind<EditEducationView>().ToSelf();
            Bind<EditExperienceView>().ToSelf();
            Bind<EditExpertiseView>().ToSelf();
            Bind<EditJobTitleView>().ToSelf();
            Bind<EditJobView>().ToSelf();
            Bind<EditPublicationView>().ToSelf();
            Bind<KeywordToolView>().ToSelf();
            Bind<ResumeCreatorView>().ToSelf();

            // VIEWMODEL

            Bind<IContainerViewModel>().To<ContainerViewModel>();
            Bind<IKeywordToolViewModel>().To<KeywordToolViewModel>();
            Bind<IResumeCreatorViewModel>().To<ResumeCreatorViewModel>();

            Bind<IAddExpertiseViewModel>().To<AddExpertiseViewModel>();
            Bind<IEditExpertiseViewModel>().To<EditExpertiseViewModel>();
            Bind<IAddJobTitleViewModel>().To<AddJobTitleViewModel>();
            Bind<IEditJobTitleViewModel>().To<EditJobTitleViewModel>();
            Bind<IDeleteJobTitleViewModel>().To<DeleteJobTitleViewModel>();
            Bind<IDeleteExpertiseViewModel>().To<DeleteExpertiseViewModel>();
            Bind<IAddExperienceViewModel>().To<AddExperienceViewModel>();
            Bind<IEditExperienceViewModel>().To<EditExperienceViewModel>();
            Bind<IDeleteExperienceViewModel>().To<DeleteExperienceViewModel>();
            Bind<IAddEducationViewModel>().To<AddEducationViewModel>();
            Bind<IEditEducationViewModel>().To<EditEducationViewModel>();
            Bind<IDeleteEducationViewModel>().To<DeleteEducationViewModel>();
            Bind<IAddPublicationViewModel>().To<AddPublicationViewModel>();
            Bind<IEditPublicationViewModel>().To<EditPublicationViewModel>();
            Bind<IDeletePublicationViewModel>().To<DeletePublicationViewModel>();
            Bind<IAddJobViewModel>().To<AddJobViewModel>();
            Bind<IEditJobViewModel>().To<EditJobViewModel>();
            Bind<IDeleteJobViewModel>().To<DeleteJobViewModel>();

            // SERVICES

            Bind<IExperienceItem>().To<ExperienceItem>();
            Bind<IJobTitle>().To<JobTitle>();

            Bind<IKeywordService>().To<KeywordService>();
            Bind<ILoggingService>().To<LoggingService>();
            Bind<IResumeCreationService>().To<ResumeCreationService>();

            Bind(typeof(IXMLService<IPublicationEntity>)).To(typeof(PublicationsXMLService));
            Bind<IPublicationsXMLService>().To<PublicationsXMLService>();
            Bind(typeof(IXMLService<IGeneralInfoEntity>)).To(typeof(GeneralInfoXMLService));
            Bind<IGeneralInfoXMLService>().To<GeneralInfoXMLService>();
            Bind(typeof(IXMLService<IExperienceEntity>)).To(typeof(ExperienceXMLService));
            Bind<IExperienceXMLService>().To<ExperienceXMLService>();
            Bind(typeof(IXMLService<IEducationEntity>)).To(typeof(EducationXMLService));
            Bind<IEducationXMLService>().To<EducationXMLService>();
            Bind(typeof(IXMLService<IExpertiseEntity>)).To(typeof(ExpertiseXMLService));
            Bind<IExpertiseXMLService>().To<ExpertiseXMLService>();

            Bind<IPersistenceService>().To<PersistenceService>();

            // MODEL

            Bind<IEducationEntity>().To<EducationEntity>();
            Bind<IExperienceEntity>().To<ExperienceEntity>();
            Bind<IExpertiseEntity>().To<ExpertiseEntity>();
            Bind<IPublicationEntity>().To<PublicationEntity>();
            Bind<IGeneralInfoEntity>().To<GeneralInfoEntity>();
            Bind<IDecoratedExperienceEntity>().To<DecoratedExperienceEntity>();

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
        }
    }
}