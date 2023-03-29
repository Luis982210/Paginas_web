using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels;

namespace ProyectoPaginasWeb.Controllers
{
    public class MobiliariosController : Controller
    {
        private readonly ProyectoPaWContext _context;

        public MobiliariosController(ProyectoPaWContext context)
        {
            _context = context;
        }

        // GET: Mobiliarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mobiliarios.ToListAsync());
        }

        // GET: Mobiliarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mobiliarios == null)
            {
                return NotFound();
            }

            var mobiliario = await _context.Mobiliarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobiliario == null)
            {
                return NotFound();
            }

            return View(mobiliario);
        }

        // GET: Mobiliarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mobiliarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdSala,Mobiliario1,Precio,Cantidad,Descripcion,CodigoMobiliario")] Mobiliario mobiliario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobiliario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mobiliario);
        }

        // GET: Mobiliarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mobiliarios == null)
            {
                return NotFound();
            }

            var mobiliario = await _context.Mobiliarios.FindAsync(id);
            if (mobiliario == null)
            {
                return NotFound();
            }
            return View(mobiliario);
        }

        // POST: Mobiliarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdSala,Mobiliario1,Precio,Cantidad,Descripcion,CodigoMobiliario")] Mobiliario mobiliario)
        {
            if (id != mobiliario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobiliario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobiliarioExists(mobiliario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mobiliario);
        }

        // GET: Mobiliarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mobiliarios == null)
            {
                return NotFound();
            }

            var mobiliario = await _context.Mobiliarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobiliario == null)
            {
                return NotFound();
            }

            return View(mobiliario);
        }

        // POST: Mobiliarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mobiliarios == null)
            {
                return Problem("Entity set 'ProyectoPaWContext.Mobiliarios'  is null.");
            }
            var mobiliario = await _context.Mobiliarios.FindAsync(id);
            if (mobiliario != null)
            {
                _context.Mobiliarios.Remove(mobiliario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobiliarioExists(int id)
        {
          return _context.Mobiliarios.Any(e => e.Id == id);
        }
    }
}
