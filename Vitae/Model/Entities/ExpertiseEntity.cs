using System;

namespace Vitae.Model
{
    public class ExpertiseEntity : IExpertiseEntity
    {
        public Guid ID { get; set; }
        public string Category { get; set; }
        public string Expertise { get; set; }

        public ExpertiseEntity()
        {
            Category = string.Empty;
            Expertise = string.Empty;
        }

        public override string ToString()
        {
            return Category + ": " + Expertise;
        }
    }
}