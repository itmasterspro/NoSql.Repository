namespace ItMastersPro.NoSql.Repository.Interfaces
{
    /// <summary>
    /// A sign that the class is an entity
    /// </summary>
    public interface IEntity
    {
        object Id { get; set; }
    }
}
