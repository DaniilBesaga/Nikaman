using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Nikaman.Extensions;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nikaman.Pages.AdminPanel
{
    public class AdminPanelAccessModel : PageModel
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        static string? ip="";
        [BindProperty]
        public Password? P { get; set; }
        public AdminPanelAccessModel(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            var i = _cache.Get(ip);
            if(i!=null && (int)i >= 3)
            {
                return ViewComponent("AdminPanelDenied");
            }
            if(HttpContext.Session.Get("token")!=null)
            {
                return RedirectToPage("../AdminPanel/Index");
            }
            return Page();
        }
        public IActionResult OnPostSubmit()
        {
            string? s = P._Password;
            string? pass = _configuration["AdminPanelPassword"];
            pass = BCrypt.Net.BCrypt.HashPassword(pass);
            bool tr = BCrypt.Net.BCrypt.Verify(P._Password, pass);
            ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var cachedData = _cache.Get(ip);
            if (cachedData == null)
            {
                int a = 1;
                _cache.Set(ip, a, TimeSpan.FromMinutes(5));
            }
            else
            {
                int a = Convert.ToInt32(cachedData);
                ++a;
                _cache.Set(ip, a, TimeSpan.FromMinutes(5));
                if (a >= 3)
                    return ViewComponent("AdminPanelDenied");

            }
            if (tr)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my super puper secret code"));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim> { new Claim(ClaimTypes.UserData, P._Password) };
                var jwt = new JwtSecurityToken(
                        issuer: "Nikaman",
                        audience: "Nikaman",
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                        signingCredentials: credentials
                    );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                HttpContext.Session.Set("token", encodedJwt);
                return RedirectToPage("../AdminPanel/Index");
            }
            else
            {
                ViewData["Password"] = "Wrong password. Try again";
                return Page();
            }
        }
    }
    public class Password
    {
        [Required]
        [BindProperty]
        public string? _Password { get; set; }
    }
}
