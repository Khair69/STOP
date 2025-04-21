using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Services;

namespace STOP.Pages.Dog
{
    public class DeleteDogModel : PageModel
    {
        public Models.Dog DogEntity { get; set; }

        [FromForm]
        public Guid DogId { get; set; }

        private readonly IDogService _dogService;
        private readonly IAuthorizationService _authService;

        public DeleteDogModel(
            IDogService dogService,
            IAuthorizationService authService)
        {
            _dogService = dogService;
            _authService = authService;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            DogEntity = await _dogService.GetDogByIdAsync(id);
            AuthorizationResult authResult = await _authService.AuthorizeAsync(User, DogEntity, "CanDeleteDog");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            Console.WriteLine(DogEntity.Name);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("invalid");
                return Page();
            }

            var successful = await _dogService.DeleteDogAsync(DogId);

            if (!successful)
            {
                return BadRequest("Couldn't Delete Dog");
            }
            
            return RedirectToPage("Index");
        }
        
    }
}
