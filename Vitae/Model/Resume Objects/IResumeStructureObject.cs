using System.Collections.ObjectModel;

namespace Vitae.Model
{
    public interface IResumeStructureObject
    {
        ReadOnlyCollection<IResumeSection> ResumeSections { get; }
        void AddSection(IResumeSection section);
        void MoveSection(int oldIndex, int newIndex);
        void RemoveSection(int index);
    }
}