using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.BLL.Interfaces;

public interface IOrganizationService
{
    Task BulkInsertOrganizations(List<OrganizationDTO> organizations);
    Task BulkInsertEmployee(List<EmployeeDTO> employees);
    Task BulkUpdateOrganizations(List<OrganizationDTO> organizations);
    Task BulkUpdateEmployee(List<EmployeeDTO> employees);
    Task BulkDeleteOrganizations(List<OrganizationDTO> organizations);
    Task BulkDeleteEmployee(List<EmployeeDTO> employees);
    Task GetAllOrganizations();
    Task GetAllEmployeesByOrganizationId(int organizationId);
}
