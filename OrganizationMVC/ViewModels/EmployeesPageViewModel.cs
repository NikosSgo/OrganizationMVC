using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.ViewModels;

public class EmployeesPageViewModel
{
    public int OrganizationId { get; init; }
    public string OrganizationName { get; init; } = string.Empty;
    public required PagedViewModel<EmployeeDTO> Employees { get; init; }
}
