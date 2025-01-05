using System.ComponentModel.DataAnnotations;

namespace laba_02;

public class Product
{
    [Key]
    public int ProductId { get; set; } // Код продукта

    [Required]
    public string ProductName { get; set; } // Название продукта

    [Required]
    public string UnitOfMeasurement { get; set; } // Ед. измерения

    // Связь с Ingredient (Таблица №2)
    public ICollection<Ingredient> Ingredients { get; set; }
}