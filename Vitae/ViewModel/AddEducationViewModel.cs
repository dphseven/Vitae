namespace Vitae.ViewModel
{
    using Ninject;
    using System;
    using System.Windows.Input;
    using Vitae.Model;

    public class AddEducationViewModel : ViewModelBase, IAddEducationViewModel
    {
        private IEducationRepository repos;

        private string institution;
        private string credential;

        public string Institution 
        {
            get { return institution; }
            set
            {
                institution = value;
                notifyPropertyChanged();
            }
        }
        public string Credential 
        {
            get { return credential; }
            set
            {
                credential = value;
                notifyPropertyChanged();
            }
        }

        public ICommand AddCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        public event EventHandler EducationAdded;

        public AddEducationViewModel(IEducationRepository repository) 
        {
            repos = repository;

            SetUpRelayCommands();
        }

        private void AddEducation() 
        {
            using (var ioc = new VitaeNinjectKernel())
            {
                var ent = ioc.Get<IEducationEntity>();
                ent.Institution = Institution;
                ent.Credential = Credential;
                repos.Add(ent);

                EducationAdded?.Invoke(this, new EventArgs());
            }
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