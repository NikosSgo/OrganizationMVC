using System.ComponentModel.DataAnnotations;

namespace OrganizationMVC.DAL.Entities;

public class EmployeeEntity
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public int OrganizationId { get; set; }

    public OrganizationEntity Organization { get; set; }
}
