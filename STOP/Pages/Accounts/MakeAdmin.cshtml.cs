using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Services;

namespace STOP.Pages.Accounts
{
    public class MakeAdminModel : PageModel
    {
        private readonly IUserService _userService;

        public MakeAdminModel(IUserService userService)
        {
            _userService = userService; 
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var succesful = await _userService.MakeAdminAsync(Id);
            if (!succesful)
            {
                return BadRequest("Couldn't make the user admin");
            }
            return RedirectToPage("Index");
        }
    }
}
