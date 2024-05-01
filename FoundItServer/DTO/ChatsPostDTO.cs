using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class ChatsPostDTO
    {
        public User Creator { get; set; }
        public User Sender { get; set; }
        public Post Post { get; set; }
        public List<PostComment> PostComments { get; set; }
    }
}
