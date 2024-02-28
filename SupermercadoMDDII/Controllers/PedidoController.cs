using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;

namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class PedidoController : Controller
    {
        private readonly SupermercadoContext _context;

        public PedidoController(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedidos.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            //Select Inner Join
            var pedido = await _context.Pedidos.Where(p => p.Idpedido == id)
                .Include(pr => pr.Productos)
                .Include(pr => pr.Proveedors).FirstOrDefaultAsync();

            if (pedido == null)
            {
                return NotFound();

            }

            ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
            ViewData["ListaProveedores"] = new SelectList(_context.Proveedors, "Idproveedor", "ruc");

            return View(pedido);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idpedido,Idproducto,Idproveedor,cantidad,precioU,fechaEntrega")] PedidoModel pedido)
        {
            if (id != pedido.Idpedido)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                pedido.fechaPedido = DateTime.Now;
                pedido.total = pedido.precioU * pedido.cantidad;
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(pr => pr.Idpedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {


            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
            ViewData["ListaProveedores"] = new SelectList(_context.Proveedors, "Idproveedor", "ruc");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Idproducto,Idproveedor,cantidad,precioU,total,fechaPedido,fechaEntrega")] PedidoModel pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.fechaPedido = DateTime.Now; // Establecer la fecha de pedido como la fecha y hora actual

                // Calcular el total multiplicando el precio por la cantidad
                pedido.total = pedido.precioU * pedido.cantidad;
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);

        }
    }
}
