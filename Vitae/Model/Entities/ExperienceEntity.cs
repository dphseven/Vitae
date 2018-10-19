namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;

    public class ExperienceEntity : IExperienceEntity
    {
        public Guid ID { get; set; }
        public string Employer { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IList<string> Titles { get; set; }
        public IList<string> Details { get; set; }

        public ExperienceEntity() 
        {
            Employer = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            Titles = new List<string>();
            Details = new List<string>();
        }
    }
}