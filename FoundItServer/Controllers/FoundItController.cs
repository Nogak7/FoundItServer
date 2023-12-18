using FountItBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoundItServer.DTO;

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
        public async Task<ActionResult<User>> RegisterAsync([FromBody] User user)
        {
            try
            {
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
            catch (Exception ex) { }
            return BadRequest();
        }
        #endregion
    }
    
}
