﻿namespace Vitae.View
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
        IResumeCreatorViewModel vm;
        List<Expander> listOfExpanders = new List<Expander>();

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

        private void closeAllOtherExpanders(object sender, RoutedEventArgs e) 
        {
            foreach (var item in listOfExpanders.Where(T => T != (Expander)sender))
                item.IsExpanded = false;
        }

        // General Info Area

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
                var exEntity = new ConstructorArgument("entity", (object)null);
                var emVM = ioc.Get<IExpertiseManagementViewModel>(exRepos, exEntity);

                var emView = new ExpertiseManagementView(emVM);
                emView.IsVisibleChanged += sortOutExpertises;

                ucHost.Content = emView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditExpertiseButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var emVM = ioc.Get<IExpertiseManagementViewModel>();
                emVM.InjectExpertiseEntity(vm.SelectedOutExpertise);
                var emView = new ExpertiseManagementView(emVM);

                emView.IsVisibleChanged += sortOutExpertises;

                ucHost.Content = emView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void sortOutExpertises(object sender, DependencyPropertyChangedEventArgs e) 
        {
            if (ucHost.Content is UserControl)
            {
                var uc = (UserControl)ucHost.Content;
                if (!uc.IsVisible) vm.SortOutExpertises();
            }
        }

        // Job Title Area

        private void AddJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ajtVM = ioc.Get<IAddJobTitleViewModel>();
                ajtVM.FormState = UIState.View;

                var ajtView = new AddJobTitleView(ajtVM);
                ajtView.IsVisibleChanged += refreshJobTitles;

                ucHost.Content = ajtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

        private void EditJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ejtVM = ioc.Get<IEditJobTitleViewModel>();
                ejtVM.FormState = UIState.View;

                var ejtView = new EditJobTitleView(ejtVM);
                ejtView.IsVisibleChanged += refreshJobTitles;

                ucHost.Content = ejtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }



        private void refreshJobTitles(object sender, DependencyPropertyChangedEventArgs e) 
        {
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

        // Education Area

        private void EducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.AddEducationCommand.CanExecute(null)) vm.AddEducationCommand.Execute(null);
        }

        private void SelectedEducationBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            if (vm.RemoveEducationCommand.CanExecute(null)) vm.RemoveEducationCommand.Execute(null);
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

        // Following are all in progress

        private void DeleteJobTitleButton_Click(object sender, RoutedEventArgs e) 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var djtVM = ioc.Get<IDeleteJobTitleViewModel>();
                djtVM.FormState = UIState.View;

                var djtView = new DeleteJobTitleView(djtVM);
                djtView.IsVisibleChanged += refreshJobTitles;

                ucHost.Content = djtView;
                ucHost.Visibility = Visibility.Visible;
            }
        }

    }
}