using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AppointmentBookingApp.Domain.Entities;
using AppointmentBookingApp.Application.Auth.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AppointmentBookingApp.Application.Professionals;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IProfessionalService _professionalService;

    public AuthController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        RoleManager<IdentityRole> roleManager, 
        IConfiguration configuration, 
        IProfessionalService professionalService
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _professionalService = professionalService;
    }

    /*[HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        // Check if the role is valid
        if (model.Role != "User" && model.Role != "Professional")
        {
            return BadRequest("Invalid role. Allowed roles: User, Professional.");
        }

        // Check if the role exists, if not create it
        var roleExists = await _roleManager.RoleExistsAsync(model.Role);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Role));
        }

        var existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null)
        {
            return BadRequest("Username already exists");
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, model.Role);


        return Ok(new
        {
            user = new { user.UserName, user.FullName, role = model.Role }
        });
    }*/

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        //if (model.Role != "User" && model.Role != "Professional")
        //{
        //    return BadRequest("Invalid role. Allowed roles: User, Professional.");
        //}

        var roleExists = await _roleManager.RoleExistsAsync(model.Role);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Role));
        }

        var existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null)
        {
            return BadRequest("Username already exists");
        }

        var existingEmail = await _userManager.FindByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            return BadRequest("Email already exists");
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            FullName = model.FullName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {

            var errorMessage = string.Join("  ", result.Errors.Select(e => e.Description));
            return BadRequest(errorMessage);
        }

        await _userManager.AddToRoleAsync(user, model.Role);

        if (model.Role == "Professional")
        {
            var professional = new Professional
            {
                UserId = user.Id, 
                Name = model.FullName,
                Email = model.Email,
                Specialization = model.Specialization, 
                PhoneNumber = model.PhoneNumber  
            };

            bool isProfessionalCreated = await _professionalService.CreateProfessional(professional);

            if (!isProfessionalCreated)
            {
                return BadRequest("Failed to create professional entry");
            }
        }

        return Ok(new RegisterResponseDto
        {
            IsSuccess = true,
            Message = "Registration successful"
        });
    }



    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password");
        }

        var token = await GenerateJwtToken(user);

        return Ok(new
        {
            token = token,
            user = new { user.UserName, user.FullName }
        });
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "User"; 

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Role, role), 
        new Claim("FullName", user.FullName)
    };

        var key = new SymmetricSecurityKey(secretKey);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpGet("users")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUsers()
    {
        var users = _userManager.Users.ToList();
        var professionals = await _professionalService.GetAllProfessionalsAsync();

        var userList = users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
            Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
            Specialization = professionals.FirstOrDefault(p => p.UserId == user.Id)?.Specialization
        }).ToList();

        return Ok(userList);
    }


    //private string GenerateJwtToken(ApplicationUser user)
    //{
    //    var jwtSettings = _configuration.GetSection("Jwt");
    //    var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.Name, user.UserName),
    //        new Claim(ClaimTypes.NameIdentifier, user.Id),
    //        new Claim(ClaimTypes.Role, "User"),  // You can add additional claims, such as roles
    //        new Claim("FullName", user.FullName)
    //    };

    //    var key = new SymmetricSecurityKey(secretKey);
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: jwtSettings["Issuer"],
    //        audience: jwtSettings["Audience"],
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddDays(1),
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    [HttpGet("roles")]
    [AllowAnonymous]
    public IActionResult GetRoles()
    {
        var roles = _roleManager.Roles.Select(r => r.Name).ToList();

        if (roles.Count == 0)
        {
            return NotFound("No roles found.");
        }

        return Ok(roles);
    }
}
