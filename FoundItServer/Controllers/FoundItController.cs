﻿using FountItBL.Models;
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
#endregion
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
        public async Task<ActionResult> LogInAsync([FromBody] LoginDto user)
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
                        return Ok(new UserDTO(user1));                    
                }
                else
                    return Conflict();
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
                Post dbpost = p.Convert();
                context.Posts.Add(dbpost);
                context.SaveChanges();

                if (file == null || file.Length == 0)
                    return BadRequest("no image file");
                string filename = $"{dbpost.Id}_postImage.jpg";
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
        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> SearchItem([FromQuery]string discription)
        {
            try
            {
                List<PostDTO> postsDTO = new List<PostDTO>();
                var posts = await context.GetPostDiscription(discription);
                if (posts.Count != 0)
                {
                    foreach (var post in posts)
                    {
                        postsDTO.Add(new PostDTO(post));
                    }
                    return Ok(postsDTO);
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


    [Route("GetPostsByPic")]
    [HttpGet]
    public async Task<ActionResult<PostDTO>> GetPostsByPic([FromQuery] string postImage)
        {
            try 
            {
                var address = postImage.Split('/');
                PostDTO postDTO=null;
                var p = await context.GetPostByImage(address[address.Length - 1]);
                //var p = context.Posts.Where(x => x.Picture == ).FirstOrDefault();
                if(p != null)   
                postDTO = new PostDTO(p);
                
                return Ok(postDTO);
            }
            catch (Exception ex) { }
            return BadRequest();
        }

    [Route("GetUserPostsPics")]
    [HttpGet]
    public async Task<ActionResult<List<string>>>GetUserPostsPics([FromQuery]int userid)
    {
            try
            {
              //  List<string> postpics = new List<string>();
                var a = context.Posts.Where(x => x.Creator == userid&&!string.IsNullOrEmpty(x.Picture)).Select(pp=>pp.Picture);
                
                return Ok(a.ToList());
            }
            catch (Exception ex) { }
            return BadRequest();


    }
        [Route("GetUserPosts")]
        [HttpGet]
        public async Task<ActionResult> GetUserPosts(int userId)
        {
            try
            {
                var posts = await context.GetPostByUser(userId);
               
                var resultposts = posts.Select(p => new PostDTO(p));
                return Ok(resultposts.ToList());

            }
            catch (Exception ex) { }
            return BadRequest();
        }

        [Route("GetPostCommentsResponses")]
        [HttpGet]
        public async Task<ActionResult> GetPostCommentsResponses(int userId)
        {
            try
            {
                var postComments = await context.GetPostCommentsResponses(userId);

                var resultpostcomments = postComments.Select(p => new PostCommentDTO(p));
                return Ok(resultpostcomments.ToList());

            }
            catch (Exception ex) { }
            return BadRequest();
        }
    }

    
}
    

