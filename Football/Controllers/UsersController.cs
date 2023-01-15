using Football.Application.DataTransferObjects.Users;
using Football.Application.Models;
using Football.Application.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpPost]
        public async ValueTask<ActionResult<UsersDTO>> PostUserAsync(
            UserForCreationDto userForCreationDto)
        {
            var createUser = await this.userServices.CreateUserAsync(userForCreationDto);

            return Created("", createUser);
        }
        [HttpGet]
        public IActionResult GetUsers([FromQuery] QueryParametr queryParametr)
        {
            var users = this.userServices.RetrieveUsers(queryParametr);

            return Ok(users);
        }
        [HttpGet("{userId:guid}")]
        public async ValueTask<ActionResult<UsersDTO>> GetUserByIdAsync(Guid Id)
        {
            var user = await this.userServices.RetrieveUserByIdAsync(Id);


            return Ok(user);
        }
        [HttpPut]
        public async ValueTask<ActionResult<UsersDTO>> PutUsersAsync(
            UserForModificationDto userForModificationDto)
        {
            var modifyuser = await this.userServices.ModifyUserAsync(userForModificationDto);

            return Ok(modifyuser);
        }
        [HttpDelete("{userId:guid}")]
        public async ValueTask<ActionResult<UsersDTO>> DeleteUserAsync(
            Guid userId)
        {
            var removed = await this.userServices.RemoveUserAsync(userId);

            return Ok(removed);
        }
    }
}
