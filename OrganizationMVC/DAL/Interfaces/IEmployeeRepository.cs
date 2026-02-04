using OrganizationMVC.DAL.Entities;

namespace OrganizationMVC.DAL.Interfaces;

public interface IEmployeeRepository
{
    Task<(List<EmployeeEntity> Items, int TotalCount)> GetPagedByOrganisationIdAsync(int organisationId, int page, int pageSize);
    Task BatchInsertAsync(List<EmployeeEntity> entities);
    Task BatchUpdateAsync(List<EmployeeEntity> entities);
    Task BatchDeleteAsync(List<EmployeeEntity> entities);
}
