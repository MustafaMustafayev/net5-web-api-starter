using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs
{
    public class UserToUpdateDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
