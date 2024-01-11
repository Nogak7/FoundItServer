using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class CommunityMember
{
    public int Id { get; set; }

    public int? User { get; set; }

    public int? Community { get; set; }

    public virtual Community? CommunityNavigation { get; set; }

    public virtual User? UserNavigation { get; set; }
}
