namespace ClientListApp.Models;

public class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Profession { get; set; } = "";
    public string OrganizationKey { get; set; } = "";
}
