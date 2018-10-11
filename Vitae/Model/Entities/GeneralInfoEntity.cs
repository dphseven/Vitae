namespace Vitae.Model
{
    using System;

    public class GeneralInfoEntity : IGeneralInfoEntity
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}