using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;

namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class VentaController : Controller
    {
        private readonly SupermercadoContext _context;

        public VentaController(SupermercadoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DetalleVenta()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ventas.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            //Select Inner Join
            var venta = await _context.Ventas.Where(v => v.Idventa == id)
                .Include(pr => pr.Clientes)
                .Include(pr => pr.Usuarios).FirstOrDefaultAsync();

            if (venta == null)
            {
                return NotFound();

            }

            ViewData["ListaUsuarios"] = new SelectList(_context.Usuarios, "Idusuario", "apellido");
            ViewData["ListaClientes"] = new SelectList(_context.Clientes, "Idcliente", "cedula");

            return View(venta);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idventa,Idusuario,Idcliente,subTotal,impuestoTotal,Total,fechaRegistro")] VentaModel Venta)
        {
            if (id != Venta.Idventa)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                //Venta.fechaRegistro = DateTime.Now;
                Venta.Total = Venta.subTotal + ((Venta.impuestoTotal / 100) * Venta.subTotal);
                _context.Update(Venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Venta);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Idventa == id);
            if (Venta == null)
            {
                return NotFound();
            }
            return View(Venta);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {


            var Venta = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(Venta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            ViewData["ListaUsuarios"] = new SelectList(_context.Usuarios, "Idusuario", "apellido");
            ViewData["ListaClientes"] = new SelectList(_context.Clientes, "Idcliente", "cedula");
            ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproductos", "descripcion");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Idusuario,Idcliente,subTotal,impuestoTotal,Total,fechaRegistro")] VentaModel venta,  string action)
        {
            if (ModelState.IsValid)
            {
                venta.fechaRegistro = DateTime.Now; // Establecer la fecha de pedido como la fecha y hora actual
                //Console.WriteLine($"Venta a registrar: Idusuario={venta.Idusuario}, Idcliente={venta.Idcliente}, SubTotal={venta.subTotal}, ImpuestoTotal={venta.impuestoTotal}, Total={venta.Total}, FechaRegistro={venta.fechaRegistro}");
                venta.subTotal = 0;
                venta.impuestoTotal = 0;
                venta.Total = 0;

                // Calcular el total multiplicando el precio por la cantidad
                //venta.Total = venta.subTotal + ((venta.impuestoTotal / 100) * venta.subTotal);
                _context.Add(venta);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(DetalleVenta));
                if (action == "Siguiente")
                {
                    // Redirige a la acción Create del controlador DetalleVenta
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "DetalleVenta");
                }

                // Redirige a la acción Index u otra acción según tus necesidades
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
            

        }


    public IActionResult CreateDT()
    {
        ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDT([Bind("Idventa,Idproducto,cantidad,precio,total")] DetalleVentaModel detalle)
    {
        if (ModelState.IsValid)
        {
            // Obtener el último Idventa de la tabla Ventas
            var idUltimaVenta = await _context.Ventas
                .OrderByDescending(v => v.Idventa)
                .Select(v => v.Idventa)
                .FirstOrDefaultAsync();

            // Si se encontró alguna venta
            if (idUltimaVenta != null)
            {
                // Asignar el último Idventa encontrado a la variable 'id'
                detalle.Idventa = idUltimaVenta;
                Console.WriteLine(detalle.Idventa);

                // Obtener el precio del producto seleccionado
                var producto = await _context.Productos.FindAsync(detalle.Idproducto);
                if (producto != null)
                {
                    detalle.precio = producto.precio;
                    detalle.total = detalle.cantidad * producto.precio;
                    detalle.Ventas.subTotal = detalle.total;

                }
                else
                {
                    detalle.precio = 0;
                    detalle.total = 0;
                    detalle.Ventas.subTotal = detalle.total;
                }


            }


            _context.Add(detalle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CreateDT));
            //ModelState.Clear();
        }
        return View(detalle);
    }
}
}
