using Microsoft.EntityFrameworkCore;
using OrganizationMVC.DAL.Entities;
using OrganizationMVC.DAL.Interfaces;

namespace OrganizationMVC.DAL.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(List<EmployeeEntity> Items, int TotalCount)> GetPagedByOrganisationIdAsync(int organisationId, int page, int pageSize)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Where(e => e.OrganizationId == organisationId)
            .OrderBy(e => e.Id);
        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task BatchInsertAsync(List<EmployeeEntity> entities)
    {
        await _context.Employees.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task BatchUpdateAsync(List<EmployeeEntity> entities)
    {
        _context.Employees.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task BatchDeleteAsync(List<EmployeeEntity> entities)
    {
        _context.Employees.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

}
