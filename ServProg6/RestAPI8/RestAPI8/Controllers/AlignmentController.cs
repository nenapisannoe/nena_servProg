using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RestAPI8.Models;
using RestAPI8.Data;

namespace RestAPI8.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlignmentController : ControllerBase
    {
        private readonly SuperheroesContext _context;

        public AlignmentController(SuperheroesContext context)
        {
            _context = context;
        }

        // GET: api/Alignment
        [HttpGet]
        //[Authorize(Roles = "user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Alignment>>> GetAlignments()
        {
            return await _context.Alignments.ToListAsync();
        }

        // GET: api/Alignment/5
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<Alignment>> GetAlignment(int id)
        {
            var alignment = await _context.Alignments.FindAsync(id);

            if (alignment == null)
            {
                return NotFound();
            }

            return alignment;
        }

        // PUT: api/Alignment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutAlignment(int id, Alignment alignment)
        {
            if (id != alignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(alignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlignmentExists(id))
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

        // POST: api/Alignment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Alignment>> PostAlignment(Alignment alignment)
        {
            _context.Alignments.Add(alignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlignment", new { id = alignment.Id }, alignment);
        }

        // DELETE: api/Alignment/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAlignment(int id)
        {
            var alignment = await _context.Alignments.FindAsync(id);
            if (alignment == null)
            {
                return NotFound();
            }

            bool hasRelatedSuperheroes = await _context.Superheroes
                                                       .AnyAsync(s => s.AlignmentId == id);

            if (hasRelatedSuperheroes)
            {
                return Conflict(new { message = "Cannot delete alignment because there are superheroes associated with it." });
            }

            _context.Alignments.Remove(alignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool AlignmentExists(int id)
        {
            return _context.Alignments.Any(e => e.Id == id);
        }
    }
}
