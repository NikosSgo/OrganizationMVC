namespace OrganizationMVC.BLL.DTO;

public class EmployeeDTO
{
    public int  Id { get; set; }
    public string FirstName  { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int OrganizationId { get; set; }
}
