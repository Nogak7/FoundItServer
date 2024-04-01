using FountItBL.Models;

namespace FoundItServer.DTO
{
    public class CommunityDTO
    {
        public int Id { get; set; }

       // public int? Manager { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public virtual ICollection<UserDTO> CommunityMembers { get; set; } = new List<UserDTO>();

        public virtual User? Manager { get; set; }
        public CommunityDTO() { }   
        public CommunityDTO(Community community)
        {
            this.Id = community.Id; 
            this.Name = community.Name;
            this.Location = community.Location;
            this.Manager = community.ManagerNavigation;
            foreach(var c in community.CommunityMembers) 
            {
                if (c != null)
                {
                    this.CommunityMembers.Add(new UserDTO(c.UserNavigation));
                }
            }
        }

        public Community Convert()
        {
            Community community = new Community()
            {
                Id = this.Id,
                Name = this.Name,
                Location = this.Location,
                Manager = this.Manager.Id,
                CommunityMembers=new List<CommunityMember>()

            };
            foreach(var member in this.CommunityMembers)
            {
                if(member!=null)
                community.CommunityMembers.Add(new CommunityMember() { User = member.Id, CommunityNavigation=community, Community = this.Id });
            }
            return community;

        }
    }
}