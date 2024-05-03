using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using UserManagementWithIdentity.Models;

namespace UserManagementWithIdentity.Controllers
{
    [Authorize(Roles = Role.userRole)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        //To receive list of roles.
        Task<List<IdentityRole>> listOfRoles;


        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
            //Get roles and bring it in listOfRoles variable.
            listOfRoles = _RoleManager.Roles.ToListAsync();
        }
        public async Task<IActionResult> Index()
        {
            return View(await listOfRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //To Add new Role.
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), await listOfRoles);

            //Check to prevent adding existed role.
            if (await _RoleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists");
                return View(nameof(Index), await listOfRoles);
            }
            //Add the new role.
            await _RoleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return RedirectToAction(nameof(Index));
        }
    }
}
