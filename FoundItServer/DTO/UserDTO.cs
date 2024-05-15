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
        public string ProfilePicture { get; set; }
        //public virtual ICollection<CommunityDTO> Communities { get; set; } = new List<CommunityDTO>();

       

       // public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public UserDTO() { }
        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email; 
            FirstName = user.FirstName; 
            LastName = user.LastName;   
            Pasword = user.Pasword; 
            UserName = user.UserName;   
            ProfilePicture = user.ProfilePicture;
           // Communities = user.Communities;
           //foreach (Community c in user.Communities) 
           //{
           //     if (c!=null)
           //     {
           //         CommunityDTO community = new CommunityDTO(c);
           //         Communities.Add(community);
           //     }
           //}
   //foreach convert user post to postdto
            //Posts = user.Posts; 

        }
        public User Convert()
        {
         return new User { Id = Id, Email = Email, FirstName = FirstName, LastName = LastName, Pasword = Pasword, UserName = UserName };
          
        }
    }
}
