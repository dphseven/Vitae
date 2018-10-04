namespace Vitae.Model
{
    using System.Collections.ObjectModel;

    public class ResumeStructureObject : IResumeStructureObject
    {
        private ObservableCollection<IResumeSection> resumeSections = new ObservableCollection<IResumeSection>();
        public ReadOnlyCollection<IResumeSection> ResumeSections 
        {
            get { return new ReadOnlyCollection<IResumeSection>(resumeSections); }
        }

        public void AddSection(IResumeSection section) 
        {
            resumeSections.Add(section);
        }

        public void MoveSection(int oldIndex, int newIndex) 
        {
            resumeSections.Move(oldIndex, newIndex);
        }

        public void RemoveSection(int index) 
        {
            resumeSections.RemoveAt(index);
        }
    }
}