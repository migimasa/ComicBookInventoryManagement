using System.Data.SqlClient;
using ComicBookInventory.Data.Abstract;

namespace ComicBookInventory.Data.Access
{
    public class BaseAccess
    {
        private IAccess _access;

        public BaseAccess(IAccess access)
        {
            _access = access;
        }

        public string ConnectionString { get { return _access.ConnectionString; } }
        public SqlConnection GetOpenConnection() { return _access.GetOpenConnection();  }
    }
}
