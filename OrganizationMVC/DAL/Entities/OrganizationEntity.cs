using System.ComponentModel.DataAnnotations;

namespace OrganizationMVC.DAL.Entities;

public class OrganizationEntity
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(30)]
    public string Inn { get; set; }

    public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
}
