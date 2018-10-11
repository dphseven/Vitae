namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;

    public interface IExperienceEntity : IEntity
    {
        string Employer { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }
        IList<string> Titles { get; set; }
        IList<string> Details { get; set; }
    }
}