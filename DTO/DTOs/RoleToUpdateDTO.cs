using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs
{
    public class RoleToUpdateDTO
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Rolename { get; set; }
    }
}
