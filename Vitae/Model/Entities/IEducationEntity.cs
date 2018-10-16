namespace Vitae.Model
{
    public interface IEducationEntity : IEntity
    {
        string Institution { get; set; }
        string Credential { get; set; }
    }
}