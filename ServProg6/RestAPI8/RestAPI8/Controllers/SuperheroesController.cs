using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI8.Data;
using RestAPI8.Models;

namespace RestAPI8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroesController : ControllerBase
    {
        private readonly SuperheroesContext _context;

        public SuperheroesController(SuperheroesContext context)
        {
            _context = context;
        }

        // GET: api/Superheroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Superhero>>> GetSuperheroes()
        {
            return await _context.Superheroes
                .Include(s => s.Alignment)
                .Include(s => s.EyeColour)
                .Include(s => s.Gender)
                .Include(s => s.HairColour)
                .Include(s => s.Publisher)
                .Include(s => s.Race)
                .Include(s => s.SkinColour)
                .ToListAsync();
        }

        // GET: api/Superheroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> GetSuperhero(int id)
        {
            var superhero = await _context.Superheroes
                .Include(s => s.Alignment)
                .Include(s => s.EyeColour)
                .Include(s => s.Gender)
                .Include(s => s.HairColour)
                .Include(s => s.Publisher)
                .Include(s => s.Race)
                .Include(s => s.SkinColour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (superhero == null)
            {
                return NotFound();
            }

            return superhero;
        }

        // POST: api/Superheroes
        [HttpPost]
        public async Task<ActionResult<Superhero>> PostSuperhero(Superhero superhero)
        {
            _context.Superheroes.Add(superhero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSuperhero), new { id = superhero.Id }, superhero);
        }

        // PUT: api/Superheroes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperhero(int id, Superhero superhero)
        {
            if (id != superhero.Id)
            {
                return BadRequest();
            }

            // Ensure the AlignmentId is valid
            var alignmentExists = await _context.Alignments.AnyAsync(a => a.Id == superhero.AlignmentId);
            if (!alignmentExists)
            {
                return BadRequest("Invalid AlignmentId");
            }

            // Attach the entity to the context and set its state to Modified
            _context.Superheroes.Attach(superhero);
            _context.Entry(superhero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperheroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"An error occurred while updating the superhero: {ex.Message}");
                throw;
            }

            return NoContent();
        }


        // DELETE: api/Superheroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperhero(int id)
        {
            var superhero = await _context.Superheroes.FindAsync(id);
            if (superhero == null)
            {
                return NotFound();
            }

            var heroAttributes = _context.HeroAttributes.Where(ha => ha.HeroId == id);
            _context.HeroAttributes.RemoveRange(heroAttributes);

            var heroPowers = _context.HeroPowers.Where(hp => hp.HeroId == id);
            _context.HeroPowers.RemoveRange(heroPowers);

            _context.Superheroes.Remove(superhero);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperheroExists(int id)
        {
            return _context.Superheroes.Any(e => e.Id == id);
        }
    }
}
