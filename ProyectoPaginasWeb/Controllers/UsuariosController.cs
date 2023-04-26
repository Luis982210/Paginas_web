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
    public class UsuariosController : Controller
    {
        private readonly ProyectoPaW2Context _context;
        static string url = "https://localhost:7135";
        public UsuariosController(ProyectoPaW2Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Usuario>>(url + "/api/Usuarios");
            if (salas != null)
            {
                Console.WriteLine("todo bien conectando con API" + url);
            }
            return View(salas);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpClient client = new HttpClient();

            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Usuario>>(url + "/api/Usuarios");

            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var mobiliario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (mobiliario == null)
            {
                return NotFound();
            }

            return View(mobiliario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync(url + "/api/Create");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Email,Contrasena,Permisos")] Usuario usuario)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync<Usuario>(url + "/api/Usuarios", usuario);
                Console.WriteLine("todo bien conectando con API" + url);
            }
            else
            {
                ViewData["IdUsuario"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Usuarios");
                Console.WriteLine(" conectando con API" + url);
                return View(usuario);
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpClient client = new HttpClient();
            var salas = await client.GetFromJsonAsync<IEnumerable<ProyectoModels.Models.Usuario>>(url + "/api/Usuarios");
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var sala = await _context.Usuarios.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            Console.WriteLine("todo bien conectando con API" + url);
            return View(sala);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Email,Contrasena,Permisos")] Usuario usuario)
        {
            HttpClient client = new HttpClient();
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var response = await client.PutAsJsonAsync(url + "/api/Usuarios/" + usuario.IdUsuario.ToString(), usuario);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuarios"] = await client.GetFromJsonAsync<List<SelectListItem>>(url + "/api/Usuarios");
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var sala = await client.GetFromJsonAsync<Usuario>(url + "/api/Usuarios/" + id.ToString());
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();
            if (_context.Usuarios == null)
            {
                return Problem("Entity set is null.");
            }
            Console.WriteLine("hola");
            var response = await client.DeleteFromJsonAsync<Usuario>(url + "/api/Usuarios/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
