using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SupermercadoMDDII.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace SupermercadoMDDII.Controllers
{
    public class AccesoController : Controller
    {

        static string cadena = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=BaseSupermercado;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel oUsuario)
        {
            bool registrado;
            string mensaje;

            oUsuario.clave = ConvertirSha256(oUsuario.clave);


            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_registrarusuarioss", cn);

                cmd.Parameters.AddWithValue("@Nombre", oUsuario.nombre);
                cmd.Parameters.AddWithValue("@Cedula", oUsuario.cedula);
                cmd.Parameters.AddWithValue("@Apellido", oUsuario.apellido);
                cmd.Parameters.AddWithValue("@Direccion", oUsuario.direccion);
                cmd.Parameters.AddWithValue("@Telefono", oUsuario.telefono);
                cmd.Parameters.AddWithValue("@Correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("@Sueldo", 0);
                cmd.Parameters.AddWithValue("@Rol", "Usuario");
                cmd.Parameters.AddWithValue("@Clave", oUsuario.clave);
                cmd.Parameters.AddWithValue("@EsActivo", true);

                // Crear parámetro para la fecha de registro y establecer su valor como la fecha y hora actual
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);

                // Definir parámetros de salida
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                // Obtener valores de los parámetros de salida
                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }


            ViewData["Mensaje"] = mensaje;


            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(UsuarioModel oUsuario)
        {

            oUsuario.clave = ConvertirSha256(oUsuario.clave);


            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("@Correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("@Clave", oUsuario.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    oUsuario.Idusuario = Convert.ToInt32(result.ToString());
                    Console.WriteLine(oUsuario.Idusuario);
                }
                else
                {
                    // Manejar el caso en el que no se obtenga ningún valor de la consulta
                }


            }

            if (oUsuario.Idusuario != 0)
            {
                string usuarioJson = JsonConvert.SerializeObject(oUsuario);
                HttpContext.Session.SetString("usuario", usuarioJson);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }



            // return View();
        }


        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
