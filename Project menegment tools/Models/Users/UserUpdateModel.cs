﻿using Project_menegment_tools.Enums;

namespace Project_menegment_tools.Models.Users;

public class UserUpdateModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }
}
