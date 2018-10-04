namespace Vitae.Services
{
    public class ExperienceItem : IExperienceItem
    {
        public string Employer { get; set; }
        public string ExperienceDetail { get; set; }

        public override string ToString() 
        {
            return ExperienceDetail;
        }
    }
}