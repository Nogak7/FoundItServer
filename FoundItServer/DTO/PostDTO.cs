using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string? Theme { get; set; }

        public string? Context { get; set; }

        public bool? FoundItem { get; set; }

        public string? Picture { get; set; }


        public DateTime? CreatingDate { get; set; }

        public string? Location { get; set; }

        

        public  UserDTO? Creator { get; set; }

        //change to ICollection<PostCommentDTO> PostComments...
        public  ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

        
        public  PostStatusDTO? Status { get; set; }

        public PostDTO() { }

        public PostDTO(Post p)
        {
            Id = p.Id;
            Theme = p.Theme;
            Context = p.Context;
            FoundItem = p.FoundItem;
            Picture = p.Picture;
            CreatingDate = p.CreatingDate;
            Location = p.Location;
            //change to UserDto....
            Creator = new UserDTO(p.CreatorNavigation);
            //change to PostComments Dto....
            PostComments = p.PostComments;
            
            Status = new PostStatusDTO(p.StatusNavigation);

        }
        public Post Convert()
        {
            var post= new Post { Id = Id, Theme = Theme, Context = Context, FoundItem = FoundItem, CreatingDate = CreatingDate, Location = Location, Picture = Picture, Creator = Creator.Id, Status = Status.Id };
            foreach(var item in PostComments) 
            {
                //post.PostComments.Add(item.Convert());
            }
            return post;
        }

    }
}
