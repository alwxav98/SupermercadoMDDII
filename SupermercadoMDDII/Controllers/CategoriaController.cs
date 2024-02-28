using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;


namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class CategoriaController : Controller
    {
        private readonly SupermercadoContext _context;

        public CategoriaController(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();

            }
            return View(categoria);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idcategoria, descripcion")] CategoriaModel categoria)
        {
            if (id != categoria.Idcategoria)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FirstOrDefaultAsync(ca => ca.Idcategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("descripcion")] CategoriaModel categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);

        }
    }
}
