using CRUD3.Context;
using CRUD3.Model;
using CRUD3.Repositories.Interfaces;
using Dapper;

namespace CRUD3.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly DapperContext context;
        public EmployeeRepository(DapperContext context)
        {
            this.context = context;
        }
       

    }
}
