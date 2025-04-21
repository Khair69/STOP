using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Services;

namespace STOP.Pages.Dog
{
    public class EditDogModel : PageModel
    {
        public Models.Dog DogEntity { get; set; }

        private readonly IDogService _dogService;
        private readonly IAuthorizationService _authService;

        public EditDogModel(
            IDogService dogService,
            IAuthorizationService authService)
        {
            _dogService = dogService;
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            DogEntity = await _dogService.GetDogByIdAsync(id);
            AuthorizationResult authResult = await _authService.AuthorizeAsync(User, DogEntity, "CanManageDog");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            return Page();
        }

        [BindProperty]
        public Models.Dog EditedDog { get; set; }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            EditedDog.DogId = id;
            var successful = await _dogService.EditDogAsync(EditedDog);

            if (!successful)
            {
                return BadRequest("Couldn't Update Dog");
            }

            return RedirectToPage("Index");
        }
    }
}
