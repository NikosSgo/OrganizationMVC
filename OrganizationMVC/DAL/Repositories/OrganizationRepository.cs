using Microsoft.EntityFrameworkCore;
using OrganizationMVC.DAL.Entities;
using OrganizationMVC.DAL.Interfaces;

namespace OrganizationMVC.DAL.Repositories;

public class OrganizationRepository:  IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrganizationEntity>> GetAllAsync()
    {
        return await _context.Organizations
            .ToListAsync();
    }

    public async Task BatchInsertAsync(List<OrganizationEntity> entities)
    {
        await _context.Organizations.AddRangeAsync(entities);
    }

    public async Task BatchUpdateAsync(List<OrganizationEntity> entities)
    {
        await _context.Organizations.AddRangeAsync(entities);
    }

    public async Task BatchDeleteAsync(List<OrganizationEntity> entities)
    {
        await _context.Organizations.AddRangeAsync(entities);
    }
}
