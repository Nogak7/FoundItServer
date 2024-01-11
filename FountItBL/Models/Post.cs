using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Theme { get; set; }

    public string? Context { get; set; }

    public bool? FoundItem { get; set; }

    public string? Picture { get; set; }

    public int? Creator { get; set; }

    public DateTime? CreatingDate { get; set; }

    public string? Location { get; set; }

    public int? Status { get; set; }

    public virtual User? CreatorNavigation { get; set; }

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual PostStatus? StatusNavigation { get; set; }
}
