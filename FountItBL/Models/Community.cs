using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class Community
{
    public int Id { get; set; }

    public int? Manager { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual User? ManagerNavigation { get; set; }
}
