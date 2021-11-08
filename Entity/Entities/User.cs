using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class User  : AuditableEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
