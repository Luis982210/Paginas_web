using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels;

namespace ProyectoPaginasWeb.Controllers
{
    public class SalasController : Controller
    {
        private readonly ProyectoPaWContext _context;

        public SalasController(ProyectoPaWContext context)
        {
            _context = context;
        }
        static string url = "https://localhost:7135";
        // GET: Salas
        public async Task<IActionResult> Index()
        {
            
            HttpClient client = new HttpClient();
            
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Sala>>(url + "/api/Salas");
            if (salas != null)
            { 
                Console.WriteLine("todo bien conectando con API"+url);
            }
            return View(salas);

        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Sala>>(url + "/api/Salas/");

            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }
            Console.WriteLine("todo bien conectando con API" + url);
            return View(sala);
        }

        // GET: Salas/Create
        public async Task <IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync(url + "/api/Create");
            if (res != null)
            {
                Console.WriteLine("todo bien conectando con API" + url);
            }
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSala,Ubicacion,Encargado,NoPersonal,Departamento")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Sala>>(url + "/api/Salas");
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            Console.WriteLine("todo bien conectando con API" + url);
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreSala,Ubicacion,Encargado,NoPersonal,Departamento")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
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
            return View(sala);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salas == null)
            {
                return Problem("Entity set 'ProyectoPaWContext.Salas'  is null.");
            }
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
          return _context.Salas.Any(e => e.Id == id);
        }
    }
}
