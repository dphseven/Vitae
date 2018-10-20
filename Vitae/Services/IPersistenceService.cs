namespace Vitae.Services
{
    using System.Xml.Linq;
    using Vitae.Model;

    public interface IPersistenceService 
    {
        void Persist<T>(XDocument document) where T : class, IEntity;
        XDocument Load<T>() where T : class, IEntity;
    }
}