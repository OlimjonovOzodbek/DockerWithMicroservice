using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.API.Persistance;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Companies> Get()
        {
            return [.. _appDbContext.Companies];
        }

        [HttpGet("{id}")]
        public async Task<List<Companies>> Get(int id)
        {
            return [.. _appDbContext.Companies];
        }

        [HttpPost]
        public async Task<Companies> Post([FromBody] Companies product)
        {
            var result = await _appDbContext.Companies.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut("{id}")]
        public async Task<Companies> Put(int id, [FromBody] Companies product)
        {
            var pr = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Companies.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Companies> Delete(int id)
        {
            var pr = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Companies.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
