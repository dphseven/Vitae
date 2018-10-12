using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using Vitae.Model;
using Vitae.Services;

namespace Vitae.ViewModel
{
    public interface IResumeCreatorViewModel
    {
        

        string FullName { get; set; }
        string AddLine1 { get; set; }
        string AddLine2 { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string TagLine { get; set; }

        ObservableCollection<IExpertiseEntity> AllExpertises { get; set; }
        ObservableCollection<IExpertiseEntity> InExpertises { get; set; }
        ObservableCollection<IExpertiseEntity> OutExpertises { get; set; }
        IExpertiseEntity SelectedOutExpertise { get; set; }
        IExpertiseEntity SelectedInExpertise { get; set; }

        ObservableCollection<string> AllEmployers { get; set; }
        string SelectedEmployer { get; set; }
        ObservableCollection<IExperienceItem> AllExperiences { get; set; }
        ObservableCollection<IExperienceItem> OutExperiences { get; set; }
        ObservableCollection<IExperienceItem> InExperiences { get; set; }
        ObservableCollection<IExperienceItem> AllInExperiences { get; set; }
        IExperienceItem SelectedInExperience { get; set; }
        IExperienceItem SelectedOutExperience { get; set; }

        ObservableCollection<IEducationEntity> AllEducations { get; set; }
        ObservableCollection<IEducationEntity> InEducations { get; set; }
        ObservableCollection<IEducationEntity> OutEducations { get; set; }
        IEducationEntity SelectedOutEducation { get; set; }
        IEducationEntity SelectedInEducation { get; set; }

        ObservableCollection<IPublicationEntity> AllPublications { get; set; }
        ObservableCollection<IPublicationEntity> InPublications { get; set; }
        ObservableCollection<IPublicationEntity> OutPublications { get; set; }
        IPublicationEntity SelectedOutPublication { get; set; }
        IPublicationEntity SelectedInPublication { get; set; }

        ObservableCollection<JobTitleSelectionObject> AllJTSOs { get; set; }

        FlowDocument ResumePreview { get; set; }

        ICommand AddExpertiseCommand { get; set; }
        ICommand RemoveExpertiseCommand { get; set; }
        ICommand MoveExpertiseUpCommand { get; set; }
        ICommand MoveExpertiseDownCommand { get; set; }

        ICommand AddExperienceCommand { get; set; }
        ICommand RemoveExperienceCommand { get; set; }
        ICommand MoveExperienceUpCommand { get; set; }
        ICommand MoveExperienceDownCommand { get; set; }

        ICommand AddEducationCommand { get; set; }
        ICommand RemoveEducationCommand { get; set; }
        ICommand MoveEducationUpCommand { get; set; }
        ICommand MoveEducationDownCommand { get; set; }

        ICommand AddPublicationCommand { get; set; }
        ICommand RemovePublicationCommand { get; set; }
        ICommand MovePublicationUpCommand { get; set; }
        ICommand MovePublicationDownCommand { get; set; }

        void ExportResumeToWord(string filePathAndName);
        void ExportResumeToPdf(string filePathAndName);
        void LoadJobTitles();
        void SortOutExpertises();
        void UpdateExperienceLists();

    }
}