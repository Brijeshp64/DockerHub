using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskTodo.Data;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;
using TaskTodo.Services.intreface;

namespace TaskTodo.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _context;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _application;
        public UserController(IUser context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(AddUser req)
        {
            try
            {
                var result = await _context.RegisterUser(req);
                if (result.StatusCode == 200)
                {
                    return Ok("User Registered successfully");
                }
                else
                {
                    return BadRequest("Email already exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginRequest req)
        {
            try
            {
                var result = await _context.LoginUser(req);
                if (result.Result)
                {
                    var token = GenerateJwtToken(req.email, result.Role);
                    return Ok(new {
                        Token=token,
                        RoleId = result.Role
                        
                    });
                    //return Ok("logged in successfully");
                }
                else
                {
                    return Unauthorized();

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private string GenerateJwtToken(string email, string Role)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, Role),

    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
