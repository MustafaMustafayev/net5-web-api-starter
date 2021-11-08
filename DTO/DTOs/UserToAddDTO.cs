using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs
{
    public class UserToAddDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
