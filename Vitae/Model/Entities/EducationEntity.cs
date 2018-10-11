namespace Vitae.Model
{
    using System;

    public class EducationEntity : IEducationEntity
    {
        public Guid ID { get; set; }
        public string Institution { get; set; }
        public string Credential { get; set; }
    }
}