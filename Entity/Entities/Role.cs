using System.ComponentModel.DataAnnotations;


namespace Entity.Entities
{
    public class Role : AuditableEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string Rolename { get; set; }
    }
}
