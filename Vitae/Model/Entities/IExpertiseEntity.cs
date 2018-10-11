namespace Vitae.Model
{
    using System;

    public interface IExpertiseEntity : IEntity
    {
        string Category { get; set; }
        string Expertise { get; set; }
    }
}