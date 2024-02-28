using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;
using System.Security.Cryptography;
using System.Text;


namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class UsuarioController : Controller
    {
        private readonly SupermercadoContext _context;

        public UsuarioController(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Desencriptar la clave antes de pasarla a la vista si no es nula
            //if (!string.IsNullOrEmpty(usuario.clave))
            //{
            //    usuario.clave = DesencriptarClave(usuario.clave);
            //}

            return View(usuario);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idusuario, cedula, nombre,apellido,direccion,telefono,correo,sueldo,rol,clave,esActivo,fechaRegistro")] UsuarioModel usuario)
        {
            if (id != usuario.Idusuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //if (!string.IsNullOrEmpty(usuario.clave))
                //{
                //    usuario.clave = EncriptarClave(usuario.clave);
                //}
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.Idusuario == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Handle error
                ModelState.AddModelError("", "Ha ocurrido un error al intentar eliminar el usuario.");
                return View(usuario); // o redirigir a una vista de error
            }
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("cedula, nombre,apellido,direccion,telefono,correo,sueldo,rol,clave,esActivo,fechaRegistro")] UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                //if (usuario.clave != null)
                //{
                //    string claveEncriptada = EncriptarClave(usuario.clave);
                //    usuario.clave = claveEncriptada;
                //}
                //else
                //{
                //    Console.WriteLine("La clave del usuario es nula. No se puede encriptar.");
                //}
                // Asignar la fecha y hora actual automáticamente al campo fechaRegistro
                usuario.fechaRegistro = DateTime.Now;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);

        }
       
        //public string EncriptarClave(string clave)
        //{
        //    string hash = "coding con c";
        //    byte[] data = UTF8Encoding.UTF8.GetBytes(clave);

        //    MD5 md5 = MD5.Create();
        //    TripleDES tripides = TripleDES.Create();

        //    tripides.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //    tripides.Mode = CipherMode.ECB;

        //    ICryptoTransform transform = tripides.CreateEncryptor();
        //    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
        //    return Convert.ToBase64String(result);
        //}

        //public string DesencriptarClave(string claveEn)
        //{
        //    string hash = "coding con c";
        //    byte[] data = Convert.FromBase64String(claveEn);

        //    MD5 md5 = MD5.Create();
        //    TripleDES tripides = TripleDES.Create();

        //    tripides.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //    tripides.Mode = CipherMode.ECB;


        //    ICryptoTransform transform = tripides.CreateDecryptor();
        //    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

        //    return UTF8Encoding.UTF8.GetString(result);

        //}


    }
}
