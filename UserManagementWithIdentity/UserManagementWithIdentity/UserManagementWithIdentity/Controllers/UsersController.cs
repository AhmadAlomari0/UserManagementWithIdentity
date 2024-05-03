using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementWithIdentity.Models;

namespace UserManagementWithIdentity.Controllers
{
    [Authorize(Roles =Role.userRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _UserManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _UserManager.GetRolesAsync(user).Result

            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound();
            }

            var roles = await _RoleManager.Roles.ToListAsync();

            var vieWModel = new UserRoleViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _UserManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };
            return View(vieWModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            //Theses 3 lines check if the user is exists or not.
            var user = await _UserManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();
            //Get all of this user roles
            var userRoles = await _UserManager.GetRolesAsync(user);

            //Get all of user roles and compare it with the roles that exist in DB
            foreach (var role in model.Roles)
            {
                //Remove role if found one.
                if(userRoles.Any(r=> r == role.RoleName) && !role.IsSelected )
                    await _UserManager.RemoveFromRoleAsync(user, role.RoleName);
                //Add role if not found.
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _UserManager.AddToRoleAsync(user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
