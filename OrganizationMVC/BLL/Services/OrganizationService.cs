using FluentValidation;
using OrganizationMVC.BLL.DTO;
using OrganizationMVC.BLL.Interfaces;
using OrganizationMVC.DAL.Entities;
using OrganizationMVC.DAL.Interfaces;

namespace OrganizationMVC.BLL.Services;

public class OrganizationService: IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<OrganizationDTO> _organizationValidator;
    private readonly IValidator<EmployeeDTO> _employeeValidator;

    public OrganizationService(
        IOrganizationRepository organizationRepository,
        IEmployeeRepository employeeRepository,
        IValidator<OrganizationDTO> organizationValidator,
        IValidator<EmployeeDTO> employeeValidator)
    {
        _organizationRepository = organizationRepository;
        _employeeRepository = employeeRepository;
        _organizationValidator = organizationValidator;
        _employeeValidator = employeeValidator;
    }

    public async Task BulkInsertOrganizations(List<OrganizationDTO> organizations)
    {
        await ValidateOrganizationsAsync(organizations);
        var entities = organizations.Select(MapOrganization).ToList();
        await _organizationRepository.BatchInsertAsync(entities);
    }

    public async Task BulkInsertEmployee(List<EmployeeDTO> employees)
    {
        await ValidateEmployeesAsync(employees);
        var entities = employees.Select(MapEmployee).ToList();
        await _employeeRepository.BatchInsertAsync(entities);
    }

    public async Task BulkUpdateOrganizations(List<OrganizationDTO> organizations)
    {
        await ValidateOrganizationsAsync(organizations);
        var entities = organizations.Select(MapOrganization).ToList();
        await _organizationRepository.BatchUpdateAsync(entities);
    }

    public async Task BulkUpdateEmployee(List<EmployeeDTO> employees)
    {
        await ValidateEmployeesAsync(employees);
        var entities = employees.Select(MapEmployee).ToList();
        await _employeeRepository.BatchUpdateAsync(entities);
    }

    public async Task BulkDeleteOrganizations(List<int> organizationIds)
    {
        var entities = organizationIds.Select(id => new OrganizationEntity { Id = id }).ToList();
        await _organizationRepository.BatchDeleteAsync(entities);
    }

    public async Task BulkDeleteEmployee(List<int> employeeIds)
    {
        var entities = employeeIds.Select(id => new EmployeeEntity { Id = id }).ToList();
        await _employeeRepository.BatchDeleteAsync(entities);
    }

    public async Task<PagedResult<OrganizationDTO>> GetOrganizationsAsync(int page, int pageSize)
    {
        var normalizedPage = NormalizePage(page);
        var normalizedPageSize = NormalizePageSize(pageSize);
        var (items, total) = await _organizationRepository.GetPagedAsync(normalizedPage, normalizedPageSize);
        return new PagedResult<OrganizationDTO>
        {
            Items = items.Select(MapOrganization).ToList(),
            TotalCount = total,
            Page = normalizedPage,
            PageSize = normalizedPageSize
        };
    }

    public async Task<PagedResult<EmployeeDTO>> GetEmployeesByOrganizationIdAsync(int organizationId, int page, int pageSize)
    {
        var normalizedPage = NormalizePage(page);
        var normalizedPageSize = NormalizePageSize(pageSize);
        var (items, total) = await _employeeRepository.GetPagedByOrganisationIdAsync(
            organizationId,
            normalizedPage,
            normalizedPageSize);
        return new PagedResult<EmployeeDTO>
        {
            Items = items.Select(MapEmployee).ToList(),
            TotalCount = total,
            Page = normalizedPage,
            PageSize = normalizedPageSize
        };
    }

    public async Task<OrganizationDTO?> GetOrganizationByIdAsync(int organizationId)
    {
        var entity = await _organizationRepository.GetByIdAsync(organizationId);
        return entity == null ? null : MapOrganization(entity);
    }

    private static OrganizationEntity MapOrganization(OrganizationDTO dto)
    {
        return new OrganizationEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Inn = dto.Inn
        };
    }

    private static OrganizationDTO MapOrganization(OrganizationEntity entity)
    {
        return new OrganizationDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Inn = entity.Inn
        };
    }

    private static EmployeeEntity MapEmployee(EmployeeDTO dto)
    {
        return new EmployeeEntity
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            OrganizationId = dto.OrganizationId
        };
    }

    private static EmployeeDTO MapEmployee(EmployeeEntity entity)
    {
        return new EmployeeDTO
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            OrganizationId = entity.OrganizationId
        };
    }

    private async Task ValidateOrganizationsAsync(List<OrganizationDTO> organizations)
    {
        foreach (var organization in organizations)
        {
            var result = await _organizationValidator.ValidateAsync(organization);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }

    private async Task ValidateEmployeesAsync(List<EmployeeDTO> employees)
    {
        foreach (var employee in employees)
        {
            var result = await _employeeValidator.ValidateAsync(employee);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }

    private static int NormalizePage(int page)
    {
        return page < 1 ? 1 : page;
    }

    private static int NormalizePageSize(int pageSize)
    {
        return pageSize < 1 ? 10 : Math.Min(pageSize, 100);
    }
}
