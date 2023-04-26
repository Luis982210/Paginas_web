using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;

namespace ProyectoPaginasWeb.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class MobiliariosController : Controller
    {
        private readonly ProyectoPaW2Context _context;

        public MobiliariosController(ProyectoPaW2Context context)
        {
            _context = context;
        }
        static string url = "https://localhost:8082";
        // GET: Mobiliarios
        public async Task<IActionResult> Index()
        {
            ProyectoPaW2Context _moviesContext = new ProyectoPaW2Context();
            IEnumerable<ProyectoModels.Models.Mobiliario> moviescategories =
                (from mc in _moviesContext.Mobiliarios
                 join m in _moviesContext.Salas on mc.IdSala equals m.IdSala
                 select new ProyectoModels.Models.Mobiliario
                 {
                     IdMobiliario=mc.IdMobiliario,
                     IdSala = m.IdSala,
                     Nombre = mc.Nombre,
                     Precio = mc.Precio,
                     Descripcion= mc.Descripcion
                 }).ToList();
           
            return View(moviescategories);

        }

        // GET: Mobiliarios/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Sala>>(url + "/api/Mobiliarios");
            if (id == null || _context.Mobiliarios == null)
            {
                return NotFound();
            }

            var mobiliario = await _context.Mobiliarios
                .FirstOrDefaultAsync(m => m.IdMobiliario == id);
            if (mobiliario == null)
            {
                return NotFound();
            }

            return View(mobiliario);
        }

        // GET: Mobiliarios/Create

        public IActionResult Create()
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync(url + "/api/Create");
            return View();
        }

        // POST: Mobiliarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMobiliario,IdSala,Nombre,Precio,Descripcion")] Mobiliario mobiliario)
        {
            HttpClient client = new HttpClient();

           // if (ModelState.IsValid)
           // {
                var response = await client.PostAsJsonAsync<Mobiliario>(url + "/api/Mobiliarios", mobiliario);
                Console.WriteLine("todo bien conectando con API" + url);
           // }
            /*else
            {
                ViewData["IdMobiliario"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Mobiliarios");
                Console.WriteLine(" conectando con API" + url);
                return View(mobiliario);
            }*/


            return RedirectToAction(nameof(Index));
        }

        // GET: Mobiliarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Sala>>(url + "/api/Mobiliarios");
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
        public async Task<IActionResult> Edit(int id, [Bind("IdMobiliario,IdSala,Nombre,Precio,Descripcion")] Mobiliario mobiliario)
        {
            HttpClient client = new HttpClient();
            if (id != mobiliario.IdMobiliario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var response = await client.PutAsJsonAsync(url + "/api/Mobiliarios/" + mobiliario.IdMobiliario.ToString(), mobiliario);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMobiliario"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Mobiliarios");
            return View(mobiliario);
        }

            // GET: Mobiliarios/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            if (id == null || _context.Mobiliarios == null)
            {
                return NotFound();
            }

            var sala = await client.GetFromJsonAsync<Mobiliario>(url + "/api/Mobiliarios/" + id.ToString());
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Mobiliarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();
            if (_context.Mobiliarios == null)
            {
                return Problem("Entity set is null.");
            }
            Console.WriteLine("hola");
            var response = await client.DeleteFromJsonAsync<Mobiliario>(url + "/api/Mobiliarios/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

        private bool MobiliarioExists(int id)
        {
          return (_context.Mobiliarios?.Any(e => e.IdMobiliario == id)).GetValueOrDefault();
        }
    }
}
