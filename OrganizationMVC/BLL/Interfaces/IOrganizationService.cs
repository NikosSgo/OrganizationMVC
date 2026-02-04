using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.BLL.Interfaces;

public interface IOrganizationService
{
    Task BulkInsertOrganizations(List<OrganizationDTO> organizations);
    Task BulkInsertEmployee(List<EmployeeDTO> employees);
    Task BulkUpdateOrganizations(List<OrganizationDTO> organizations);
    Task BulkUpdateEmployee(List<EmployeeDTO> employees);
    Task BulkDeleteOrganizations(List<int> organizationIds);
    Task BulkDeleteEmployee(List<int> employeeIds);
    Task<PagedResult<OrganizationDTO>> GetOrganizationsAsync(int page, int pageSize);
    Task<PagedResult<EmployeeDTO>> GetEmployeesByOrganizationIdAsync(int organizationId, int page, int pageSize);
    Task<OrganizationDTO?> GetOrganizationByIdAsync(int organizationId);
}
