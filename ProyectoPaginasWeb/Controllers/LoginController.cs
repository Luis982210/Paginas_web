using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using ProyectoModels;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ProyectoModels.Models;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ProyectoPaginasWeb.Controllers
{
    
    public class LoginController : Controller
    {
        private IConfiguration config;
        
        public IActionResult Index()
        {
            return View();
        }
        public LoginController(IConfiguration config)
        { 
            this.config = config;
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            SqlConnection con = new SqlConnection("Server=LUIS\\MSSQLSERVER01; Database=ProyectoPaW2; Integrated Security=sspi;trusted_connection=true;TrustServerCertificate=True");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Usuarios WHERE email='" + email + "'AND contrasena = '" + password + "'AND permisos=1", con);
            DataTable dt = new DataTable("contador");
            Console.WriteLine(email);
            Console.WriteLine(password);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                
                Console.WriteLine("ENCONTRADO");
                var claims = new List<Claim>();
                claims.Add(new Claim("password", password));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1234"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM Usuarios WHERE email='" + email + "'AND contrasena = '" + password + "'AND permisos=2", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    Console.WriteLine("ENCONTRADO");
                    return Redirect("/Salas");
                }
                else
                {
                    ViewBag.Email = "Ingresa tus credenciales nuevamente";
                    Console.WriteLine("NO ENCONTRADO");
                    return Redirect("/Login");

                }
            }

            

        }
        public async Task<IActionResult> login(string email, string contrasena)
        {
            SqlConnection con = new SqlConnection("Server=LUIS\\MSSQLSERVER01; Database=ProyectoPaW; Integrated Security=sspi;trusted_connection=true;TrustServerCertificate=True");
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Usuarios WHERE email='" + email + "'AND contrasena = '" + contrasena + "'AND permisos=1", con);
            DataTable dt = new DataTable("contador");
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("ENCONTRADO");
                var claims=new List<Claim>();
                claims.Add(new Claim("contrasena", contrasena));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1234"));
                var claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "login");
                
            }
            else
            {
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM Usuarios WHERE email='" + email + "'AND contrasena = '" + contrasena + "'AND permisos=2", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    Console.WriteLine("ENCONTRADO");
                    return Redirect("/Salas");
                }
                else
                {
                    ViewBag.Email = "Ingresa tus credenciales nuevamente";
                    Console.WriteLine("NO ENCONTRADO");
                    return Redirect("/Login");

                }
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "login");
        }



        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)

        {

            var _symmetricSecurityKey = new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes(config["JWT:SecretKey"])

                );

            var _signingCredentials = new SigningCredentials(

                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256

                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName)

            };

            var _Payload = new JwtPayload(

                    issuer: config["JWT:Issuer"],

                    audience: config["JWT:Audience"],

                    claims: _Claims,

                    notBefore: DateTime.UtcNow,

                    expires: token_expiration

                );

            var _Token = new JwtSecurityToken(

                    _Header,

                    _Payload

                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}