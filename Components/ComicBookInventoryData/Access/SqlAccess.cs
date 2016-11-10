using System.Configuration;
using System.Data.SqlClient;
using ComicBookInventory.Data.Abstract;

namespace ComicBookInventory.Data.Access
{
    public class SqlAccess : IAccess
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ComicBookInventory_ConnectionString"].ConnectionString;
            }
        }

        public SqlConnection GetOpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            return connection;
        }
    }
}
