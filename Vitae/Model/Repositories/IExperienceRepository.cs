using System.Collections.Generic;
using Vitae.Services;

namespace Vitae.Model
{
    public interface IExperienceRepository : IRepository<IExperienceEntity>
    {
        IList<IExperienceItem> GetAllExperienceItems();
    }
}