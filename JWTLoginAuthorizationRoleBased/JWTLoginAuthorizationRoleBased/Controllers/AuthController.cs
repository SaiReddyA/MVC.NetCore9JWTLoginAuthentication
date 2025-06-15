
namespace JWTLoginAuthorizationRoleBased.Controllers
{    
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequest login)
        {
            var user = _userService.Authenticate(login.Email, login.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return RedirectToAction("Login"); 
            }

            var token = _userService.GenerateJwtToken(user);

            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            if (user.Role == "Admin")
                return RedirectToAction("AdminPanel", "Home");
            else
                return RedirectToAction("UserPanel", "Home");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            Response.Cookies.Delete("jwtToken");
            return Redirect("/Auth/Login");
        }

    }
}
