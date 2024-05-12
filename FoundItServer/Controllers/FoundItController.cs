using FountItBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoundItServer.DTO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;

namespace FoundItServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoundItController : ControllerBase
    {
        #region Add connection to the db context using dependency injection 

        FoundItDbContext context;

             public FoundItController(FoundItDbContext context)
             {
                this.context = context;
             }

        [Route("Test")]
        [HttpGet]
        public string Hello()
        {
            return "this is Noga Server";
        }
        
       [Route ("Register")]
       [HttpPost]
        public async Task<ActionResult<User>> RegisterAsync([FromBody] UserDTO userDto)
        {
            try
            {
                User user=userDto.Convert();
                bool isEmailExist = context.Users.Any(u => (u.Email == user.Email)||(u.UserName == user.UserName));
                if (isEmailExist == false)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return Ok(user);
                }
                else
                    return Conflict(user);
            }
            catch (Exception ex) { }
            return BadRequest();
        }
        [Route("LogIn")]
        [HttpPost]
        public async Task<ActionResult<User>> LogInAsync([FromBody] LoginDto user)
        {
            //
            try
            {
                bool IsUserExist = context.Users.Any(u => (u.UserName == user.UserName));
                if (IsUserExist)
                {
                    User user1 = context.Users.Where( u=> u.UserName == user.UserName).FirstOrDefault();
                    bool IsUserMatchPassword = (user.Pasword == user1.Pasword);
                    if (IsUserMatchPassword)                                         
                        return Ok(user);                    
                }
                else
                    return Conflict(user);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message    ); }
            return BadRequest();
        }

        [Route("CreateNewPost")]
        [HttpPost]
        public async Task<ActionResult<Post>> CreateNewPostAsync(IFormFile file, [FromForm] string post)
        {
            try
            {
                var p=JsonSerializer.Deserialize<PostDTO>(post);
                p.Picture = string.Empty;
                context.Posts.Add(p.Convert());
                context.SaveChanges();

                if (file == null || file.Length == 0)
                    return BadRequest("no image file");
                string filename = $"{p.Id}_postImage.jpg";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Images",filename);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                p.Picture = filename;
                return Ok(p);


            }
                
            catch (Exception ex) { }
            return BadRequest();
        }



        [Route("SearchItem")]
        [HttpPost]
        public async Task<ActionResult<List<Post>>> SearchItem([FromBody]string discription)
        {
            try
            {
                var posts = context.Posts.Where(u => u.Context.Contains(discription) || u.Location.Contains(discription) || u.Context.Contains(discription) || u.Theme.Contains(discription)).ToList();
                if (posts.Count != 0)
                {
                    return Ok(posts);
                }
            }
            catch (Exception ex) { }

            return BadRequest();    
        }

            [Route("UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string user)
        {

            User? p = JsonSerializer.Deserialize<User>(user);
            User? u = this.context.Users.Find(p.Id);


            //check file size
            if (file.Length > 0)
            {
                // Generate unique file name
                string fileName = $"{u.Id}{Path.GetExtension(file.FileName)}";

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                try
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    return Ok();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            return BadRequest();
        }

    

    [Route("GetUserPostsPics")]
    [HttpGet]
    public async Task<ActionResult<List<string>>>GetUserPostsPics([FromQuery]UserDTO user)
    {
            try
            {
                List<string> postpics = new List<string>();
                var a = context.Posts.Where(x => x.Creator == user.Id).ToList();
                foreach (var post in a)
                {
                    postpics.Add(post.Picture);
                }
                return Ok(postpics);
            }
            catch (Exception ex) { }
            return BadRequest();


    }
}

    #endregion
}
    

