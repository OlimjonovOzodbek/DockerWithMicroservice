using Blog.API.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public async Task<IEnumerable<Models.Client>> Get()
        {
            return await _appDbContext.Client.ToListAsync();
        }

        [HttpPost]
        public async Task<Models.Client> Post([FromBody] Models.Client client)
        {
            var result = await _appDbContext.Client.AddAsync(client);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut("{id}")]
        public async Task<Models.Client> Put(int id, [FromBody] Models.Client client)
        {
            var pr = await _appDbContext.Client.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = client.Name;
            pr.Description = client.Description;
            var result = _appDbContext.Client.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Models.Client> Delete(int id)
        {
            var pr = await _appDbContext.Client.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Client.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
