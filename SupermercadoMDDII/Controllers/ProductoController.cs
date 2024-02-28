using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Implementacion;
using SupermercadoMDDII.Models;
using SupermercadoMDDII.Services;

namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class ProductoController : Controller
    {
        private readonly SupermercadoContext _context;
        private readonly IEmailService _emailService;

        public ProductoController(SupermercadoContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            //Select Inner Join
            var producto = await _context.Productos.Where(p => p.Idproducto == id)
                .Include(pr => pr.Categorias)
                .Include(pr => pr.Proveedors).FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();

            }
            ViewData["ListaCategorias"] = new SelectList(_context.Categorias, "Idcategoria", "descripcion");
            ViewData["ListaProveedores"] = new SelectList(_context.Proveedors, "Idproveedor", "ruc");
            return View(producto);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idproducto,codigoBarra,marca,descripcion,Idcategoria,Idproveedor,stock,precio,esActivo,fechaCaducidad")] ProductoModel producto)
        {
            if (id != producto.Idproducto)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _context.Update(producto);
           

                await _context.SaveChangesAsync();
                await VerificarStockYEnviarCorreo(id); // Verificar y enviar correo después de guardar los cambios
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FirstOrDefaultAsync(pr => pr.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var producto = await _context.Productos.FindAsync(id);
        //    _ = _context.Productos.Remove(producto);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            //var categoria = await _context.Categorias.FirstOrDefaultAsync(pr => pr.idCategoria == id);
            //_context.Categorias.Remove(categoria);
            //await _context.SaveChangesAsync();

            //var proveedor = await _context.Proveedors.FirstOrDefaultAsync(pr => pr.Idproveedor == id);
            //_context.Proveedors.Remove(proveedor);
            //await _context.SaveChangesAsync();

            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            ViewData["ListaCategorias"] = new SelectList(_context.Categorias,"Idcategoria","descripcion");
            ViewData["ListaProveedores"] = new SelectList(_context.Proveedors,"Idproveedor","ruc");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("codigoBarra,marca,descripcion,Idcategoria,Idproveedor,stock,precio,esActivo,fechaCaducidad")] ProductoModel producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);

        }

        // Método para verificar el stock y enviar correo si es necesario
        public async Task VerificarStockYEnviarCorreo(int productoId)
        {
            var producto = await _context.Productos
                .Include(p => p.Proveedors)
                .FirstOrDefaultAsync(p => p.Idproducto == productoId);

            if (producto == null)
            {
                return;
            }

            if (producto.stock < 10)
            {
                var emailRequest = new EmailDTO
                {
                    Para = producto.Proveedors.correo, 
                    Asunto = "Alerta de stock bajo",
                    Contenido = $"El producto {producto.descripcion} tiene un stock bajo. Por favor, reabastece el inventario."
                };

                _emailService.SendEmail(emailRequest);
            }
        }




    }
}
