using ArkanoidApi.DataBaseContext;
using ArkanoidApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ArkanoidApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BallSkinsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BallSkinsController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все скины мячей
        [HttpGet]
        public IActionResult GetAll()
        {
            var ballSkins = _context.BallSkins.ToList();
            return new OkObjectResult(new { ballSkins = ballSkins });
        }

        // Получить скин мяча по ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ballSkin = _context.BallSkins.Find(id);
            if (ballSkin == null) return NotFound();
            return Ok(ballSkin);
        }

        // Создать новый скин мяча
        [HttpPost]
        public IActionResult Create([FromBody] BallSkin ballSkin)
        {
            _context.BallSkins.Add(ballSkin);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = ballSkin.Id }, ballSkin);
        }

        // Обновить скин мяча
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BallSkin ballSkin)
        {
            var existingBallSkin = _context.BallSkins.Find(id);
            if (existingBallSkin == null) return NotFound();

            existingBallSkin.Name = ballSkin.Name;
            existingBallSkin.Description = ballSkin.Description;
            existingBallSkin.Price = ballSkin.Price;

            _context.SaveChanges();
            return NoContent();
        }

        // Удалить скин мяча
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ballSkin = _context.BallSkins.Find(id);
            if (ballSkin == null) return NotFound();

            _context.BallSkins.Remove(ballSkin);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
