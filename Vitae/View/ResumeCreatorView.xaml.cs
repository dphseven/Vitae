namespace Vitae.View
{
    using Microsoft.Win32;
    using Ninject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using ViewModel;

    public partial class ResumeCreatorView : UserControl
    {
        IResumeCreatorViewModel vm;
        List<Expander> listOfExpanders = new List<Expander>();

        public ResumeCreatorView() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                this.DataContext = vm = ioc.Get<IResumeCreatorViewModel>();
                InitializeComponent();

                DocViewRTB.Document = vm.ResumePreview;

                listOfExpanders.Add(hGeneralInfo);
                listOfExpanders.Add(hExpertise);
                listOfExpanders.Add(hJobTitles);
                listOfExpanders.Add(hExperience);
                listOfExpanders.Add(hEducation);
                listOfExpanders.Add(hPublications);
            }
        }

        private void PublicationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddPublicationCommand.CanExecute(null)) vm.AddPublicationCommand.Execute(null);
        }

        private void SelectedPublicationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemovePublicationCommand.CanExecute(null)) vm.RemovePublicationCommand.Execute(null);
        }

        private void EducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddEducationCommand.CanExecute(null)) vm.AddEducationCommand.Execute(null);
        }

        private void SelectedEducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveEducationCommand.CanExecute(null)) vm.RemoveEducationCommand.Execute(null);
        }

        private void ExpertiseBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddExpertiseCommand.CanExecute(null)) vm.AddExpertiseCommand.Execute(null);
        }

        private void SelectedExpertiseBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveExpertiseCommand.CanExecute(null)) vm.RemoveExpertiseCommand.Execute(null);
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e) 
        {
            vm.UpdateExperienceLists();
        }

        private void closeAllOtherExpanders(object sender, RoutedEventArgs e) 
        {
            foreach (var item in listOfExpanders.Where(T => T != (Expander)sender))
                item.IsExpanded = false;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e) 
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Document(*.docx)|*.docx";
            if (sfd.ShowDialog() == true)
            {
                vm.ExportResumeToWord(sfd.FileName);
            }
        }

        private void ExportPdfButton_Click(object sender, RoutedEventArgs e) 
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Document(*.pdf)|*.pdf";
            if (sfd.ShowDialog() == true)
            {
                vm.ExportResumeToPdf(sfd.FileName);
            }
        }

        private void SelectedExperienceBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveExperienceCommand.CanExecute(null)) vm.RemoveExperienceCommand.Execute(null);
        }

        private void ExperienceBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddExperienceCommand.CanExecute(null)) vm.AddExperienceCommand.Execute(null);
        }
        
    }
}