using OrganizationMVC.DAL.Entities;

namespace OrganizationMVC.DAL.Interfaces;

public interface IOrganizationRepository
{
    Task<OrganizationEntity?> GetByIdAsync(int id);
    Task<(List<OrganizationEntity> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
    Task BatchInsertAsync(List<OrganizationEntity> entities);
    Task BatchUpdateAsync(List<OrganizationEntity> entities);
    Task BatchDeleteAsync(List<OrganizationEntity> entities);
}
