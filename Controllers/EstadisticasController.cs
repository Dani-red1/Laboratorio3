using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EstadisticasController : Controller
{
    private readonly InventarioDbContext _context;

    public EstadisticasController(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new EstadisticasViewModel();

        var hayProductos = await _context.Productos.AnyAsync();

        if (hayProductos)
        {
            viewModel.ProductosMasCaros = await _context.Productos
                .OrderByDescending(p => p.Precio)
                .Take(10)
                .ToListAsync();

            viewModel.PromedioPrecios = await _context.Productos
                .AverageAsync(p => p.Precio);

            viewModel.ValorTotalInventario = await _context.Productos
                .SumAsync(p => p.Precio * p.Stock);
        }
        else
        {
            viewModel.ProductosMasCaros = new List<Producto>();
            viewModel.PromedioPrecios = 0;
            viewModel.ValorTotalInventario = 0;
        }

        viewModel.StockCritico = await _context.Productos
            .Where(p => p.Stock < 5)
            .OrderBy(p => p.Stock)
            .ToListAsync();

        return View(viewModel);
    }
}
