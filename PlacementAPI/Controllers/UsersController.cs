using Microsoft.AspNetCore.Mvc;
using PlacementAPI.Models;
using PlacementAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService) =>
        _userService = userService;

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAsync());
        }

        [HttpGet]
        [Route("GetUserMailDetails")]
        public IActionResult GetUserMailDetails(string emailId)
        {
            return Ok(_userService.GetUserMailDetails(emailId));
        }

        [HttpGet]
        [Route("GetCompanies")]
        public IActionResult GetCompanies()
        {
            return Ok(_userService.GetCompanies());
        }

        [HttpGet]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            User userData = new User();
            bool IsLogin = false;
            var user = _userService.GetUsers();
            if (user.Any() && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                IsLogin = user.Where(u => u.Email == email && u.Password == password).Any() ? true : false;
                userData = user.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            }
            return Ok(IsLogin ? userData : null);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult PostUser(User newUser)
        {
            string msg;
            msg = _userService.CreateUser(newUser);
            return Ok(msg);
        }

        [HttpPut("{id:length(24)}")]
        [Route("UpdateUser")]
        public async Task<IActionResult> Update(IEnumerable<User> updatedUser)
        {
            var id = updatedUser.FirstOrDefault().Id;
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.UpdateAsync(id, updatedUser.FirstOrDefault());

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id:length(24)}")]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}
