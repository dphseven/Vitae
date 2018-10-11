namespace Vitae.Model
{
    using System;

    public interface IPublicationEntity : IEntity
    {
        string Publication { get; set; }
    }
}