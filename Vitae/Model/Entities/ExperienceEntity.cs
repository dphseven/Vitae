namespace Vitae.Model
{
    using System.Collections.Generic;

    public class ExperienceEntity : IExperienceEntity
    {
        public string Employer { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IList<string> Titles { get; set; }
        public IList<string> Details { get; set; }

        public ExperienceEntity()
        {
            Titles = new List<string>();
            Details = new List<string>();
        }
    }
}