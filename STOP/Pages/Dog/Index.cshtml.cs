using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Services;

namespace STOP.Pages.Dog
{
    public class IndexModel : PageModel
    {
        private readonly IDogService _dogService;

        public IndexModel(IDogService dogService)
        {
            _dogService = dogService;
        }

        public Models.Dog[] Dogs { get; set; }
        public bool IsSingedIn { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IsSingedIn = User.Identity.IsAuthenticated;
            Dogs = await _dogService.GetDogsAsync();
            return Page();
        }

    }
}
