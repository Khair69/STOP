using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STOP.Models;

namespace STOP.Pages.Accounts
{
    [Authorize("IsAdmin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<(ApplicationUser User, string FavColor)> UsersWithClaims { get; set; }
        public List<(ApplicationUser User, string FavColor)> AdminsWithClaims { get; set; }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            UsersWithClaims = new List<(ApplicationUser, string)>();
            AdminsWithClaims = new List<(ApplicationUser, string)>();

            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);

                var colorClaim = claims.FirstOrDefault(c => c.Type == "FavColor");
                var isAdminClaim = claims.FirstOrDefault(c => c.Type == "IsAdmin");
                if (isAdminClaim != null)
                { 
                AdminsWithClaims.Add((user, colorClaim?.Value ?? "Not set"));
                }
                else
                {
                UsersWithClaims.Add((user, colorClaim?.Value ?? "Not set"));
                }


            }
        }
    }
}
