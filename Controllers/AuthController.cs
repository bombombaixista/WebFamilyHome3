using Microsoft.AspNetCore.Mvc;
using WebFamilyHome.Data;
using WebFamilyHome.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebFamilyHome.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Função para gerar hash da senha
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, int groupId)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                GroupId = groupId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || user.PasswordHash != HashPassword(password))
                return Unauthorized("Usuário ou senha inválidos");

            return Ok(new { user.Username, Grupo = user.Group?.Name });
        }
    }
}
