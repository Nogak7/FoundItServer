using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class PostStatusDTO
    {
        public int Id { get; set; }

        public string? Poststatus1 { get; set; }

        public PostStatusDTO() { }
        public PostStatusDTO(PostStatus status)
        {
            Id = status.Id;
            Poststatus1 = status.Poststatus1;

        }
        public PostStatus Convert()
        {
            return new PostStatus { Id = Id, Poststatus1 = Poststatus1 };
        }
    }
}
