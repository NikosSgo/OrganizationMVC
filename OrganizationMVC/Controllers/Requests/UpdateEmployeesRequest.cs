using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Controllers.Requests;

public class UpdateEmployeesRequest
{
    public List<EmployeeDTO> Employees { get; set; } = new();
}
