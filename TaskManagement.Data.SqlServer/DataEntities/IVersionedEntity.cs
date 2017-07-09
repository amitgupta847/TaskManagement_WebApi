
namespace TaskManagement.Data.SqlServer
{
    public interface IVersionedEntity
    {
        byte[] Version { get; set; }
    }
}
