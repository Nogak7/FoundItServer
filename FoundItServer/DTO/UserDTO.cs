using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; } 

        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public string Pasword { get; set; } 
        public string UserName { get; set; } 

        public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

        public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
