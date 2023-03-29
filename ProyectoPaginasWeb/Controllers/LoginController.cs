using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using ProyectoModels;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPaginasWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            SqlConnection con = new SqlConnection("Server=LUIS\\MSSQLSERVER01; Database=ProyectoPaW; Integrated Security=sspi;trusted_connection=true;TrustServerCertificate=True");
            SqlDataAdapter da=new SqlDataAdapter("SELECT * FROM Usuarios WHERE email='"+email+"'AND contrasena = '"+password+"'AND permisos=1",con);
            DataTable dt=new DataTable("contador");
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("ENCONTRADO");
                return Redirect("/Usuarios");
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
    }
}
