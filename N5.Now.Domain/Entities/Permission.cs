using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Now.Domain.Entities;

public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required, StringLength(100)]
    [Column("employee_first_name")]
    public string EmployeeFirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    [Column("employee_last_name")]
    public string EmployeeLastName { get; set; } = string.Empty;

    [Column("permission_type_id")]
    public int PermissionTypeId { get; set; }

    [Column("permission_date")]
    public DateTime PermissionDate { get; set; }

    public virtual PermissionType? PermissionType { get; set; }
}
