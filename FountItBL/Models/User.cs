using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Pasword { get; set; } = null!;

    

    public string UserName { get; set; } = null!;
}
