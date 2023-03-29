using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobiliariosController : ControllerBase
    {
        private readonly ProyectoPaWContext _context;

        public MobiliariosController(ProyectoPaWContext context)
        {
            _context = context;
        }

        // GET: api/Mobiliarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mobiliario>>> GetMobiliarios()
        {
          if (_context.Mobiliarios == null)
          {
              return NotFound();
          }
            return await _context.Mobiliarios.ToListAsync();
        }

        // GET: api/Mobiliarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mobiliario>> GetMobiliario(int id)
        {
          if (_context.Mobiliarios == null)
          {
              return NotFound();
          }
            var mobiliario = await _context.Mobiliarios.FindAsync(id);

            if (mobiliario == null)
            {
                return NotFound();
            }

            return mobiliario;
        }

        // PUT: api/Mobiliarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMobiliario(int id, Mobiliario mobiliario)
        {
            if (id != mobiliario.Id)
            {
                return BadRequest();
            }

            _context.Entry(mobiliario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobiliarioExists(id))
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

        // POST: api/Mobiliarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mobiliario>> PostMobiliario(Mobiliario mobiliario)
        {
          if (_context.Mobiliarios == null)
          {
              return Problem("Entity set 'ProyectoPaWContext.Mobiliarios'  is null.");
          }
            _context.Mobiliarios.Add(mobiliario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMobiliario", new { id = mobiliario.Id }, mobiliario);
        }

        // DELETE: api/Mobiliarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMobiliario(int id)
        {
            if (_context.Mobiliarios == null)
            {
                return NotFound();
            }
            var mobiliario = await _context.Mobiliarios.FindAsync(id);
            if (mobiliario == null)
            {
                return NotFound();
            }

            _context.Mobiliarios.Remove(mobiliario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MobiliarioExists(int id)
        {
            return (_context.Mobiliarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
