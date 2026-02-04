namespace OrganizationMVC.Controllers.Requests;

public class GetEmployeesByOrganizationIdRequest
{
    public int OrganizationId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
