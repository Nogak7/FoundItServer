using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class PostCommentDTO
    {
        public int Id { get; set; }

        public int? Post { get; set; }

        public string? Comment { get; set; }

        public DateTime? Date { get; set; }

        public int? Postcomment1 { get; set; }

      // public List<PostCommentDTO> Comments { get; set; } = new List<PostCommentDTO>();

        public PostCommentDTO() { }
        public PostCommentDTO(PostComment status)
        {
            Id = status.Id;
            Post = status.Post;
            Comment = status.Comment;
            Date = status.Date;
            Postcomment1 = status.Postcomment1;

        }
        public PostComment Convert()
        {
            return new PostComment { Id = Id, Post = Post, Date=Date,Postcomment1 = Postcomment1  };
        }

    }

}
