﻿namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddExpertiseViewModel : ViewModelBase, IAddExpertiseViewModel
    {
        private readonly IKernel _kernel;
        private IExpertiseRepository repos;

        private UIState formState;
        private IExpertiseEntity entity;

        public UIState FormState 
         {
            get { return formState; }
            set
            {
                formState = value;
                NotifyPropertyChanged();
            }
        }
        public string Category 
        {
            get
            {
                if (entity != null) return entity.Category;
                else return string.Empty;
            }
            set
            {
                entity.Category = value;
                NotifyPropertyChanged();
            }
        }
        public string Expertise 
        {
            get
            {
                if (entity != null) return entity.Expertise;
                else return string.Empty;
            }
            set
            {
                entity.Expertise = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand AddButtonCmd { get; set; }
        public ICommand CancelButtonCmd { get; set; }

        public event EventHandler ExpertiseAdded;

		// Public Methods

        public AddExpertiseViewModel(IExpertiseRepository repository, IKernel kernel) 
        {
            _kernel = kernel;
            repos = repository;
            entity = _kernel.Get<IExpertiseEntity>();
            
            setUpRelayCommands();
        }

		// Private Methods

        private void addExpertiseToRepository() 
        {
            repos.Add(entity);

            ExpertiseAdded?.Invoke(this, new EventArgs());
        }

        private void reset() 
        {
            repos = null;
            entity = null;
		}

        private void setUpRelayCommands() 
        {
            AddButtonCmd = new RelayCommand(
                T => { addExpertiseToRepository(); reset(); },
                T => true);
            CancelButtonCmd = new RelayCommand(
                T => { reset(); },
                T => true);
        }

    }
}