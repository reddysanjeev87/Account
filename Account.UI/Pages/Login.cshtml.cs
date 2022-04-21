using Account.UI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Account.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login Model { get; set; }
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string returnUrl = string.Empty;
            if (ModelState.IsValid)
            {
                var result=await this.signInManager.PasswordSignInAsync(Model.Email, Model.Password,Model.RememberMe,false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl) || returnUrl=="/")
                    {
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name or Password is incorrect");
                }
            }
            return Page();
        }
    }
}
