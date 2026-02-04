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

    public async Task<OrganizationEntity?> GetByIdAsync(int id)
    {
        return await _context.Organizations
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<(List<OrganizationEntity> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Organizations.AsNoTracking().OrderBy(o => o.Id);
        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task BatchInsertAsync(List<OrganizationEntity> entities)
    {
        await _context.Organizations.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task BatchUpdateAsync(List<OrganizationEntity> entities)
    {
        _context.Organizations.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task BatchDeleteAsync(List<OrganizationEntity> entities)
    {
        _context.Organizations.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}
