using System.Collections.Generic;

namespace Vitae.Model
{
    public interface IExperienceEntity
    {
        string Employer { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }
        IList<string> Titles { get; set; }
        IList<string> Details { get; set; }
    }
}