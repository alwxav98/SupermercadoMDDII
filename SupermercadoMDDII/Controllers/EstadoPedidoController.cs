using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;

namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class EstadoPedidoController : Controller
    {
        private readonly SupermercadoContext _context;

        public EstadoPedidoController(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoPedidos.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            //Select Inner Join
            var estadoP = await _context.EstadoPedidos.Where(p => p.IdestadoPedido == id)
                .Include(pr => pr.Pedidos).FirstOrDefaultAsync();



            if (estadoP == null)
            {
                return NotFound();

            }
            ViewData["ListaPedidos"] = new SelectList(_context.Pedidos, "Idpedido", "Idpedido");
            return View(estadoP);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("IdestadoPedido,Idpedido,estado")] EstadoPedidoModel estadoP)
        {
            if (id != estadoP.IdestadoPedido)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                if (estadoP.estado.Equals("Entregado a tiempo"))
                {
                    estadoP.calificiacion = "Excelente";
                }
                else if (estadoP.estado.Equals("Entregado con retraso"))
                {
                    estadoP.calificiacion = "Regular";
                }
                else if (estadoP.estado.Equals("NO Entregado"))
                {
                    estadoP.calificiacion = "Malo";
                }
                _context.Update(estadoP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoP);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var estadoP = await _context.EstadoPedidos.FirstOrDefaultAsync(pr => pr.IdestadoPedido == id);
            if (estadoP == null)
            {
                return NotFound();
            }
            return View(estadoP);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {


            var estadoP = await _context.EstadoPedidos.FindAsync(id);
            _context.EstadoPedidos.Remove(estadoP);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            ViewData["ListaPedidos"] = new SelectList(_context.Pedidos, "Idpedido", "Idpedido");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Idpedido,estado")] EstadoPedidoModel estadoP)
        {
            if (ModelState.IsValid)
            {
                if (estadoP.estado.Equals("Entregado a tiempo"))
                {
                    estadoP.calificiacion = "Excelente";
                }
                else if (estadoP.estado.Equals("Entregado con retraso"))
                {
                    estadoP.calificiacion = "Regular";
                }
                else if (estadoP.estado.Equals("NO Entregado"))
                {
                    estadoP.calificiacion = "Malo";
                }
                _context.Add(estadoP);
                await _context.SaveChangesAsync();
                // Obtener el pedido correspondiente al estado
                var pedido = await _context.Pedidos.FindAsync(estadoP.Idpedido);

                if (pedido != null)
                {
                    // Obtener el producto asociado al pedido
                    var producto = await _context.Productos.FindAsync(pedido.Idproducto);

                    if (producto != null)
                    {
                        // Actualizar el stock del producto según el estado del pedido
                        if (estadoP.estado.Equals("Entregado a tiempo"))
                        {
                            producto.stock += pedido.cantidad;
                        }
                        else if (estadoP.estado.Equals("Entregado con retraso"))
                        {
                            producto.stock += pedido.cantidad;
                        }
                        // No hacer nada si el pedido no se entrega

                        // Guardar los cambios en el stock del producto
                        _context.Update(producto);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(estadoP);

        }

        // Acción para generar el reporte de egresos
        public async Task<IActionResult> ReporteEgresos()
        {
            var reporteEgresos = await _context.EstadoPedidos
                .Where(ep => ep.estado == "Entregado a tiempo" || ep.estado == "Entregado con retraso")
                .Include(ep => ep.Pedidos)
                    .ThenInclude(p => p.Proveedors)
                .Include(ep => ep.Pedidos)
                    .ThenInclude(p => p.Productos)
                .Select(ep => new ReporteEgresoViewModel
                {
                    IdPedido = ep.Pedidos.Idpedido,
                    NombreProveedor = ep.Pedidos.Proveedors.nombre,
                    ApellidoProveedor = ep.Pedidos.Proveedors.apellido,
                    NombreProducto = ep.Pedidos.Productos.descripcion,
                    ValorTotalPagado = ep.Pedidos.total,
                    FechaPedido = ep.Pedidos.fechaPedido
                })
                .ToListAsync();

            return View(reporteEgresos);
        }

    }
}
