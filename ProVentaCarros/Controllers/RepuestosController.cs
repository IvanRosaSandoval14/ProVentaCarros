using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProVentaCarros.Models;
using System.IO;

namespace ProVentaCarros.Controllers
{
    public class RepuestosController : Controller
    {
        private readonly ProVentacarProyectContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RepuestosController(ProVentacarProyectContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Repuestos
        public async Task<IActionResult> Index()
        {
            var proVentacarProyectContext = _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation);
            return View(await proVentacarProyectContext.ToListAsync());
        }

        // GET: Repuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        public async Task<string> GuardarImage(IFormFile? file, string url = "")
        {
            string urlImage = url;
            if (file != null && file.Length > 0)
            {
                string nameFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", nameFile);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                urlImage = "/imagenes/" + nameFile;
            }
            return urlImage;
        }

        // GET: Repuestos/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1");
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre");
            return View();
        }

        // POST: Repuestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreRepuesto,IdVendedor,IdDepartamento,ImgProducto,Compatiblilidad,DescripcionR,Proveniencia,EstadoRp,Precio,FechaRp,Disponibilidad,Actividad,ComentarioR")] Repuesto repuesto, IFormFile? file = null)
        {
            if (ModelState.IsValid)
            {
                repuesto.ImgProducto = await GuardarImage(file);
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // GET: Repuestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // POST: Repuestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreRepuesto,IdVendedor,IdDepartamento,ImgProducto,Compatiblilidad,DescripcionR,Proveniencia,EstadoRp,Precio,FechaRp,Disponibilidad,Actividad,ComentarioR")] Repuesto repuesto, IFormFile? file = null)
        {
            if (id != repuesto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repuesto.ImgProducto = await GuardarImage(file);
                    _context.Update(repuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoExists(repuesto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // GET: Repuestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // POST: Repuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto != null)
            {
                _context.Repuestos.Remove(repuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuestos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> PublicLista()
        {
            var repuestos = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .ToListAsync();
            return View(repuestos);
        }
    }
}





