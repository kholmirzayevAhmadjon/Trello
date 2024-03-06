using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Commons;

namespace Project_menegment_tools.Models.User;

public class User : Auditable
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }
}