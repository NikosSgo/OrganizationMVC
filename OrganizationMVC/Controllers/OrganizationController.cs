using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrganizationMVC.BLL.DTO;
using OrganizationMVC.BLL.Interfaces;
using OrganizationMVC.Controllers.Requests;
using OrganizationMVC.ViewModels;

namespace OrganizationMVC.Controllers;

[Route("organizations")]
public class OrganizationController : Controller
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index([FromQuery] GetOrganizationsRequest request)
    {
        var result = await _organizationService.GetOrganizationsAsync(request.Page, request.PageSize);
        var viewModel = new OrganizationsPageViewModel
        {
            Organizations = new PagedViewModel<OrganizationDTO>
            {
                Items = result.Items,
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            }
        };
        return View(viewModel);
    }

    [HttpGet("{organizationId:int}/employees")]
    public async Task<IActionResult> Employees([FromRoute] int organizationId, [FromQuery] GetEmployeesByOrganizationIdRequest request)
    {
        if (request.OrganizationId <= 0)
        {
            request.OrganizationId = organizationId;
        }

        var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);
        if (organization == null)
        {
            return NotFound();
        }

        var result = await _organizationService.GetEmployeesByOrganizationIdAsync(
            organizationId,
            request.Page,
            request.PageSize);

        var viewModel = new EmployeesPageViewModel
        {
            OrganizationId = organizationId,
            OrganizationName = organization.Name,
            Employees = new PagedViewModel<EmployeeDTO>
            {
                Items = result.Items,
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            }
        };

        return View(viewModel);
    }

    [HttpPost("batch-insert")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchInsertOrganizations([FromBody] BatchInsertOrganizationsRequest request)
    {
        if (request?.Organizations == null || request.Organizations.Count == 0)
        {
            return BadRequest(new { message = "Organizations are required." });
        }

        try
        {
            await _organizationService.BulkInsertOrganizations(request.Organizations);
            return Ok(new { message = "Organizations inserted.", count = request.Organizations.Count });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = "Validation failed.", errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPost("batch-update")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchUpdateOrganizations([FromBody] UpdateOrganizationsRequest request)
    {
        if (request?.Organizations == null || request.Organizations.Count == 0)
        {
            return BadRequest(new { message = "Organizations are required." });
        }

        try
        {
            await _organizationService.BulkUpdateOrganizations(request.Organizations);
            return Ok(new { message = "Organizations updated.", count = request.Organizations.Count });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = "Validation failed.", errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPost("batch-delete")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchDeleteOrganizations([FromBody] DeleteOrganizationsRequest request)
    {
        if (request?.OrganizationIds == null || request.OrganizationIds.Count == 0)
        {
            return BadRequest(new { message = "OrganizationIds are required." });
        }

        await _organizationService.BulkDeleteOrganizations(request.OrganizationIds);
        return Ok(new { message = "Organizations deleted.", count = request.OrganizationIds.Count });
    }

    [HttpPost("employees/batch-insert")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchInsertEmployees([FromBody] BatchInsertEmployeesRequest request)
    {
        if (request?.Employees == null || request.Employees.Count == 0)
        {
            return BadRequest(new { message = "Employees are required." });
        }

        try
        {
            await _organizationService.BulkInsertEmployee(request.Employees);
            return Ok(new { message = "Employees inserted.", count = request.Employees.Count });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = "Validation failed.", errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPost("employees/batch-update")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchUpdateEmployees([FromBody] UpdateEmployeesRequest request)
    {
        if (request?.Employees == null || request.Employees.Count == 0)
        {
            return BadRequest(new { message = "Employees are required." });
        }

        try
        {
            await _organizationService.BulkUpdateEmployee(request.Employees);
            return Ok(new { message = "Employees updated.", count = request.Employees.Count });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = "Validation failed.", errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPost("employees/batch-delete")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> BatchDeleteEmployees([FromBody] DeleteEmployeesRequest request)
    {
        if (request?.EmployeeIds == null || request.EmployeeIds.Count == 0)
        {
            return BadRequest(new { message = "EmployeeIds are required." });
        }

        await _organizationService.BulkDeleteEmployee(request.EmployeeIds);
        return Ok(new { message = "Employees deleted.", count = request.EmployeeIds.Count });
    }
}
