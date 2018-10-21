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
    using System.Windows;
    using System.Windows.Media;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Windows.Controls;

    public class ResumeCreatorViewModel : ViewModelBase, IResumeCreatorViewModel
    {
        // Dependencies
        private IResumeCreationService rcs;
        private IGeneralInfoRepository giRepos;
        private IExperienceRepository experienceRepos;
        private IExpertiseRepository expertiseRepos;
        private IEducationRepository edRepos;
        private IPublicationsRepository pubRepos;
        private ILoggingService ls;
        private readonly IKernel _kernel;

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
                NotifyPropertyChanged();
            }
        }
        public string AddLine1 
        {
            get { return addLine1; }
            set
            {
                addLine1 = value;
                NotifyPropertyChanged();
            }
        }
        public string AddLine2 
        {
            get { return addLine2; }
            set
            {
                addLine2 = value;
                NotifyPropertyChanged();
            }
        }
        public string Phone 
        {
            get { return phone; }
            set
            {
                phone = value;
                NotifyPropertyChanged();
            }
        }
        public string Email 
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged();
            }
        }
        public string TagLine 
        {
            get { return tagline; }
            set
            {
                tagline = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }
        private IExpertiseEntity selectedInExpertise;
        public IExpertiseEntity SelectedInExpertise 
        {
            get { return selectedInExpertise; }
            set
            {
                selectedInExpertise = value;
                NotifyPropertyChanged();
            }
        }

        // Jobs Properties
        public ObservableCollection<IDecoratedExperienceEntity> AllJobs { get; } 
            = new ObservableCollection<IDecoratedExperienceEntity>();
        private IDecoratedExperienceEntity selectedJob;
        public IDecoratedExperienceEntity SelectedJob 
        {
            get { return selectedJob; }
            set
            {
                selectedJob = value;
                NotifyPropertyChanged();
            }
        }

        // Experience Properties
        public string SelectedInExperience { get; set; }
        public string SelectedOutExperience { get; set; }

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
                NotifyPropertyChanged();
            }
        }
        private IEducationEntity selectedInEducation;
        public IEducationEntity SelectedInEducation 
        {
            get { return selectedInEducation; }
            set
            {
                selectedInEducation = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }
        private IPublicationEntity selectedInPublication;
        public IPublicationEntity SelectedInPublication 
        {
            get { return selectedInPublication; }
            set
            {
                selectedInPublication = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }

        // Commands
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

        public ICommand SearchCmd { get; set; }

        /*************************
        **************************
        ***** PUBLIC METHODS *****
        **************************
        *************************/

        public ResumeCreatorViewModel(
            IResumeCreationService rcs,
            ILoggingService ls,
            IGeneralInfoRepository giRepository,
            IExperienceRepository experienceRepository,
            IExpertiseRepository expertiseRepository,
            IEducationRepository educationRepository,
            IPublicationsRepository publicationsRepository,
            IKernel kernel) 
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
                this._kernel = kernel;

                SetUpRelayCommands();

                var gie = giRepos.Get(Guid.Empty);
                FullName = gie.FullName;
                Email = gie.Email;
                Phone = gie.Phone;
                AddLine1 = gie.Add1;
                AddLine2 = gie.Add2;

                InExpertises = new ObservableCollection<IExpertiseEntity>();
                OutExpertises = AllExpertises = new ObservableCollection<IExpertiseEntity>(expertiseRepos.GetAll());

                RefreshJobs();

                InEducations = new ObservableCollection<IEducationEntity>();
                OutEducations = AllEducations = new ObservableCollection<IEducationEntity>(edRepos.GetAll());

                InPublications = new ObservableCollection<IPublicationEntity>();
                OutPublications = AllPublications = new ObservableCollection<IPublicationEntity>(pubRepos.GetAll());

                UpdateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        public void DeleteJob(Guid jobId) 
        {
            if (experienceRepos.Get(jobId) != null)
            {
                experienceRepos.Remove(jobId);
                RefreshJobs();
            }
        }

        public void ExportResumeToPdf(string filePathAndName) 
        {
            try
            {
                var rdo = CreateRdo();
                var rfo = _kernel.Get<IResumeFormatObject>();
                var rso = _kernel.Get<IResumeStructureObject>();

                rso.AddSection(_kernel.Get<IFullNameSection>());
                rso.AddSection(_kernel.Get<IBasicInfoSection>());
                rso.AddSection(_kernel.Get<ITagLineSection>());
                rso.AddSection(_kernel.Get<IExpertiseSection>());
                rso.AddSection(_kernel.Get<IExperienceSection>());
                rso.AddSection(_kernel.Get<IEducationSection>());
                rso.AddSection(_kernel.Get<IPublicationsSection>());

                rcs.CreateResumeAsPdfFile(rdo, rfo, rso, filePathAndName);
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
                var rdo = CreateRdo();
                var rfo = _kernel.Get<IResumeFormatObject>();
                var rso = _kernel.Get<IResumeStructureObject>();

                rso.AddSection(_kernel.Get<IFullNameSection>());
                rso.AddSection(_kernel.Get<IBasicInfoSection>());
                rso.AddSection(_kernel.Get<ITagLineSection>());
                rso.AddSection(_kernel.Get<IExpertiseSection>());
                rso.AddSection(_kernel.Get<IExperienceSection>());
                rso.AddSection(_kernel.Get<IEducationSection>());
                rso.AddSection(_kernel.Get<IPublicationsSection>());

                rcs.CreateResumeAsWordFile(rdo, rfo, rso, filePathAndName);
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        public void RefreshExpertises() 
        {
            OutExpertises = new ObservableCollection<IExpertiseEntity>(
                expertiseRepos.GetAll().OrderBy(T => T.Category).ThenBy(T => T.Expertise));
            foreach (var item in InExpertises)
            {
                if (OutExpertises.Contains(item)) OutExpertises.Remove(item);
            }
            NotifyPropertyChanged(nameof(OutExpertises));
        }

        public void RefreshJobs() 
        {
            if (AllJobs == null) return;
            AllJobs.Clear();
            var list = experienceRepos.GetAllDecorators();
            foreach (var job in list)
            {
                AllJobs.Add(job);
                job.PropertyChanged += Job_PropertyChanged;
            }
        }

        public void RefreshEducationList() 
        {
            var oldInList = new List<IEducationEntity>(InEducations);
            var oldOutList = new List<IEducationEntity>(OutEducations);
            var newList = edRepos.GetAll();
            OutEducations.Clear();
            InEducations.Clear();
            foreach (var item in newList)
            {
                if (oldInList.Any(T => T.ID == item.ID)) InEducations.Add(item);
                else OutEducations.Add(item);
            }
        }

        public void RefreshPublicationsList() 
        {
            var oldInList = new List<IPublicationEntity>(InPublications);
            var oldOutList = new List<IPublicationEntity>(OutPublications);
            var newList = pubRepos.GetAll();
            OutPublications.Clear();
            InPublications.Clear();
            foreach (var item in newList)
            {
                if (oldInList.Any(T => T.ID == item.ID)) InPublications.Add(item);
                else OutPublications.Add(item);
            }
        }

        public void RefreshExperienceLists() 
        {

        }

        /**************************
        ***************************
        ***** PRIVATE METHODS *****
        ***************************
        **************************/

        private void AddExpertiseToResume() 
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
        private void RemoveExpertiseFromResume() 
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
        private void MoveExpertiseUpInResume() 
        {
            try
            {
                if (SelectedInExpertise == null) return;

                int currentIndex = InExpertises.IndexOf(SelectedInExpertise);
                InExpertises.Move(currentIndex, currentIndex - 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void MoveExpertiseDownInResume() 
        {
            try
            {
                if (SelectedInExpertise == null) return;

                int currentIndex = InExpertises.IndexOf(SelectedInExpertise);
                InExpertises.Move(currentIndex, currentIndex + 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void AddExperienceItemToResume() 
        {
            try
            {
                if (SelectedOutExperience == null || SelectedJob == null) return;
                SelectedJob.SelectedDetails.Add(SelectedOutExperience);
                if (SelectedJob.Details.Contains(SelectedOutExperience)) SelectedJob.Details.Remove(SelectedOutExperience);
                UpdateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void RemoveExperienceItemFromResume() 
        {
            try
            {
                if (SelectedInExperience == null || SelectedJob == null) return;
                SelectedJob.Details.Add(SelectedInExperience);
                SelectedJob.SelectedDetails.Remove(SelectedInExperience);
                UpdateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void MoveExperienceItemUpInResume() 
        {
            try
            {
                if (SelectedJob == null || SelectedInExperience == null) return;
                int currentIndex = SelectedJob.SelectedDetails.IndexOf(SelectedInExperience);
                SelectedJob.SelectedDetails.Move(currentIndex, currentIndex - 1);
                UpdateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void MoveExperienceItemDownInResume() 
        {
            try
            {
                if (SelectedInExperience == null) return;
                int currentIndex = SelectedJob.SelectedDetails.IndexOf(SelectedInExperience);
                SelectedJob.SelectedDetails.Move(currentIndex, currentIndex + 1);
                UpdateDocumentPreview();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void AddEducationToResume() 
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
        private void RemoveEducationFromResume() 
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
        private void MoveEducationUpInResume() 
        {
            try
            {
                if (SelectedInEducation == null) return;

                int currentIndex = InEducations.IndexOf(SelectedInEducation);
                InEducations.Move(currentIndex, currentIndex - 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void MoveEducationDownInResume() 
        {
            try
            {
                if (SelectedInEducation == null) return;

                int currentIndex = InEducations.IndexOf(SelectedInEducation);
                InEducations.Move(currentIndex, currentIndex + 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private void AddPublicationToResume() 
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
        private void RemovePublicationFromResume() 
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
        private void MovePublicationUpInResume() 
        {
            if (SelectedInPublication == null) return;
            try
            {
                int currentIndex = InPublications.IndexOf(SelectedInPublication);
                InPublications.Move(currentIndex, currentIndex - 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }
        private void MovePublicationDownInResume() 
        {
            if (SelectedInPublication == null) return;
            try
            {
                int currentIndex = InPublications.IndexOf(SelectedInPublication);
                InPublications.Move(currentIndex, currentIndex + 1);
                NotifyPropertyChanged();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }

        private IResumeDataObject CreateRdo() 
        {
            IResumeDataObject rdo = _kernel.Get<IResumeDataObject>();
            rdo.FullName = this.FullName;
            rdo.PhoneNumber = this.Phone;
            rdo.Email = this.Email;
            rdo.AddressLine1 = this.AddLine1;
            rdo.AddressLine2 = this.AddLine2;
            rdo.TagLine = this.TagLine;

            if (InExpertises != null) foreach (IExpertiseEntity item in InExpertises) rdo.ExpertiseEntities.Add(item);

            foreach (var job in AllJobs)
            {
                IExperienceEntity ee = _kernel.Get<IExperienceEntity>();
                ee.Employer = job.Employer;
                ee.StartDate = job.StartDate;
                ee.EndDate = job.EndDate;
                ee.Titles.Add(job.SelectedJobTitle);
                foreach (var detail in job.SelectedDetails) ee.Details.Add(detail);
                rdo.ExperienceEntities.Add(ee);
            }

            if (InEducations != null) foreach (IEducationEntity item in InEducations) rdo.EducationEntities.Add(item);

            if (InPublications != null) foreach (IPublicationEntity item in InPublications) rdo.PublicationEntities.Add(item);

            return rdo;
        }

        private void Job_PropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private void UpdateDocumentPreview() 
        {
            try
            {
                IResumeDataObject rdo = CreateRdo();
                IResumeFormatObject rfo = _kernel.Get<IResumeFormatObject>();

                var blks = ResumePreview.Blocks;
                blks.Clear();

                IFullNameSection fns = _kernel.Get<IFullNameSection>();
                fns.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                IBasicInfoSection bis = _kernel.Get<IBasicInfoSection>();
                bis.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                ITagLineSection tls = _kernel.Get<ITagLineSection>();
                tls.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                IExpertiseSection es = _kernel.Get<IExpertiseSection>();
                es.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                IExperienceSection xs = _kernel.Get<IExperienceSection>();
                xs.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                IEducationSection eds = _kernel.Get<IEducationSection>();
                eds.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();

                IPublicationsSection pubs = _kernel.Get<IPublicationsSection>();
                pubs.AddToFlowDocument(rdo, rfo, ResumePreview).DynamicInvoke();
            }
            catch (Exception e)
            {
                ls.Log(e, "Exception");
            }
        }



        // SKETCHY, HACKY DOCUMENT SEARCH METHODS FOLLOW:

        private void SearchDocumentPreview(string searchText) 
        {
            HighlightWords(
                ResumePreview.ContentStart, 
                searchText,
                new TextRange(ResumePreview.ContentEnd, ResumePreview.ContentStart).Text);
        }

        private void HighlightWords(TextPointer startOfText, string searchTerm, string fullText) 
        {
            TextRange fullDoc = new TextRange(startOfText.DocumentStart, startOfText.DocumentEnd);
            fullDoc.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.White));

            int countMatches = Regex.Matches(fullText.ToLower(), searchTerm.ToLower()).Count;

            for (int i = 0; i < countMatches; i++)
            {
                int lastInstanceIndex = HighlightNextInstance(startOfText, searchTerm);
                if (lastInstanceIndex == -1)
                {
                    break;
                }
                startOfText = startOfText.GetPositionAtOffset(lastInstanceIndex);
            }
        }

        private int HighlightNextInstance(TextPointer text, string searchWord) 
        {
            int indexOfLastInstance = -1;

            while (true)
            {
                TextPointer next = text.GetNextContextPosition(LogicalDirection.Forward);
                if (next == null)
                {
                    break;
                }
                TextRange newText = new TextRange(text, next);

                int index = newText.Text.ToLower().IndexOf(searchWord.ToLower());
                if (index != -1)
                {
                    indexOfLastInstance = index;
                }

                if (index >= 0)
                {
                    TextPointer start = text.GetPositionAtOffset(index);
                    TextPointer end = text.GetPositionAtOffset(index + searchWord.Length);

                    if (start.Paragraph != null &&
                        start.Paragraph.Parent.GetType() == typeof(ListItem) &&
                        new TextRange(start, end).Text.ToLower() != searchWord.ToLower())
                    {
                        start = text.GetPositionAtOffset(index - 2);
                        end = text.GetPositionAtOffset(index - 2 + searchWord.Length);
                    }
                                                                             
                    TextRange textRange = new TextRange(start, end);
                    textRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Goldenrod));
                }
                text = next;
            }

            return indexOfLastInstance;
        }

        // SKETCHY, HACKY DOCUMENT SEARCH METHODS ABOVE



        private void SetUpRelayCommands() 
        {
            AddExpertiseCommand = new RelayCommand(T => AddExpertiseToResume(), T => SelectedOutExpertise != null);
            RemoveExpertiseCommand = new RelayCommand(T => RemoveExpertiseFromResume(), T => SelectedInExpertise != null);
            MoveExpertiseUpCommand = new RelayCommand(
                T => MoveExpertiseUpInResume(),
                T => SelectedInExpertise != null && InExpertises.IndexOf(SelectedInExpertise) > 0);
            MoveExpertiseDownCommand = new RelayCommand(
                T => MoveExpertiseDownInResume(),
                T => SelectedInExpertise != null && InExpertises.IndexOf(SelectedInExpertise) < InExpertises.Count - 1);
            AddExperienceCommand = new RelayCommand(T => AddExperienceItemToResume(), T => SelectedOutExperience != null);
            RemoveExperienceCommand = new RelayCommand(T => RemoveExperienceItemFromResume(), T => SelectedInExperience != null);
            MoveExperienceUpCommand = new RelayCommand(
                T => MoveExperienceItemUpInResume(),
                T => SelectedInExperience != null && 
                     SelectedJob.SelectedDetails.IndexOf(SelectedInExperience) > 0);
            MoveExperienceDownCommand = new RelayCommand(
                T => MoveExperienceItemDownInResume(),
                T => SelectedInExperience != null &&
                     SelectedJob.SelectedDetails.IndexOf(SelectedInExperience) < SelectedJob.SelectedDetails.Count - 1);
            AddEducationCommand = new RelayCommand(T => AddEducationToResume(), T => SelectedOutEducation != null);
            RemoveEducationCommand = new RelayCommand(T => RemoveEducationFromResume(), T => SelectedInEducation != null);
            MoveEducationUpCommand = new RelayCommand(
                T => MoveEducationUpInResume(),
                T => SelectedInEducation != null && InEducations.IndexOf(SelectedInEducation) > 0);
            MoveEducationDownCommand = new RelayCommand(
                T => MoveEducationDownInResume(),
                T => SelectedInEducation != null && InEducations.IndexOf(SelectedInEducation) < InEducations.Count - 1);
            AddPublicationCommand = new RelayCommand(T => AddPublicationToResume(), T => SelectedOutPublication != null);
            RemovePublicationCommand = new RelayCommand(T => RemovePublicationFromResume(), T => SelectedInPublication != null);
            MovePublicationUpCommand = new RelayCommand(
                T => MovePublicationUpInResume(),
                T => SelectedInPublication != null && InPublications.IndexOf(SelectedInPublication) > 0);
            MovePublicationDownCommand = new RelayCommand(
                T => MovePublicationDownInResume(),
                T => SelectedInPublication != null && InPublications.IndexOf(SelectedInPublication) < InPublications.Count - 1);



            SearchCmd = new RelayCommand(
                T => SearchDocumentPreview((string)T),
                T => true);
        }

        protected override void NotifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            UpdateDocumentPreview();
            base.NotifyPropertyChanged(propertyName);
        }
    }
}