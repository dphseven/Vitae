namespace Vitae.Model
{
    using System;

    public interface IGeneralInfoEntity : IEntity
    {
        string FullName { get; set; }
        string Add1 { get; set; }
        string Add2 { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
    }
}