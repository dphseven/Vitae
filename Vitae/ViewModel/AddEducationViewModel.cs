namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddEducationViewModel : ViewModelBase, IAddEducationViewModel
    {
        private readonly IKernel _kernel;
        private IEducationRepository repos;

        private string institution;
        private string credential;

        public string Institution 
        {
            get { return institution; }
            set
            {
                institution = value;
                NotifyPropertyChanged();
            }
        }
        public string Credential 
        {
            get { return credential; }
            set
            {
                credential = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand AddCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler EducationAdded;

        public AddEducationViewModel(IEducationRepository repository, IKernel kernel) 
        {
            _kernel = kernel;
            repos = repository;

            SetUpRelayCommands();
        }

        private void AddEducation() 
        {
            var ent = _kernel.Get<IEducationEntity>();
            ent.Institution = Institution;
            ent.Credential = Credential;
            repos.Add(ent);

            EducationAdded?.Invoke(this, new EventArgs());
        }

        private void Reset() 
        {
            repos = null;
            Institution = null;
            Credential = null;
        }

        private void SetUpRelayCommands() 
        {
            AddCmd = new RelayCommand(
                T => { AddEducation(); Reset(); },
                T => true);
            CancelCmd = new RelayCommand(
                T => Reset(),
                T => true);
        }
    }
}