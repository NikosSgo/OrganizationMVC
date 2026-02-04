using OrganizationMVC.BLL.DTO;

namespace OrganizationMVC.Controllers.Requests;

public class BatchInsertOrganizationsRequest
{
    public List<OrganizationDTO> Organizations { get; set; } = new();
}
