using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Now.Domain.Entities
{
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
