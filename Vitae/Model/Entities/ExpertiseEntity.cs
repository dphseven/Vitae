namespace Vitae.Model
{
    public class ExpertiseEntity : IExpertiseEntity
    {
        public string Category { get; set; }
        public string Expertise { get; set; }

        public override string ToString()
        {
            return Category + ": " + Expertise;
        }
    }
}