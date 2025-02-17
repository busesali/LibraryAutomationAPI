using LibraryAutomationAPI.Data;
using LibraryAutomationAPI.Helpers;
using LibraryAutomationAPI.Models;
using LibraryAutomationAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryAutomationAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
                return BadRequest("Kullanıcı adı zaten mevcut!");

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                PasswordHash = HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kullanıcı başarıyla kaydedildi!");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (dbUser == null || dbUser.PasswordHash != HashPassword(loginDto.Password))
                return Unauthorized("Geçersiz kullanıcı adı veya şifre!");

            var token = _jwtService.GenerateToken(dbUser); 

            return Ok(new { Token = token });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
