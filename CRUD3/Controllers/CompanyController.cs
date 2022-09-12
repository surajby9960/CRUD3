using CRUD3.Model;
using CRUD3.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository repo;
        public CompanyController(ICompanyRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await repo.GetAllCompanies();
                return Ok(companies);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var company = await repo.GetCompanyById(id);
                return Ok(company);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> InsertCompnay(Company company )
        {
            try
            {
                var res=await repo.InsertCompany(company);
                return Ok("inserted Succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            try
            {
                var res = await repo.UpdateCompany(company,id);
                return Ok("Updated Succsfully");

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                 await repo.DeleteCompany(id);
                return Ok("Deleted Succsfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
