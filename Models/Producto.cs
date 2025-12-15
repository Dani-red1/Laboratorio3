using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100)]
    [Display(Name = "Nombre del Producto")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Precio de Venta")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Display(Name = "Stock Actual")]
    public int Stock { get; set; }
}