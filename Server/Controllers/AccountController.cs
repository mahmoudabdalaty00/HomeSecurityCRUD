//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Server.Models.Identities;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IConfiguration _configuration;

//        public AccountController(
//            UserManager<IdentityUser> userManager
//            , RoleManager<IdentityRole> roleManager,
//            IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] Register model)
//        {
//            var user = new IdentityUser
//            {
//                UserName = model.Email,
//                Email = model.Email
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded)
//            {
//                return Ok(new { message = "User registered Successfully" });
//            }

//            return BadRequest(result.Errors);
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] Login model)
//        {
//            var user = await _userManager.FindByNameAsync(model.UserName);
//            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//            {
//                var userRole = await _userManager.GetRolesAsync(user);

//                var authClaims = new List<Claim>
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                };

//                authClaims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));

//                var token = new JwtSecurityToken(
//                    issuer: _configuration["JWT:Issuer"],
//                    //we dont need audence for now
//                    //  audience: _configuration["JWT:ValidAudience"],
//                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
//                    claims: authClaims,
//                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
//                    SecurityAlgorithms.HmacSha256)
//                );
//                return Ok(new
//                {
//                    token = new JwtSecurityTokenHandler().WriteToken(token),
//                    expiration = token.ValidTo
//                });
//            }
//            //return BadRequest(new { message = "Email or password is incorrect" });
//            return Unauthorized();
//        }

//        [HttpPost("add-role")]
//        public async Task<IActionResult> AddRole([FromBody] string role)
//        {
//            var roleExist = await _roleManager.RoleExistsAsync(role);
//            if (!roleExist)
//            {
//                var result = await _roleManager.CreateAsync(new IdentityRole(role));
//                if (result.Succeeded)
//                {
//                    return Ok(new {message = "Role Added Successfully"});
//                }
//                return BadRequest(result.Errors);

//            }
//            return BadRequest("Role already Exists");
//        }

//        [HttpPost("assign-role")]
//        public async Task<IActionResult> AssignRole([FromBody] UserRole roleAssignment)
//        {
//            var user = await _userManager.FindByNameAsync(roleAssignment.UserName);
//            var role = await _roleManager.FindByNameAsync(roleAssignment.Role);
//            if (user == null || role == null)
//            {
//                return BadRequest("User or Role does not exist");
//            }
//            var result = await _userManager.AddToRoleAsync(user, role.Name);
//            if (result.Succeeded)
//            {
//                return Ok(new { message = "Role Assigned Successfully" });
//            }
//            return BadRequest(result.Errors);
//        }
//    }
//}
