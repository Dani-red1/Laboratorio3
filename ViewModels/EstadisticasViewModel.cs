using System.Collections.Generic;

public class EstadisticasViewModel
{
    public decimal PromedioPrecios { get; set; }
    public decimal ValorTotalInventario { get; set; }
    public List<Producto> ProductosMasCaros { get; set; }
    public List<Producto> StockCritico { get; set; }
}