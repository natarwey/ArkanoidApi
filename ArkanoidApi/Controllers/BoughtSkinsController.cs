using ArkanoidApi.DataBaseContext;
using ArkanoidApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArkanoidApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoughtSkinsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoughtSkinsController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все купленные скины пользователя
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var boughtSkins = _context.BoughtSkins
                .Include(bs => bs.BallSkin)
                .Where(bs => bs.UserId == userId)
                .ToList();

            return Ok(boughtSkins);
        }

        // Добавить новый купленный скин
        [HttpPost]
        public IActionResult Create([FromBody] BoughtSkins boughtSkin)
        {
            // Проверяем существует ли пользователь и скин
            var userExists = _context.Users.Any(u => u.Id == boughtSkin.UserId);
            var skinExists = _context.BallSkins.Any(bs => bs.Id == boughtSkin.SkinId);

            if (!userExists || !skinExists)
            {
                return BadRequest("User or Skin does not exist");
            }

            _context.BoughtSkins.Add(boughtSkin);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = boughtSkin.Id }, boughtSkin);
        }

        // Получить конкретную запись о купленном скине
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var boughtSkin = _context.BoughtSkins
                .Include(bs => bs.BallSkin)
                .FirstOrDefault(bs => bs.Id == id);

            if (boughtSkin == null) return NotFound();

            return Ok(boughtSkin);
        }

        // Удалить запись о купленном скине
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var boughtSkin = _context.BoughtSkins.Find(id);
            if (boughtSkin == null) return NotFound();

            _context.BoughtSkins.Remove(boughtSkin);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
