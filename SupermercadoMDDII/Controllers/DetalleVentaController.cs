using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;

namespace SupermercadoMDDII.Controllers
{
    public class DetalleVentaController : Controller
    {
        private readonly SupermercadoContext _context;
        private readonly ProductoController _productoController;

        private List<DetalleVentaModel> listaDetallesVenta = new List<DetalleVentaModel>(); // Lista para almacenar los detalles de venta

        public DetalleVentaController(SupermercadoContext context, ProductoController productoController)
        {
            _context = context;
            _productoController = productoController;
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("Idventa,Idproducto,cantidad,precio,total")] DetalleVentaModel detalle)
        //{
        //    //ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
        //    if (ModelState.IsValid)
        //    {
        //        //ViewData["ListaProductos"] = new SelectList(_context.Productos, "Idproducto", "descripcion");
        //        // Obtener el último Idventa de la tabla Ventas
        //        var idUltimaVenta = await _context.Ventas
        //            .OrderByDescending(v => v.Idventa)
        //            .Select(v => v.Idventa)
        //            .FirstOrDefaultAsync();

        //        // Si se encontró alguna venta
        //        if (idUltimaVenta != null)
        //        {
        //            // Asignar el último Idventa encontrado a la variable 'id'
        //            detalle.Idventa = idUltimaVenta;
        //            Console.WriteLine(detalle.Idventa);

        //            // Obtener el precio del producto seleccionado
        //            var producto = await _context.Productos.FindAsync(detalle.Idproducto);
        //            if (producto != null)
        //            {
        //                detalle.precio = producto.precio;
        //                detalle.total = detalle.cantidad * producto.precio;
        //                producto.stock-=detalle.cantidad;

        //                var producto2 = await _context.Productos.Include(p => p.Categorias).FirstOrDefaultAsync(p => p.Idproducto == detalle.Idproducto);

        //                decimal impuesto = 0;
        //                foreach (var categoria in producto2.Categorias.Idcategoria)
        //                {
        //                    impuesto += categoria.impuesto * detalle.total; // Sumar el impuesto de cada categoría por el total
        //                }
        //                // Llama al método VerificarStockYEnviarCorreo del ProductoController después de actualizar el stock
        //                await _productoController.VerificarStockYEnviarCorreo(detalle.Idproducto);
        //                //detalle.Ventas.subTotal = detalle.total;

        //            }
        //            else
        //            {
        //                detalle.precio = 0;
        //                detalle.total = 0;
        //                //detalle.Ventas.subTotal = detalle.total;
        //            }


        //        }

        //        listaDetallesVenta.Add(detalle); // Agregar detalle a la lista
        //                                         // Calcular la suma de los totales de todos los detalles de venta en la lista
        //        decimal sumaTotales = listaDetallesVenta.Sum(detalle => detalle.total);
        //        // Obtener el producto asociado al pedido
        //        var venta = await _context.Ventas.FindAsync(idUltimaVenta);


        //        venta.subTotal+= sumaTotales;
        //        //venta.impuestoTotal = venta.subTotal * 0.12m;
        //        venta.Total= venta.subTotal+venta.impuestoTotal;



        //        // Actualizar el impuesto en la venta
        //        venta.impuestoTotal += impuesto;
        //        venta.Total = venta.subTotal + venta.impuestoTotal;
        //        // Obtener la venta asociada
        //        var ventaC = await _context.Ventas.Include(v => v.Clientes).FirstOrDefaultAsync(v => v.Idventa == idUltimaVenta);

        //        if (ventaC != null && ventaC.Clientes != null)
        //        {
        //            // Verificar si el valor total de la venta supera los 10 dólares
        //            if (venta.Total > 10)
        //            {
        //                // Aumentar los puntos de recompensa del cliente en 5
        //                ventaC.Clientes.puntosRecompensa += 5;
        //            }

        //            // Guardar los cambios en el contexto
        //            await _context.SaveChangesAsync();
        //        }

        //        _context.Add(detalle);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Create));
        //        ModelState.Clear();
        //    }
        //    return View(detalle);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Idventa,Idproducto,cantidad,precio,total")] DetalleVentaModel detalle)
        {
            if (ModelState.IsValid)
            {

                var idUltimaVenta = await _context.Ventas
                    .OrderByDescending(v => v.Idventa)
                    .Select(v => v.Idventa)
                    .FirstOrDefaultAsync();

                if (idUltimaVenta != null)
                {
                    detalle.Idventa = idUltimaVenta;

                    var producto = await _context.Productos.FindAsync(detalle.Idproducto);
                    if (producto != null)
                    {
                        detalle.precio = producto.precio;
                        decimal subtotal = detalle.cantidad * producto.precio;
                        //detalle.total = detalle.cantidad * producto.precio;
                        producto.stock -= detalle.cantidad;

                        var productoConCategorias = await _context.Productos
                            .Include(p => p.Categorias)
                            .FirstOrDefaultAsync(p => p.Idproducto == detalle.Idproducto);

                        decimal impuesto = 0;
                        if (productoConCategorias != null && productoConCategorias.Categorias != null)
                        {
                            var categoria = productoConCategorias.Categorias;
                            //{
                            impuesto += (categoria.impuesto ?? 0) * subtotal;
                            detalle.total = impuesto+subtotal;
                            // Sumar el impuesto de cada categoría por el total
                            //}
                        }

                        await _productoController.VerificarStockYEnviarCorreo(detalle.Idproducto);
                    }
                    else
                    {
                        detalle.precio = 0;
                        detalle.total = 0;
                    }
                }

                listaDetallesVenta.Add(detalle);

                decimal sumaTotales = listaDetallesVenta.Sum(det => det.total);
                var venta = await _context.Ventas.FindAsync(idUltimaVenta);
                

                venta.subTotal += sumaTotales;
                venta.impuestoTotal =0;
                venta.Total = venta.subTotal + venta.impuestoTotal;

                var ventaConCliente = await _context.Ventas.Include(v => v.Clientes).FirstOrDefaultAsync(v => v.Idventa == idUltimaVenta);

                if (ventaConCliente != null && ventaConCliente.Clientes != null)
                {
                    if (venta.Total > 10)
                    {
                        ventaConCliente.Clientes.puntosRecompensa += 5;
                    }

                    await _context.SaveChangesAsync();
                }

                _context.Add(detalle);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Create));
            }
            return View(detalle);
        }


        public async Task<IActionResult> ReporteIngresos()
        {
            var reporteIngresos = await _context.DetalleVentas
                .Include(ep => ep.Ventas)
                    .ThenInclude(p => p.Clientes)
                .Include(ep => ep.Productos)
                .GroupBy(ep => ep.Idventa) // Agrupar por Idventa
                .Select(group => new ReporteIngresoViewModel
                {
                    IdVenta = group.Key, // Key es el Idventa del grupo
                    NombreCliente = group.FirstOrDefault().Ventas.Clientes.nombre,
                    ApellidoCliente = group.FirstOrDefault().Ventas.Clientes.apellido,
                    ValorTotalPagado = group.FirstOrDefault().Ventas.Total, // Sumar los totales de cada grupo
                    FechaVenta = group.FirstOrDefault().Ventas.fechaRegistro
                })
                .ToListAsync();

            return View(reporteIngresos);
        }

        public async Task<IActionResult> Facturas()
        {
            // Obtener el ID de la última venta
            var idUltimaVenta = await _context.Ventas
                .OrderByDescending(v => v.Idventa)
                .Select(v => v.Idventa)
                .FirstOrDefaultAsync();

            if (idUltimaVenta != null)
            {
                // Obtener los detalles de venta asociados a la última venta
                var facturas = await _context.DetalleVentas
                    .Where(d => d.Idventa == idUltimaVenta)
                    .Select(d => new FacturaViewModel
                    {
                        IdVenta = idUltimaVenta,
                        NombreCliente = d.Ventas.Clientes.nombre,
                        ApellidoCliente = d.Ventas.Clientes.apellido,
                        NombreProducto = d.Productos.descripcion,
                        Cantidad = d.cantidad,
                        PrecioUnitario = d.Productos.precio,
                        Total = d.cantidad*d.Productos.precio,
                        FechaVenta = d.Ventas.fechaRegistro
                    })
                    .ToListAsync();

                return View(facturas);
            }

            return View(new List<FacturaViewModel>());
        }




    }
}
