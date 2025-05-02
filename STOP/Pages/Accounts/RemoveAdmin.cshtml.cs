using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Services;

namespace STOP.Pages.Accounts
{
    [Authorize("IsAdmin")]
    public class RemoveAdminModel : PageModel
    {
        private readonly IUserService _userService;

        public RemoveAdminModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var succesful = await _userService.RemoveAdminAsync(Id);
            if (!succesful)
            {
                return BadRequest("Couldn't remove the admin");
            }
            return RedirectToPage("Index");
        }
    }
}
