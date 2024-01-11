using System;
using System.Collections.Generic;

namespace FountItBL.Models;

public partial class PostComment
{
    public int Id { get; set; }

    public int? Post { get; set; }

    public string? Comment { get; set; }

    public DateTime? Date { get; set; }

    public int? Postcomment1 { get; set; }

    public virtual ICollection<PostComment> InversePostcomment1Navigation { get; set; } = new List<PostComment>();

    public virtual Post? PostNavigation { get; set; }

    public virtual PostComment? Postcomment1Navigation { get; set; }
}
