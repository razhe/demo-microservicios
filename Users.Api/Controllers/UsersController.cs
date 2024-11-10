using Microsoft.AspNetCore.Mvc;
using Users.Api.Dtos;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<UserDto> users = new()
        {
            new UserDto { Id = 1, Name = "Juan Pérez", Email = "juan.perez@example.com" },
            new UserDto { Id = 2, Name = "María López", Email = "maria.lopez@example.com" },
            new UserDto { Id = 3, Name = "Carlos García", Email = "carlos.garcia@example.com" },
            new UserDto { Id = 4, Name = "Ana Martínez", Email = "ana.martinez@example.com" },
            new UserDto { Id = 5, Name = "Pedro Rodríguez", Email = "pedro.rodriguez@example.com" }
        };

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<List<UserDto>> ListUsers()
        {
            return Ok(users);
        }
    }
}
