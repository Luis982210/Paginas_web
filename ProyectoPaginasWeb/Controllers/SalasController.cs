using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoModels.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.Resources;
using System.Security.Policy;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProyectoPaginasWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SalasController : Controller
    {
        private readonly ProyectoPaW2Context _context;

        public SalasController(ProyectoPaW2Context context)
        {
            _context = context;
        }
        static string url = "https://localhost:7135";
        // GET: Salas
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Sala>>(url + "/api/Salas");
            if (salas != null)
            {
                Console.WriteLine("todo bien conectando con API" + url);
            }
            return View(salas);
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Sala>>(url + "/api/Salas/");

            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.IdSala == id);
            if (sala == null)
            {
                return NotFound();
            }
            Console.WriteLine("todo bien conectando con API" + url);
            return View(sala);
        }

        // GET: Salas/Create
        public IActionResult Create()
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
        public async Task<IActionResult> Create([Bind("IdSala,Ubicacion,Encargado,Departamento")] Sala sala)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync<Sala>(url + "/api/Salas", sala);
                Console.WriteLine("todo bien conectando con API" + url);
            }
            else
            {
                ViewData["IdSala"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Salas");
                Console.WriteLine(" conectando con API" + url);
                return View(sala);
            }


            return RedirectToAction(nameof(Index));

        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Sala>>(url + "/api/Salas");
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
        public async Task<IActionResult> Edit(int id, [Bind("IdSala,Ubicacion,Encargado,Departamento")] Sala sala)
        {
            HttpClient client = new HttpClient();
            if (id != sala.IdSala)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var response = await client.PutAsJsonAsync(url + "/api/Salas/" + sala.IdSala.ToString(), sala);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSala"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Salas");
            return View(sala);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await client.GetFromJsonAsync<Sala>(url + "/api/Salas/" + id.ToString());
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
            HttpClient client = new HttpClient();
            if (_context.Salas == null)
            {
                return Problem("Entity set is null.");
            }
            Console.WriteLine("hola");
            var response = await client.DeleteFromJsonAsync<Sala>(url + "/api/Salas/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

    

        private bool SalaExists(int id)
        {
          return (_context.Salas?.Any(e => e.IdSala == id)).GetValueOrDefault();
        }
    }
}
