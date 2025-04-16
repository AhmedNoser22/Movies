using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOS
{
    public class DtoNewUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
