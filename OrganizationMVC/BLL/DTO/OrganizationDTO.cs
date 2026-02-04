namespace OrganizationMVC.BLL.DTO;

public class OrganizationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Inn { get; set; }

    public List<EmployeeDTO> Employees { get; set; }
}
