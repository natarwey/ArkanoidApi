using ArkanoidApi.DataBaseContext;
using ArkanoidApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ArkanoidApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoinsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCoins(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            return Ok(new { coins = user.Coins });
        }

        [HttpPut("{userId}/put")]
        public IActionResult AddCoins(int userId, [FromBody] AddCoinsRequest request)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            user.Coins += request.Coins;
            _context.SaveChanges();

            return Ok(new { coins = user.Coins });
        }
    }

    public class AddCoinsRequest
    {
        public int Coins { get; set; }
    }
}
