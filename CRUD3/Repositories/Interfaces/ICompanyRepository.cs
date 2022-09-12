using CRUD3.Model;

namespace CRUD3.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompanies();
        public Task<Company> GetCompanyById(int id);
        public Task<int> InsertCompany(Company company);
        public Task<int> UpdateCompany(Company company,int id);    
        public Task DeleteCompany(int id);
    }
}
