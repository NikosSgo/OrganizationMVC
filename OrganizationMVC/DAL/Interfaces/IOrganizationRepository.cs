using OrganizationMVC.DAL.Entities;

namespace OrganizationMVC.DAL.Interfaces;

public interface IOrganizationRepository
{
    Task<List<OrganizationEntity>> GetAllAsync();
    Task BatchInsertAsync(List<OrganizationEntity> entities);

}
