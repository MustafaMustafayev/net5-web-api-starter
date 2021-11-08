using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs
{
    public class RoleToAddDTO
    {
        [Required]
        public string Rolename { get; set; }
    }
}
