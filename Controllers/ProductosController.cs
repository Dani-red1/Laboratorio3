using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ProductosController : Controller
{
    private readonly InventarioDbContext _context;

    public ProductosController(InventarioDbContext context)
    {
        _context = context;
    }

    // Búsqueda
    public async Task<IActionResult> Index(string searchString)
    {
        IQueryable<Producto> productosQuery = _context.Productos;

        if (!string.IsNullOrEmpty(searchString))
        {
            productosQuery = productosQuery
                .Where(p => p.Nombre.Contains(searchString));
        }

        var productos = await productosQuery
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        ViewData["CurrentFilter"] = searchString;
        return View(productos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Precio,Stock")] Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Producto producto)
    {
        if (id != producto.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}