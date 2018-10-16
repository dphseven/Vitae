﻿namespace Vitae.ViewModel
{
    using Ninject;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddPublicationViewModel : ViewModelBase, IAddPublicationViewModel
    {
        private IPublicationsRepository repos;

        private string publication;

        public string Publication 
        {
            get { return publication; }
            set
            {
                publication = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public AddPublicationViewModel(IPublicationsRepository repository) 
        {
            repos = repository;

            SetUpRelayCommands();
        }

        private void AddPublication() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ent = ioc.Get<IPublicationEntity>();
                ent.Publication = Publication;
                repos.Add(ent);
            }            
        }

        private void Reset() 
        {
            repos = null;
            publication = null;
        }

        private void SetUpRelayCommands() 
        {
            AddCmd = new RelayCommand(
                T => { AddPublication(); Reset(); },
                T => !string.IsNullOrWhiteSpace(Publication) );
            CancelCmd = new RelayCommand(
                T => { Reset(); },
                T => true);
        }
    }
}