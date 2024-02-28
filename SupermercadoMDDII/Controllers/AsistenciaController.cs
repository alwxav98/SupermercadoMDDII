using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;

namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class AsistenciaController : Controller
    {
        private readonly SupermercadoContext _context;

        public AsistenciaController(SupermercadoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Asistencias.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            //Select Inner Join
            var asistencia = await _context.Asistencias.Where(p => p.Idasistencia == id)
                .Include(pr => pr.Usuarios).FirstOrDefaultAsync();



            if (asistencia == null)
            {
                return NotFound();

            }
            ViewData["ListaUsuarios"] = new SelectList(_context.Usuarios, "Idusuario", "cedula");
            return View(asistencia);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idasistencia,Idusuario,ingreso,salida,horasExtra")] AsistenciaModel asistencia)
        {
            if (id != asistencia.Idasistencia)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                // Obtener la asistencia actual de la base de datos
                

                asistencia.salida = DateTime.Now;
                // Calcular la diferencia entre ingreso y salida
                TimeSpan duracion = asistencia.salida - asistencia.ingreso;
                double horasTrabajadas = duracion.TotalHours;
                double horasExtraOP = horasTrabajadas - 8;

                if (horasExtraOP< 0)
                {
                    asistencia.horasExtra = 0;
                }
                else
                {
                    asistencia.horasExtra=horasExtraOP;
                }
                //asistencia.horasExtra = duracion.TotalHours;
                _context.Update(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asistencia);
        }
        


        public IActionResult Create()
        {
            ViewData["ListaUsuarios"] = new SelectList(_context.Usuarios, "Idusuario", "cedula");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Idusuario,ingreso,salida,horasExtra")] AsistenciaModel asistencia)
        {
            if (ModelState.IsValid)
            {
                asistencia.ingreso = DateTime.Now;
                asistencia.salida = DateTime.Now;
                // Calcular la diferencia entre ingreso y salida
                TimeSpan duracion = asistencia.salida - asistencia.ingreso;
                double horasTrabajadas = duracion.TotalHours;
                double horasExtraOP = horasTrabajadas - 8;

                if (horasExtraOP < 0)
                {
                    asistencia.horasExtra = 0;
                }
                else
                {
                    asistencia.horasExtra = horasExtraOP;
                }
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asistencia);

        }
    }
}
