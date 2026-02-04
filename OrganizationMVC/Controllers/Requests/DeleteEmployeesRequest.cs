namespace OrganizationMVC.Controllers.Requests;

public class DeleteEmployeesRequest
{
    public List<int> EmployeeIds { get; set; } = new();
}
