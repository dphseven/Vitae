namespace Vitae.Model
{
    using System;

    public interface IEducationEntity : IEntity
    {
        string Institution { get; set; }
        string Credential { get; set; }
    }
}