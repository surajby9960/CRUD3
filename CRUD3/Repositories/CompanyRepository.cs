using CRUD3.Context;
using CRUD3.Model;
using CRUD3.Repositories.Interfaces;
using Dapper;

namespace CRUD3.Repositories
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly DapperContext context;
        public CompanyRepository(DapperContext context)
        {
           this. context = context;
        }

        public async Task DeleteCompany(int id)
        {

            var qry = "delete from company where cid=@cid";
            using (var conn = context.CreateConnection())
            {
                await conn.ExecuteAsync(qry, new {cid=id});

                
            }
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
            {
            List<Company> companies = new List<Company>();  
            var qry = "select * from company";
            using (var conn = context.CreateConnection())
            {
                var company=await conn.QueryAsync<Company>(qry);
                companies=company.ToList();
                foreach(Company company2 in companies)
                {
                    var qry1 = "select * from Employee where cid=@cid";
                   var employee= await conn.QueryAsync<Employee>(qry1,new {cid=company2.cId});
                    company2.empList=employee.ToList();
                }
                return companies;
                
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            Company cmp = new Company();
            var qry = "select * from company where cid=@id";
            using (var conn=context.CreateConnection())
            {
                 cmp = await conn.QuerySingleAsync<Company>(qry, new { id });
               if(cmp!=null)
                {
                    var qry1 = "select * from employee where cid=@cid";
                    var employee=await conn.QueryAsync<Employee>(qry1 ,new {cid=cmp.cId});
                    cmp.empList=employee.ToList();

                }
               return cmp;
            }
        }

        public async Task<int> InsertCompany(Company company)
        {
            var qry = @"insert into company(cName,cAddress,cCountry)values(@cName,@cAddress,@cCountry);
                              SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var conn = context.CreateConnection())
            {
                var res = await conn.QueryFirstOrDefaultAsync<int>(qry,company);
                if (res != 0)
                {
                    await AddEmployee(company.empList, res);


                }
                return res;
            }
        }
           
        public async Task<int> AddEmployee(List<Employee> emp, int id)
              {
            int res1 = 0;
            if (emp.Count > 0)
            {
                foreach (Employee employee in emp)
                {
                   
                    employee.cId = id;
                    using (var conn = context.CreateConnection())
                    {
                        var qry = @"insert into employee(eName,eAddress,Salary,cId)values(@eName,@eAddress,@Salary,@cId)
                                SELECT CAST(SCOPE_IDENTITY() as int)";
                        var res = await conn.QueryFirstOrDefaultAsync<int>(qry, employee );
                        res1= res;
                        await AddProject(employee.plist, id, res);
                    }
                   
                }

            }
            return res1;
        }
        public async Task<int> AddProject(List<Project> projects,int cid,int eid)
        {
            if(projects.Count > 0)
            {
                foreach(Project project in projects)
                {
                    project.cId = cid;
                    project.eId = eid;
                    using(var conn = context.CreateConnection())
                    {
                        var qry = "insert into project(pname,cid,eid)values(@pname,@cid,@eid)";
                        var res=await conn.ExecuteAsync(qry, project);
                    }
                }
                
            }
            return 0;
        }

        public async Task<int> UpdateCompany(Company company,int id)
        {
            Company cmp=new Company();
            var qry = "update company set cName=@cName,cAddress=@cAddress,cCountry=@cCountry where cid=@id";
            using (var conn= context.CreateConnection())
            {
                var res = await conn.ExecuteAsync(qry, new { id });
               cmp= await GetCompanyById(id);
                await UpdateEmployee(cmp.empList, id);
                return res;
            }
        }
        public async Task<int> UpdateEmployee(List<Employee> emp,int cid)
        {
            var qry = "update employee set Salary=@salary where cid=@cid";
            using (var conn = context.CreateConnection())
            {
                foreach (Employee employee in emp)
                {
                    var par = new DynamicParameters();
                    par.Add("salary",employee.Salary);
                    var res = await conn.ExecuteAsync(qry, new { cid,par });
                    return res;
                }
            }return 0;
        }
    }
}
