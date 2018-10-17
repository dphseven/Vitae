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

        private List<Expander> listOfExpanders = new List<Expander>();
        //private UserControl currentDialog;

        public ResumeCreatorView() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                DataContext = vm = ioc.Get<IResumeCreatorViewModel>();

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
            using (var ioc = new VitaeNinjectKernel())
            {
                var exRepos = new ConstructorArgument("repository", ioc.Get<IExpertiseRepository>());
                var aeVM = ioc.Get<IAddExpertiseViewModel>(exRepos);

                aeVM.ExpertiseAdded += ExpertiseDialog_Closing;

                ucHost.Content = new AddExpertiseView(aeVM);
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var eeVM = ioc.Get<IEditExpertiseViewModel>();
                var eeView = new EditExpertiseView(eeVM);

                eeVM.ExpertiseEdited += ExpertiseDialog_Closing;

                ucHost.Content = eeView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void DeleteExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var deVM = ioc.Get<IDeleteExpertiseViewModel>();
                var deView = new DeleteExpertiseView(deVM);

                deVM.ExpertiseDeleted += ExpertiseDialog_Closing;

                ucHost.Content = deView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void ExpertiseDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.SortOutExpertises();
        }

        // Job Title Area

        private void AddJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ajtVM = ioc.Get<IAddJobTitleViewModel>();
                ajtVM.JobTitleAdded += JobTitleDialog_Closing;
                ajtVM.FormState = UIState.View;
                var ajtView = new AddJobTitleView(ajtVM);

                ucHost.Content = ajtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ejtVM = ioc.Get<IEditJobTitleViewModel>();
                ejtVM.JobTitleEdited += JobTitleDialog_Closing;
                var ejtView = new EditJobTitleView(ejtVM);

                ucHost.Content = ejtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void DeleteJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var djtVM = ioc.Get<IDeleteJobTitleViewModel>();
                djtVM.JobTitleDeleted += JobTitleDialog_Closing;
                var djtView = new DeleteJobTitleView(djtVM);

                ucHost.Content = djtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void JobTitleDialog_Closing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.LoadJobTitles();
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
            vm.UpdateExperienceLists();
        }

        private void AddExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var aeVM = ioc.Get<IAddExperienceViewModel>();
                aeVM.ExperienceAdded += ExperienceDialogClosing;
                var aeView = new AddExperienceView(aeVM);

                ucHost.Content = aeView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var eeVM = ioc.Get<IEditExperienceViewModel>();
                eeVM.ExperienceEdited += ExperienceDialogClosing;
                var eeView = new EditExperienceView(eeVM);

                ucHost.Content = eeView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void DeleteExperienceButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel()) 
            {
                var deVM = ioc.Get<IDeleteExperienceViewModel>();
                deVM.ExperienceDeleted += ExperienceDialogClosing;
                var deView = new DeleteExperienceView(deVM);

                ucHost.Content = deView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void ExperienceDialogClosing(object sender, EventArgs e) 
        {
            ucHost.Visibility = Visibility.Collapsed;
            vm.UpdateExperienceLists();
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
            using (var ioc = new VitaeNinjectKernel())
            {
                var aeVM = ioc.Get<IAddEducationViewModel>();

                var aeView = new AddEducationView(aeVM);

                ucHost.Content = aeView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditEducationButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var eeVM = ioc.Get<IEditEducationViewModel>();

                var eeView = new EditEducationView(eeVM);

                ucHost.Content = eeView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void DeleteEducationButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var deVM = ioc.Get<IDeleteEducationViewModel>();

                var deView = new DeleteEducationView(deVM);

                ucHost.Content = deView;
                ucHost.Visibility = Visibility.Visible;
            }
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
            using (var ioc = new VitaeNinjectKernel())
            {
                var apVM = ioc.Get<IAddPublicationViewModel>();

                var apView = new AddPublicationView(apVM);

                ucHost.Content = apView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditPublicationButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var epVM = ioc.Get<IEditPublicationViewModel>();

                var epView = new EditPublicationView(epVM);

                ucHost.Content = epView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void DeletePublicationButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var dpVM = ioc.Get<IDeletePublicationViewModel>();

                var dpView = new DeletePublicationView(dpVM);

                ucHost.Content = dpView;
                ucHost.Visibility = Visibility.Visible;
            }
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