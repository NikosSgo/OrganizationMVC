namespace OrganizationMVC.Controllers.Requests;

public class GetOrganizationsRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
