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

    public string? ProfilePicture { get; set; }

    public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
