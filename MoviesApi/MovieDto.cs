using System.ComponentModel.DataAnnotations;

namespace MoviesApi
{
    public class MovieDto
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public IFormFile? poster { get; set; }
        public byte GenreId { get; set; }
    }
}
