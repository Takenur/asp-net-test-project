using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Services;

namespace TestRestCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userService;

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok(name);
                }
                else
                {
                    return BadRequest("Role create Error");
                }
            }
            return BadRequest("Empty value");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return NotFound();
        }
        [HttpPut("update-user-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userService.FindByIdAsync(userId);
            if (user != null)
            {
               
                var userRoles = await _userService.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                var addedRoles = roles.Except(userRoles); 

                var removedRoles = userRoles.Except(roles);

                await _userService.AddToRolesAsync(user, addedRoles);

                await _userService.RemoveFromRolesAsync(user, removedRoles);

                return Ok(user);
            }

            return NotFound();
        }
    }
}
