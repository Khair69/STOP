using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Models;
using STOP.Services;

namespace STOP.Pages.Dog
{
    [Authorize]
    public class AddDogModel : PageModel
    {
        private readonly IDogService _dogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddDogModel(IDogService dogService,
            UserManager<ApplicationUser> userManager)
        {
            _dogService = dogService;
            _userManager = userManager;
        }

        [BindProperty]
        public Models.Dog InDog { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            InDog.OwnerId = currentUser.Id;

            var successful = await _dogService.AddDogAsync(InDog);

            if (!successful) 
            {
                return BadRequest("Could not add dog.");
            }

            return RedirectToPage("Index");
        }
    }
}
