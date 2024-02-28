using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SupermercadoMDDII.Models;
using static SupermercadoMDDII.Controllers.ProveedorController;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using System.Net;
using System;
using System.Diagnostics;
using SupermercadoMDDII.Services;
using SupermercadoMDDII.Implementacion;


namespace SupermercadoMDDII.Controllers
{
    [ValidarSesion]
    public class ProveedorController : Controller
    {
        private readonly SupermercadoContext _context;

        private readonly IEmailService _emailService;

        public ProveedorController(SupermercadoContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedors.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Error 404 a la vista del Usuario
                return NotFound();

            }
            var proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();

            }
            return View(proveedor);
        }

        [HttpPost] //Esto diferencia el metodo Edit que graba, del edit de vista
        public async Task<IActionResult> Edit(int id, [Bind("Idproveedor,ruc,nombre,apellido,direccion,telefono,correo")] ProveedorModel proveedor)
        {
            if (id != proveedor.Idproveedor)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _context.Update(proveedor);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedors.FirstOrDefaultAsync(pro => pro.Idproveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ruc,nombre,apellido,direccion,telefono,correo")] ProveedorModel proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }


        //// Método para verificar el stock y enviar correo si es necesario
        //private async Task CheckStockAndSendEmail(int proveedorId)
        //{
        //    var productos = await _context.Productos.Where(p => p.Idproveedor == proveedorId && p.stock < 10).ToListAsync();

        //    if (productos.Any())
        //    {
        //        var proveedor = await _context.Proveedors.FindAsync(proveedorId);

        //        foreach (var producto in productos)
        //        {
        //            var emailDTO = new EmailDTO
        //            {
        //                Para = proveedor.correo,
        //                Asunto = "Stock bajo en productos",
        //                Contenido = $"El producto {producto.descripcion} tiene un stock bajo. Por favor, reabastece el inventario."
        //            };

        //            // Envía el correo utilizando el servicio de correo electrónico
        //            _emailService.SendEmail(emailDTO);
        //        }
        //    }
        //}
    }
}

