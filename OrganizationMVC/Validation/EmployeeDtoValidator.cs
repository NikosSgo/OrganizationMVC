using FluentValidation;
using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Validation;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDTO>
{
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .MaximumLength(100);
        RuleFor(x => x.OrganizationId)
            .GreaterThan(0);
    }
}
