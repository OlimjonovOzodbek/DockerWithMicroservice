using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistanse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Models.Product> Get()
        {
            return [.. _appDbContext.Products];
        }

        [HttpGet("{id}")]
        public async Task<List<Models.Product>> Get(int id)
        {
            return [.. _appDbContext.Products];
        }

        [HttpPost]
        public async Task<Models.Product> Post([FromBody] Models.Product product)
        {
            var result = await _appDbContext.Products.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut("{id}")]
        public async Task<Models.Product> Put(int id, [FromBody] Models.Product product)
        {
            var pr = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Products.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Models.Product> Delete(int id)
        {
            var pr = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Products.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
