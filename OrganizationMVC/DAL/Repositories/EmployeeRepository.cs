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

    public async Task<List<EmployeeEntity>> GetAllByOrganisationIdAsync(int organisationId)
    {
        return await _context.Employees.
            Where(e => e.OrganizationId  == organisationId)
            .ToListAsync();
    }

    public async Task BatchInsertAsync(List<EmployeeEntity> entities)
    {
        await _context.Employees.AddRangeAsync(entities);
    }

    public async Task BatchUpdateAsync(List<EmployeeEntity> entities)
    {
        await _context.Employees.AddRangeAsync(entities);
    }

    public async Task BatchDeleteAsync(List<EmployeeEntity> entities)
    {
        await _context.Employees.AddRangeAsync(entities);
    }

}
