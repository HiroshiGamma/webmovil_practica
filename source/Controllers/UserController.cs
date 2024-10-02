using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.data;
using api01.source.Dtos;
using api01.source.Mappers;
using api01.source.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api01.source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly  ApplicationDBContext _context;

        public UserController(ApplicationDBContext context){

            _context = context;
        }

        //obtener todos los usuarios 
        [HttpGet] // read
        public IActionResult Get()
        {

            var users = _context.Users.Include(u => u.Rol).ToList().Select(u => u.toUserDto());
            return Ok(users);

        }

        //obtener un usuario por id 
        [HttpGet("{id}")] // read 
        public IActionResult GetById([FromRoute] int id)
        {

            var user = _context.Users.Include(u => u.Rol).FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user.toUserDto());
        } 

        [HttpPost] // Create
        public IActionResult Post([FromBody] CreateUserRequestDto userDto)
        {
            
            var rol = _context.Rols.FirstOrDefault(r => r.Id == userDto.RolId);
            if(rol == null)
            {
                return BadRequest("Rol not found");

            }
            
            var userModel = userDto.toUserFromCreatoDto();
            _context.Users.Add(userModel);
            _context.SaveChanges();
            return Ok();
        }


        
        [HttpPut("{id}")] // update
        public IActionResult Put([FromRoute] int id, [FromBody] User user )
        {
            var rol = _context.Rols.FirstOrDefault(r => r.Id == user.RolId);
            if( rol == null)    
            {
                return BadRequest("Rol not found");
            }

            var userToUpdate = _context.Users.FirstOrDefault( u => u.Id == id);
            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Rut = user.Rut;
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.RolId = user.RolId;

            _context.SaveChanges(); 
            return Ok(userToUpdate);
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = _context.Users.FirstOrDefault( u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost("withCookie")] // crear un usuario con cookie
        public IActionResult PostByCookie([FromBody]User user)
        {
            var rol = _context.Rols.FirstOrDefault(r => r.Id == user.RolId);
            if(rol == null)
            {
                return BadRequest("Rol not found");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions{
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(5)
            });

            return Ok(user);
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if(Request.Cookies.TryGetValue("UserId", out var userId))
            {
                var user = _context.Users.Include(u => u.Rol).FirstOrDefault( u => u.Id == int.Parse(userId));

                if(user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            return NotFound("UserId cookie not found");
        }
    }

}