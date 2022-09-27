using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechanicAPI.Models;
using TechanicAPI.Repository;


namespace TechanicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(User entity)
        {
            return Ok(await _userRepository.Insert(entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update(User entity)
        {
            return Ok(await _userRepository.Update(entity));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userRepository.GetById(id));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _userRepository.Delete(id));
        }
    }
}
