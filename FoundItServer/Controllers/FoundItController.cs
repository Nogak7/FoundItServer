using FountItBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

#endregion
    }
}
