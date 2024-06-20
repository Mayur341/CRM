using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Controllers // Adjust namespace as per your application structure
{
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // Action to display all roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRoles();
            return View(roles);
        }

        // Action to create a new role
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError(string.Empty, "Role name cannot be empty.");
                return View();
            }

            var result = await _roleService.CreateRole(roleName);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Failed to create role '{roleName}'. Role already exists.");
                return View();
            }
        }

        // Action to delete a role
        [HttpPost]
        public async Task<IActionResult> Delete(string roleName)
        {
            var result = await _roleService.DeleteRole(roleName);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Failed to delete role '{roleName}'. Role not found.");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET action to assign roles to a user
        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            ViewBag.UserId = userId;
            ViewBag.AllRoles = await _roleService.GetAllRoles();
            ViewBag.UserRoles = await _roleService.GetRolesForUser(userId);

            return View();
        }

        // POST action to assign a role to a user
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError(string.Empty, "Role name cannot be empty.");
            }
            else
            {
                var result = await _roleService.AssignRoleToUser(userId, roleName);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, $"Failed to assign role '{roleName}' to user.");
                }
            }

            ViewBag.UserId = userId;
            ViewBag.AllRoles = await _roleService.GetAllRoles();
            ViewBag.UserRoles = await _roleService.GetRolesForUser(userId);

            return View();
        }

        // Action to remove a role from a user
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var result = await _roleService.RemoveRoleFromUser(userId, roleName);
            if (result)
            {
                return RedirectToAction(nameof(AssignRole), new { userId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Failed to remove role '{roleName}' from user.");
                return RedirectToAction(nameof(AssignRole), new { userId });
            }
        }

        // Action to get users in a specific role
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var users = await _roleService.GetUsersInRole(roleName);
            return View(users);
        }

        // Action to check if a role exists
        public async Task<IActionResult> RoleExists(string roleName)
        {
            var exists = await _roleService.RoleExists(roleName);
            return Json(new { exists });
        }

        // Action to get roles for a specific user
        public async Task<IActionResult> GetRolesForUser(string userId)
        {
            var roles = await _roleService.GetRolesForUser(userId);
            return View(roles);
        }
    }
}
