using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MoviesApi
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year {  get; set; }
        public double Rate { get; set; }
        [MaxLength(100)]
        public string StoreLine { get; set; }
        public byte[] poster { get; set; }
        [ForeignKey(nameof(genra))]
        public byte GenreId {  get; set; }
        public Genra genra { get; set; }
    }
}
