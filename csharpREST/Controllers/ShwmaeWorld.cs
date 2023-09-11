using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace csharpREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShwmaeWorld : ControllerBase
    {
        
        private readonly List<User> _users;
        //private String fname;
        
        // CONSTRUCTOR 
        public ShwmaeWorld(String fname)
        {
            //fname = this.fname;
            _users = new List<User>()
            {
                new User() { Id =  1, Name = "Bob"},
                new User() { Id = 2, Name = "John"},
                new User() { Id = 3, Name = "Derek"}
                new User() { Id = 4, Name = "Johnny"}
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers(int id)
        {
            return _users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _users[id];
            if(user != null)
            {
                return user;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            user.Id = GenerateNewId();
            _users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        private int GenerateNewId()
        {
            return _users.Count + 1;
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _users.Find(x => x.Id == id);
            if(user != null)
            {
                user.Name = updatedUser.Name;
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if( user != null )
            {
                _users.Remove(user);
                return NoContent();
            }else
            {
                return NotFound();
            }
        }
    }
}
