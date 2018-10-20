namespace Vitae.Services
{
    using Microsoft.Office.Interop.Word;
    using System.Xml.Linq;
    using Vitae.Model;

    public interface IPersistenceService 
    {
        void Persist(Document document, string filePath, DocumentPersistenceFileType type);

        void Persist<T>(XDocument document) where T : class, IEntity;

        XDocument Load<T>() where T : class, IEntity;
    }
}