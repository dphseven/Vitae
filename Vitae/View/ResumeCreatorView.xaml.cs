namespace Vitae.View
{
    using Microsoft.Win32;
    using Ninject;
    using Ninject.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using ViewModel;
    using Vitae.Model;

    public partial class ResumeCreatorView : UserControl
    {
        private IResumeCreatorViewModel vm;
        private IKernel kernel;

        private List<Expander> listOfExpanders = new List<Expander>();

        public ResumeCreatorView(IResumeCreatorViewModel viewModel, IKernel kernel) 
        {
            DataContext = vm = viewModel;

            InitializeComponent();

            this.kernel = kernel;

            DocViewRTB.Document = vm.ResumePreview;

            listOfExpanders.Add(hGeneralInfo);
            listOfExpanders.Add(hExpertise);
            listOfExpanders.Add(hJobs);
            listOfExpanders.Add(hExperience);
            listOfExpanders.Add(hEducation);
            listOfExpanders.Add(hPublications);
        }

        private void CloseAllOtherExpanders(object sender, RoutedEventArgs e) 
        {
            foreach (var item in listOfExpanders.Where(T => T != (Expander)sender))
                item.IsExpanded = false;
        }

        // Expertise Area

        private void ExpertiseBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddExpertiseCommand.CanExecute(null)) vm.AddExpertiseCommand.Execute(null);
        }

        private void SelectedExpertiseBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveExpertiseCommand.CanExecute(null)) vm.RemoveExpertiseCommand.Execute(null);
        }

        private void AddExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            var exRepos = new ConstructorArgument("repository", kernel.Get<IExpertiseRepository>());
            var aeVM = kernel.Get<IAddExpertiseViewModel>(exRepos);

            aeVM.ExpertiseAdded += ExpertiseDialog_Closing;

            ucHost.Content = new AddExpertiseView(aeVM);
            ucHost.Visibility = Visibility.Visible;
        }

        private void EditExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            var eeVM = kernel.Get<IEditExpertiseViewModel>();
            var eeView = new EditExpertiseView(eeVM);

            eeVM.ExpertiseEdited += ExpertiseDialog_Closing;

            ucHost.Content = eeView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void DeleteExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            var deVM = kernel.Get<IDeleteExpertiseViewModel>();
            var deView = new DeleteExpertiseView(deVM);

            deVM.ExpertiseDeleted += ExpertiseDialog_Closing;

            ucHost.Content = deView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void ExpertiseDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.RefreshExpertises();
        }

        // Jobs Area

        private void AddJobButton_Click(object sender, RoutedEventArgs e) 
        {
            var exRepos = new ConstructorArgument("repository", kernel.Get<IExperienceRepository>());
            var ajVM = kernel.Get<IAddJobViewModel>(exRepos);
            ajVM.JobAdded += JobDialog_Closing;

            ucHost.Content = new AddJobView(ajVM);
            ucHost.Visibility = Visibility.Visible;
        }
        
        private void EditJobButton_Click(object sender, RoutedEventArgs e) 
        {
            var reposArgument = new ConstructorArgument("repository", kernel.Get<IExperienceRepository>());

            var id = ((IExperienceEntity)JobsDataGrid.SelectedItem).ID;
            var idArgument = new ConstructorArgument("jobId", id);

            var ejVM = kernel.Get<IEditJobViewModel>(reposArgument, idArgument);
            ejVM.JobEdited += JobDialog_Closing;

            ucHost.Content = new EditJobView(ejVM);
            ucHost.Visibility = Visibility.Visible;
        }

        private void DeleteJobButton_Click(object sender, RoutedEventArgs e) 
        {
            var result = MessageBox.Show("Are you 100% sure that you wish to delete this job?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = ((IExperienceEntity)JobsDataGrid.SelectedItem).ID;
                vm.DeleteJob(id);
                vm.RefreshJobs();
            }
        }

        private void JobDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.RefreshJobs();
        }

        // Experience Area

        private void SelectedExperienceBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveExperienceCommand.CanExecute(null)) vm.RemoveExperienceCommand.Execute(null);
        }

        private void ExperienceBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddExperienceCommand.CanExecute(null)) vm.AddExperienceCommand.Execute(null);
        }

        private void UpdateExperienceLists(object sender, RoutedEventArgs e) 
        {
            vm.RefreshExperienceLists();
        }

        private void AddExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            var aeVM = kernel.Get<IAddExperienceViewModel>();
            aeVM.ExperienceAdded += ExperienceDialogClosing;
            var aeView = new AddExperienceView(aeVM);

            ucHost.Content = aeView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void EditExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            var eeVM = kernel.Get<IEditExperienceViewModel>();
            eeVM.ExperienceEdited += ExperienceDialogClosing;
            var eeView = new EditExperienceView(eeVM);

            ucHost.Content = eeView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void DeleteExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            var deVM = kernel.Get<IDeleteExperienceViewModel>();
            deVM.ExperienceDeleted += ExperienceDialogClosing;
            var deView = new DeleteExperienceView(deVM);

            ucHost.Content = deView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void ExperienceDialogClosing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.RefreshExperienceLists();
        }

        // Education Area

        private void EducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddEducationCommand.CanExecute(null)) vm.AddEducationCommand.Execute(null);
        }

        private void SelectedEducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveEducationCommand.CanExecute(null)) vm.RemoveEducationCommand.Execute(null);
        }

        private void AddEducationButton_Click(object sender, RoutedEventArgs e) 
        {
            var aeVM = kernel.Get<IAddEducationViewModel>();
            aeVM.EducationAdded += EducationDialog_Closing;
            var aeView = new AddEducationView(aeVM);

            ucHost.Content = aeView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void EditEducationButton_Click(object sender, RoutedEventArgs e) 
        {
            var eeVM = kernel.Get<IEditEducationViewModel>();
            eeVM.EducationEdited += EducationDialog_Closing;
            var eeView = new EditEducationView(eeVM);

            ucHost.Content = eeView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void DeleteEducationButton_Click(object sender, RoutedEventArgs e) 
        {
            var deVM = kernel.Get<IDeleteEducationViewModel>();
            deVM.EducationDeleted += EducationDialog_Closing;
            var deView = new DeleteEducationView(deVM);

            ucHost.Content = deView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void EducationDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.RefreshEducationList();
        }

        // Publications Area

        private void PublicationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddPublicationCommand.CanExecute(null)) vm.AddPublicationCommand.Execute(null);
        }

        private void SelectedPublicationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemovePublicationCommand.CanExecute(null)) vm.RemovePublicationCommand.Execute(null);
        }

        private void AddPublicationButton_Click(object sender, RoutedEventArgs e) 
        {
            var apVM = kernel.Get<IAddPublicationViewModel>();
            apVM.PublicationAdded += PublicationDialog_Closing;
            var apView = new AddPublicationView(apVM);

            ucHost.Content = apView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void EditPublicationButton_Click(object sender, RoutedEventArgs e) 
        {
            var epVM = kernel.Get<IEditPublicationViewModel>();
            epVM.PublicationEdited += PublicationDialog_Closing;
            var epView = new EditPublicationView(epVM);

            ucHost.Content = epView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void DeletePublicationButton_Click(object sender, RoutedEventArgs e) 
        {
            var dpVM = kernel.Get<IDeletePublicationViewModel>();
            dpVM.PublicationDeleted += PublicationDialog_Closing;
            var dpView = new DeletePublicationView(dpVM);

            ucHost.Content = dpView;
            ucHost.Visibility = Visibility.Visible;
        }

        private void PublicationDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.RefreshPublicationsList();
        }

        // Bottom Area

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

    }
}