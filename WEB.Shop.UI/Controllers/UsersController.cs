using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB.Shop.Application.UsersAdmin;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;

        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request request)
        {
            await _createUser.Do(request);
            return Ok();
        }


    }
}