using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagementApp.Data;
using GymManagementApp.Data.Entities;

namespace GymManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerEntityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainerEntityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainerEntity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerEntity>>> GetTrainers()
        {
            return await _context.Trainers.Include(t => t.WorkoutClasses).ToListAsync();
        }

        // GET: api/TrainerEntity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerEntity>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.Include(t => t.WorkoutClasses).FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

        // PUT: api/TrainerEntity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, TrainerEntity trainer)
        {
            if (id != trainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Trainers.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrainerEntity
        [HttpPost]
        public async Task<ActionResult<TrainerEntity>> PostTrainer(TrainerEntity trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrainer), new { id = trainer.Id }, trainer);
        }

        // DELETE: api/TrainerEntity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
