using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProVentaCarros.Models;

namespace ProVentaCarros.Controllers
{
    public class CarritoComprasController : Controller
    {
        private readonly ProVentacarProyectContext _context;

        public CarritoComprasController(ProVentacarProyectContext context)
        {
            _context = context;
        }

        // GET: CarritoCompras
        public async Task<IActionResult> Index()
        {
            var proVentacarProyectContext = _context.CarritoCompras.Include(c => c.IdClienteNavigation);
            return View(await proVentacarProyectContext.ToListAsync());
        }

        // GET: CarritoCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompras
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // GET: CarritoCompras/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: CarritoCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,FechaCreacion,Estado")] CarritoCompra carritoCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carritoCompra.IdCliente);
            return View(carritoCompra);
        }

        // GET: CarritoCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompras.FindAsync(id);
            if (carritoCompra == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carritoCompra.IdCliente);
            return View(carritoCompra);
        }

        // POST: CarritoCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,FechaCreacion,Estado")] CarritoCompra carritoCompra)
        {
            if (id != carritoCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoCompraExists(carritoCompra.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carritoCompra.IdCliente);
            return View(carritoCompra);
        }

        // GET: CarritoCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompras
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // POST: CarritoCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoCompra = await _context.CarritoCompras.FindAsync(id);
            if (carritoCompra != null)
            {
                _context.CarritoCompras.Remove(carritoCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoCompraExists(int id)
        {
            return _context.CarritoCompras.Any(e => e.Id == id);
        }
    }
}
