using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Controllers.Requests;

public class UpdateOrganizationsRequest
{
    public List<OrganizationDTO> Organizations { get; set; } = new();
}
