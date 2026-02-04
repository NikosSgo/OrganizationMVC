using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Validation;

using FluentValidation;

public class OrganizationDtoValidator:
    AbstractValidator<OrganizationDTO>
{
    public OrganizationDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();

    }
}
