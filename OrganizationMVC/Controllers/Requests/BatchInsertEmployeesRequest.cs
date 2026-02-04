using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Controllers.Requests;

public class BatchInsertEmployeesRequest
{
    public List<EmployeeDTO> Employees { get; set; } = new();
}
