using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.ViewModels;

public class OrganizationsPageViewModel
{
    public required PagedViewModel<OrganizationDTO> Organizations { get; init; }
}
