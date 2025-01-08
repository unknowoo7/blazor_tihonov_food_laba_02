using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laba_02.Models;

public class Ingredient
{
    [Key]
    public int IngredientId { get; set; }

    [ForeignKey("Dish")]
    public int DishId { get; set; } // Код блюда
    public Dish Dish { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; } // Код продукта
    public Product Product { get; set; }

    [Required]
    public double ProductVolume { get; set; } // Объем продукта
}