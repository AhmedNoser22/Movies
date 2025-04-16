using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOS
{
    public class copyclass
    {
        [MaxLength(50)]
        public string Name {  get; set; }
    }
}
