using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class PostStatus
{
    public int Id { get; set; }

    public string? Poststatus1 { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
