using System.Data;
using System.Data.SqlClient;

namespace CRUD3.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string Connectionstring;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Connectionstring = _configuration.GetConnectionString("sqlConnection");
        }
        public IDbConnection CreateConnection()
            =>new SqlConnection(Connectionstring);
    }
}
