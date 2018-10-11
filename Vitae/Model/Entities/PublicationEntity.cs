namespace Vitae.Model
{
    using System;

    public class PublicationEntity : IPublicationEntity
    {
        public Guid ID { get; set; }
        public string Publication { get; set; }
    }
}