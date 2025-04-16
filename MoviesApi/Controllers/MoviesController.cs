using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOS;
using MoviesApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private List<string> allowedexten = new List<string> {".jpg",".png" };
        private long Maxallowed = 2097152;
        public MoviesController(AppDbContext context)
        {
            _context=context;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var item = await _context.Movies.Include(x => x.genra).ToListAsync();
            return Ok(item);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataId(int id)
        {
            var item = await _context.Movies.Include(m=>m.genra).SingleOrDefaultAsync(x=>x.Id==id);
            if (item == null) return NotFound($"Id {id} not exist");
            var x = new services
            {
                Id = item.Id,
                Title = item.Title,
                Year = item.Year,
                poster = item.poster,
                GenreId = item.GenreId,
                Rate=item.Rate,
                genra = item.genra.Name

            };
            return Ok(x);
        }
        [Authorize]
        [HttpGet("GetFId/{id}")]
        public async Task<IActionResult> GetFId(byte id)
        {
            var item = await _context.Movies
                .Where(x => x.GenreId == id)
                .Include(m => m.genra)
                .Select(x => new services
                {
                    Id = x.Id,
                    poster = x.poster,
                    GenreId=x.GenreId,
                    Title=x.Title,
                    Rate = x.Rate,
                    Year=x.Year,
                    genra = x.genra.Name

                }).ToListAsync();

            return Ok(item);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddData([FromForm] MovieDto movieDto)
        {
            var ValidGenre = await _context.Genras.AnyAsync(x => x.Id == movieDto.GenreId);
            if (!ValidGenre) return BadRequest("The Movie Not Equal Any Value In The Genra");
            if (movieDto.poster.Length > Maxallowed) 
                return BadRequest("The Size Not Allowed");
            if (!allowedexten.Contains(Path.GetExtension(movieDto.poster.FileName).ToLower())) 
                return BadRequest("The Extension Not Allowed");
            using var datastream = new MemoryStream();
            await movieDto.poster.CopyToAsync(datastream);
            var item = new Movie
            {
                GenreId = movieDto.GenreId,
                Title = movieDto.Title,
                poster = datastream.ToArray(),
                Rate = movieDto.Rate,
                Year = movieDto.Year

            };
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateData(int id, [FromForm] MovieDto movieDto)
        {
            if (movieDto == null) return BadRequest("required");
            var item = await _context.Movies.SingleOrDefaultAsync(x=>x.Id==id);
            if (item == null) return BadRequest($"Id: {id} not exist");
            var validField = await _context.Genras.AnyAsync(x=>x.Id==movieDto.GenreId);
            if (!validField) return BadRequest();
            if (movieDto.poster != null)
            {
                if (!allowedexten.Contains(Path.GetExtension(movieDto.poster.FileName).ToLower()))
                    return BadRequest("only png , gpg");
                if (movieDto.poster.Length > Maxallowed)
                    return BadRequest("not allowed");
                using (var datastream = new MemoryStream())
                await movieDto.poster.CopyToAsync(datastream);
            }
            item.GenreId = movieDto.GenreId;
            item.Title = movieDto.Title;
            item.Rate = movieDto.Rate;
            item.Year = movieDto.Year;
            
            await _context.SaveChangesAsync();
            return Ok(item);


        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteData(int id)
        {
            var item = await _context.Movies.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound($" Id : {id} not found");
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(item);

        }
     

        }
}
