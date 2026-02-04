using OrganizationMVC.DAL.Entities;

namespace OrganizationMVC.DAL.Interfaces;

public interface IEmployeeRepository
{
    Task<List<EmployeeEntity>> GetAllByOrganisationIdAsync(int organisationId);
    Task BatchInsertAsync(List<EmployeeEntity> entities);

}
