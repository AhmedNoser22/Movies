using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOS
{
    public class DTOLogin
    {
        [Required]
        public string userName {  get; set; }
        [Required]
        public string password {  get; set; }
    }
}
