using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOS;
using MoviesApi.Models;


namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenersController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var item = await _context.Genras.OrderBy(x=>x.Name).ToListAsync();
            return Ok(item);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult>AddData(List<copyclass> copyclass)
        {
            var item = copyclass.Select(x => new Genra { Name = x.Name }).ToList();
            await _context.Genras.AddRangeAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, [FromBody] copyclass copyclass)
        {
            var item = await _context.Genras.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound($"id {id} not exist ");
            item.Name = copyclass.Name; 
            await _context.SaveChangesAsync();
            return Ok(item);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteData(List<int> id)
        {
            var items = await _context.Genras.
               // Where(x => id.Contains(x.Id)).ToListAsync();
               Where(x => x.Id > 12 & x.Id < 20).ToListAsync();
            if (items == null) return NotFound($"id {id} not exist ");
            _context.Genras.RemoveRange(items);
            await _context.SaveChangesAsync();
            return Ok(items);
        }


    }
}
