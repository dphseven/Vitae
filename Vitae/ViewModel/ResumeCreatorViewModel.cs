namespace Vitae.ViewModel
{
    using Ninject;
    using Model;
    using Services;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Collections.Generic;

    public class ResumeCreatorViewModel : INotifyPropertyChanged, IResumeCreatorViewModel
    {
        private IResumeCreationService rcs;
        private IGeneralInfoRepository giRepos;
        private IExperienceRepository experienceRepos;
        private IExpertiseRepository expertiseRepos;
        private IEducationRepository edRepos;
        private IPublicationsRepository pubRepos;
        private ILoggingService ls;

        // Basic Info Properties
        private string fullName = string.Empty;
        private string addLine1 = string.Empty;
        private string addLine2 = string.Empty;
        private string phone = string.Empty;
        private string email = string.Empty;
        private string tagline = string.Empty;
        public string FullName 
        {
            get { return fullName; }
            set
            {
                fullName = value;
                notifyPropertyChanged();
            }
        }
        public string AddLine1 
        {
            get { return addLine1; }
            set
            {
                addLine1 = value;
                notifyPropertyChanged();
            }
        }
        public string AddLine2 
        {
            get { return addLine2; }
            set
            {
                addLine2 = value;
                notifyPropertyChanged();
            }
        }
        public string Phone 
        {
            get { return phone; }
            set
            {
                phone = value;
                notifyPropertyChanged();
            }
        }
        public string Email 
        {
            get { return email; }
            set
            {
                email = value;
                notifyPropertyChanged();
            }
        }
        public string TagLine 
        {
            get { return tagline; }
            set
            {
                tagline = value;
                notifyPropertyChanged();
            }
        }

        // Expertise Properties
        public ObservableCollection<IExpertiseEntity> AllExpertises { get; set; }
        public ObservableCollection<IExpertiseEntity> InExpertises { get; set; }
        public ObservableCollection<IExpertiseEntity> OutExpertises { get; set; }
        private IExpertiseEntity selectedOutExpertise;
        public IExpertiseEntity SelectedOutExpertise 
        {
            get { return selectedOutExpertise; }
            set
            {
                selectedOutExpertise = value;
                notifyPropertyChanged();
            }
        }
        private IExpertiseEntity selectedInExpertise;
        public IExpertiseEntity SelectedInExpertise 
        {
            get { return selectedInExpertise; }
            set
            {
                selectedInExpertise = value;
                notifyPropertyChanged();
            }
        }

        // Job Titles Properties
        public ObservableCollection<JobTitleSelectionObject> AllJTSOs { get; set; }

        // Experience Properties
        public ObservableCollection<string> AllEmployers { get; set; }
        public string SelectedEmployer { get; set; }
        public ObservableCollection<IExperienceItem> AllExperiences { get; set; }
        public ObservableCollection<IExperienceItem> OutExperiences { get; set; }
        public ObservableCollection<IExperienceItem> InExperiences { get; set; }
        public ObservableCollection<IExperienceItem> AllInExperiences { get; set; }
        public IExperienceItem SelectedInExperience { get; set; }
        public IExperienceItem SelectedOutExperience { get; set; }

        // Education Properties
        public ObservableCollection<IEducationEntity> AllEducations { get; set; }
        public ObservableCollection<IEducationEntity> InEducations { get; set; }
        public ObservableCollection<IEducationEntity> OutEducations { get; set; }
        private IEducationEntity selectedOutEducation;
        public IEducationEntity SelectedOutEducation 
        {
            get { return selectedOutEducation; }
            set
            {
                selectedOutEducation = value;
                notifyPropertyChanged();
            }
        }
        private IEducationEntity selectedInEducation;
        public IEducationEntity SelectedInEducation 
        {
            get { return selectedInEducation; }
            set
            {
                selectedInEducation = value;
                notifyPropertyChanged();
            }
        }

        // Publication Properties
        public ObservableCollection<IPublicationEntity> AllPublications { get; set; }
        public ObservableCollection<IPublicationEntity> InPublications { get; set; }
        public ObservableCollection<IPublicationEntity> OutPublications { get; set; }
        private IPublicationEntity selectedOutPublication;
        public IPublicationEntity SelectedOutPublication 
        {
            get { return selectedOutPublication; }
            set
            {
                selectedOutPublication = value;
                notifyPropertyChanged();
            }
        }
        private IPublicationEntity selectedInPublication;
        public IPublicationEntity SelectedInPublication 
        {
            get { return selectedInPublication; }
            set
            {
                selectedInPublication = value;
                notifyPropertyChanged();
            }
        }

        // Resume Preview Properties
        private FlowDocument resumePreview = new FlowDocument();
        public FlowDocument ResumePreview 
        {
            get { return resumePreview; }
            set
            {
                resumePreview = value;
                notifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddExpertiseCommand { get; set; }
        public ICommand RemoveExpertiseCommand { get; set; }
        public ICommand MoveExpertiseUpCommand { get; set; }
        public ICommand MoveExpertiseDownCommand { get; set; }

        public ICommand AddExperienceCommand { get; set; }
        public ICommand RemoveExperienceCommand { get; set; }
        public ICommand MoveExperienceUpCommand { get; set; }
        public ICommand MoveExperienceDownCommand { get; set; }

        public ICommand AddEducationCommand { get; set; }
        public ICommand RemoveEducationCommand { get; set; }
        public ICommand MoveEducationUpCommand { get; set; }
        public ICommand MoveEducationDownCommand { get; set; }

        public ICommand AddPublicationCommand { get; set; }
        public ICommand RemovePublicationCommand { get; set; }
        public ICommand MovePublicationUpCommand { get; set; }
        public ICommand MovePublicationDownCommand { get; set; }

        /*************************
        **************************
        ***** PUBLIC METHODS *****
        **************************
        *************************/

        public ResumeCreatorViewModel(
            IResumeCreationService rcs, 
            ILoggingService ls,
            Guid giGuid,
            IGeneralInfoRepository giRepository,
            IExperienceRepository experienceRepository,
            IExpertiseRepository expertiseRepository,
            IEducationRepository educationRepository,
            IPublicationsRepository publicationsRepository
            ) 
        {
            try
            {
                this.rcs = rcs;
                this.giRepos = giRepository;
                this.experienceRepos = experienceRepository;
                this.expertiseRepos = expertiseRepository;
                this.edRepos = educationRepository;
                this.pubRepos = publicationsRepository;
                this.ls = ls;

                setUpRelayCommands();
                
                var gie = giRepos.Get(giGuid);
                FullName = gie.FullName;
                Email = gie.Email;
                Phone = gie.Phone;
                AddLine1 = gie.Add1;
                AddLine2 = gie.Add2;

                AllEmployers = new ObservableCollection<string>(experienceRepos.GetAll().Select(T => T.Employer).Distinct());

                InExpertises = new ObservableCollection<IExpertiseEntity>();
                OutExpertises = AllExpertises = new ObservableCollection<IExpertiseEntity>(expertiseRepos.GetAll());

                InEducations = new ObservableCollection<IEducationEntity>();
                OutEducations = AllEducations = new ObservableCollection<IEducationEntity>(edRepos.GetAll());

                InPublications = new ObservableCollection<IPublicationEntity>();
                OutPublications = AllPublications = new ObservableCollection<IPublicationEntity>(pubRepos.GetAll());

                AllExperiences = new ObservableCollection<IExperienceItem>(experienceRepos.GetAllExperienceItems());
                OutExperiences = new ObservableCollection<IExperienceItem>();
                InExperiences = new ObservableCollection<IExperienceItem>();
                AllInExperiences = new ObservableCollection<IExperienceItem>();

                AllJTSOs = new ObservableCollection<JobTitleSelectionObject>();
                loadJobTitleSectionObjects();

                updateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        public void ExportResumeToPdf(string filePathAndName) 
        {
            try
            {
                using (var ioc = new VitaeNinjectKernel())
                {
                    var rdo = createRdo();
                    var rfo = ioc.Get<IResumeFormatObject>();
                    var rso = ioc.Get<IResumeStructureObject>();
                    rso.AddSection(ioc.Get<IFullNameSection>());
                    rso.AddSection(ioc.Get<IBasicInfoSection>());
                    rso.AddSection(ioc.Get<ITagLineSection>());
                    rso.AddSection(ioc.Get<IExpertiseSection>());
                    rso.AddSection(ioc.Get<IExperienceSection>());
                    rso.AddSection(ioc.Get<IEducationSection>());
                    rso.AddSection(ioc.Get<IPublicationsSection>());

                    rcs.CreateResumeAsPdfFile(rdo, rfo, rso, filePathAndName);
                }
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        public void ExportResumeToWord(string filePathAndName) 
        {
            try
            {
                using (var ioc = new VitaeNinjectKernel())
                {
                    var rdo = createRdo();
                    var rfo = ioc.Get<IResumeFormatObject>();
                    var rso = ioc.Get<IResumeStructureObject>();
                    rso.AddSection(ioc.Get<IFullNameSection>());
                    rso.AddSection(ioc.Get<IBasicInfoSection>());
                    rso.AddSection(ioc.Get<ITagLineSection>());
                    rso.AddSection(ioc.Get<IExpertiseSection>());
                    rso.AddSection(ioc.Get<IExperienceSection>());
                    rso.AddSection(ioc.Get<IEducationSection>());
                    rso.AddSection(ioc.Get<IPublicationsSection>());
                    
                    rcs.CreateResumeAsWordFile(rdo, rfo, rso, filePathAndName);
                }
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        public void UpdateExperienceLists() 
        {
            try
            {
                // Load In Experiences
                var y = new ObservableCollection<IExperienceItem>(AllInExperiences.Where(T => T.Employer == SelectedEmployer));
                InExperiences.Clear();
                foreach (var item in y) InExperiences.Add(item);

                // Load Out Experiences
                var x = new ObservableCollection<IExperienceItem>(AllExperiences.Where(T => T.Employer == SelectedEmployer && !InExperiences.Contains(T)));
                OutExperiences.Clear();
                foreach (var item in x) OutExperiences.Add(item);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        /**************************
        ***************************
        ***** PRIVATE METHODS *****
        ***************************
        **************************/

        private void addExpertise() 
        {
            if (SelectedOutExpertise == null) return;
            try
            {
                InExpertises.Add(SelectedOutExpertise);
                OutExpertises.Remove(SelectedOutExpertise);
                SelectedOutExpertise = null;
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void removeExpertise() 
        {
            try
            {
                if (SelectedInExpertise == null) return;

                OutExpertises.Add(SelectedInExpertise);
                InExpertises.Remove(SelectedInExpertise);
                SelectedInExpertise = null;
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveExpertiseUp() 
        {
            try
            {
                if (SelectedInExpertise == null) return;

                int currentIndex = InExpertises.IndexOf(SelectedInExpertise);
                InExpertises.Move(currentIndex, currentIndex - 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveExpertiseDown() 
        {
            try
            {
                if (SelectedInExpertise == null) return;

                int currentIndex = InExpertises.IndexOf(SelectedInExpertise);
                InExpertises.Move(currentIndex, currentIndex + 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void addExperienceItem() 
        {
            try
            {
                if (SelectedOutExperience == null) return;

                AllInExperiences.Add(SelectedOutExperience);
                InExperiences.Add(SelectedOutExperience);
                OutExperiences.Remove(SelectedOutExperience);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void removeExperienceItem() 
        {
            try
            {
                if (SelectedInExperience == null) return;

                OutExperiences.Add(SelectedInExperience);
                AllInExperiences.Remove(SelectedInExperience);
                InExperiences.Remove(SelectedInExperience);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveExperienceItemUp() 
        {
            try
            {
                if (SelectedInExperience == null) return;

                int currentIndex = InExperiences.IndexOf(SelectedInExperience);
                InExperiences.Move(currentIndex, currentIndex - 1);

                currentIndex = AllInExperiences.IndexOf(SelectedInExperience);
                AllInExperiences.Move(currentIndex, currentIndex - 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveExperienceItemDown() 
        {
            try
            {
                if (SelectedInExperience == null) return;

                int currentIndex = InExperiences.IndexOf(SelectedInExperience);
                InExperiences.Move(currentIndex, currentIndex + 1);

                currentIndex = AllInExperiences.IndexOf(SelectedInExperience);
                AllInExperiences.Move(currentIndex, currentIndex + 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void addEducation() 
        {
            try
            {
                if (SelectedOutEducation == null) return;

                InEducations.Add(SelectedOutEducation);
                OutEducations.Remove(SelectedOutEducation);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void removeEducation() 
        {
            try
            {
                if (SelectedInEducation == null) return;

                OutEducations.Add(SelectedInEducation);
                InEducations.Remove(SelectedInEducation);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveEducationUp() 
        {
            try
            {
                if (SelectedInEducation == null) return;

                int currentIndex = InEducations.IndexOf(SelectedInEducation);
                InEducations.Move(currentIndex, currentIndex - 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void moveEducationDown() 
        {
            try
            {
                if (SelectedInEducation == null) return;

                int currentIndex = InEducations.IndexOf(SelectedInEducation);
                InEducations.Move(currentIndex, currentIndex + 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void addPublication() 
        {
            try
            {
                if (SelectedOutPublication == null) return;

                InPublications.Add(SelectedOutPublication);
                OutPublications.Remove(SelectedOutPublication);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void removePublication() 
        {
            if (SelectedInPublication == null) return;
            try
            {
                OutPublications.Add(SelectedInPublication);
                InPublications.Remove(SelectedInPublication);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void movePublicationUp() 
        {
            if (SelectedInPublication == null) return;
            try
            {
                int currentIndex = InPublications.IndexOf(SelectedInPublication);
                InPublications.Move(currentIndex, currentIndex - 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void movePublicationDown() 
        {
            if (SelectedInPublication == null) return;
            try
            {
                int currentIndex = InPublications.IndexOf(SelectedInPublication);
                InPublications.Move(currentIndex, currentIndex + 1);
                notifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private IResumeDataObject createRdo() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                IResumeDataObject rdo = ioc.Get<IResumeDataObject>();
                rdo.FullName = this.FullName;
                rdo.PhoneNumber = this.Phone;
                rdo.Email = this.Email;
                rdo.AddressLine1 = this.AddLine1;
                rdo.AddressLine2 = this.AddLine2;
                rdo.TagLine = this.TagLine;

                if (InExpertises != null)
                {
                    foreach (IExpertiseEntity item in InExpertises) rdo.ExpertiseEntities.Add(item);
                }

                var allJobs = experienceRepos.GetAll();
                for (int i = 0; i < allJobs.Count; i++)
                {
                    IExperienceEntity ee = ioc.Get<IExperienceEntity>();
                    ee.Employer = allJobs[i].Employer;
                    ee.StartDate = allJobs[i].StartDate;
                    ee.EndDate = allJobs[i].EndDate;
                    if (AllJTSOs != null) ee.Titles.Add(AllJTSOs.SingleOrDefault(T => T.Company == allJobs[i].Employer)?.SelectedJobTitle);
                    if (AllInExperiences != null && AllInExperiences.Count > 0)
                        (ee.Details as List<string>).AddRange(AllInExperiences.Where(T => T.Employer == ee.Employer).Select(T => T.ExperienceDetail));
                    rdo.ExperienceEntities.Add(ee);
                }

                if (InEducations != null) foreach (IEducationEntity item in InEducations) rdo.EducationEntities.Add(item);

                if (InPublications != null) foreach (IPublicationEntity item in InPublications) rdo.PublicationEntities.Add(item);

                return rdo;
            }
        }

        private void loadJobTitleSectionObjects() 
        {
            AllJTSOs.Clear();
            var listOfJobs = experienceRepos.GetAll();
            foreach (var job in listOfJobs)
            {
                JobTitleSelectionObject jtso = new JobTitleSelectionObject();
                jtso.Company = job.Employer;
                foreach (var item in job.Titles) jtso.JobTitles.Add(item);
                AllJTSOs.Add(jtso);
                jtso.PropertyChanged += Jtso_PropertyChanged;
            }
        }

        private void Jtso_PropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            notifyPropertyChanged(e.PropertyName);
        }

        private void updateDocumentPreview() 
        {
            try
            {
                using (var ioc = new VitaeNinjectKernel())
                {
                    IResumeDataObject rdo = createRdo();
                    IResumeFormatObject rfo = ioc.Get<IResumeFormatObject>();

                    var blks = ResumePreview.Blocks;
                    blks.Clear();

                    IFullNameSection fns = ioc.Get<IFullNameSection>();
                    fns.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    IBasicInfoSection bis = ioc.Get<IBasicInfoSection>();
                    bis.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    ITagLineSection tls = ioc.Get<ITagLineSection>();
                    tls.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    IExpertiseSection es = ioc.Get<IExpertiseSection>();
                    es.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    IExperienceSection xs = ioc.Get<IExperienceSection>();
                    xs.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    IEducationSection eds = ioc.Get<IEducationSection>();
                    eds.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                    IPublicationsSection pubs = ioc.Get<IPublicationsSection>();
                    pubs.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();
                }
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            updateDocumentPreview();
        }

        private void setUpRelayCommands() 
        {
            AddExpertiseCommand = new RelayCommand(T => addExpertise(), T => SelectedOutExpertise != null);
            RemoveExpertiseCommand = new RelayCommand(T => removeExpertise(), T => SelectedInExpertise != null);
            MoveExpertiseUpCommand = new RelayCommand(
                T => moveExpertiseUp(), 
                T => SelectedInExpertise != null && InExpertises.IndexOf(SelectedInExpertise) > 0);
            MoveExpertiseDownCommand = new RelayCommand(
                T => moveExpertiseDown(), 
                T => SelectedInExpertise != null && InExpertises.IndexOf(SelectedInExpertise) < InExpertises.Count - 1);

            AddExperienceCommand = new RelayCommand(T => addExperienceItem(), T => SelectedOutExperience != null);
            RemoveExperienceCommand = new RelayCommand(T => removeExperienceItem(), T => SelectedInExperience != null);
            MoveExperienceUpCommand = new RelayCommand(
                T => moveExperienceItemUp(), 
                T => SelectedInExperience != null && InExperiences.IndexOf(SelectedInExperience) > 0);
            MoveExperienceDownCommand = new RelayCommand(
                T => moveExperienceItemDown(),
                T => SelectedInExperience != null && InExperiences.IndexOf(SelectedInExperience) < InExperiences.Count - 1);

            AddEducationCommand = new RelayCommand(T => addEducation(), T => SelectedOutEducation != null);
            RemoveEducationCommand = new RelayCommand(T => removeEducation(), T => SelectedInEducation != null);
            MoveEducationUpCommand = new RelayCommand(
                T => moveEducationUp(),
                T => SelectedInEducation != null && InEducations.IndexOf(SelectedInEducation) > 0);
            MoveEducationDownCommand = new RelayCommand(
                T => moveEducationDown(),
                T => SelectedInEducation != null && InEducations.IndexOf(SelectedInEducation) < InEducations.Count - 1);

            AddPublicationCommand = new RelayCommand(T => addPublication(), T => SelectedOutPublication != null);
            RemovePublicationCommand = new RelayCommand(T => removePublication(), T => SelectedInPublication != null);
            MovePublicationUpCommand = new RelayCommand(
                T => movePublicationUp(),
                T => SelectedInPublication != null && InPublications.IndexOf(SelectedInPublication) > 0);
            MovePublicationDownCommand = new RelayCommand(
                T => movePublicationDown(),
                T => SelectedInPublication != null && InPublications.IndexOf(SelectedInPublication) < InPublications.Count - 1);
        }

    }

    public class JobTitleSelectionObject : INotifyPropertyChanged
    {
        private string selectedJobTitle;

        public string Company { get; set; }
        public ObservableCollection<string> JobTitles { get; set; }
        public string SelectedJobTitle 
        {
            get { return selectedJobTitle; }
            set
            {
                selectedJobTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedJobTitle"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public JobTitleSelectionObject() 
        {
            JobTitles = new ObservableCollection<string>();
        }
    }

}