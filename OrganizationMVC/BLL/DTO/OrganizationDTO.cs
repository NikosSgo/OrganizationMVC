namespace OrganizationMVC.BLL.DTO;

public class OrganizationDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;

    public List<EmployeeDTO> Employees { get; set; } = new();
}
