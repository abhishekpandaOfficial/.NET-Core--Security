using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credentials Credential { get; set; }
        public void OnGet()
        {
            //this.Credential = new Credentials { Username = "admin" };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Credential.Username == "admin" && Credential.Password == "password")
                {
                    // Creating our Security Context 
                    // ---- Claims -----------
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"admin"),
                    new Claim(ClaimTypes.Email,"admin@gmail.com")
                };
                    // ---- Identity -----------
                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                    // ---- Principal -----------
                    // ------- Now our Security context is under "claimsPrincipal" ------
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    // ------- Now we want to store in Cookie while signin  ------
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                    return RedirectToPage("/index");
                }
            }
            return Page();
        }

    }

    public class Credentials
    {
        [Required]
        [Display(Name ="User Name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
