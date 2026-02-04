namespace OrganizationMVC.Controllers.Requests;

public class DeleteOrganizationsRequest
{
    public List<int> OrganizationIds { get; set; } = new();
}
