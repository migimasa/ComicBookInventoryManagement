using System.Data.SqlClient;

namespace ComicBookInventory.Data.Abstract
{
    public interface IAccess
    {
        string ConnectionString { get; }
        SqlConnection GetOpenConnection();
    }
}
