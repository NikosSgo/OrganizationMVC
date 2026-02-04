namespace OrganizationMVC.BLL.DTO;

public class EmployeeDTO
{
    public int  Id { get; set; }
    public string FirstName  { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int OrganizationId { get; set; }
}
