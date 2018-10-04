namespace Vitae.Services
{
    public interface IExperienceItem
    {
        string Employer { get; set; }
        string ExperienceDetail { get; set; }
        string ToString();
    }
}