using System.ComponentModel.DataAnnotations;

namespace laba_02.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; } // Код блюда

    [Required]
    public string DishType { get; set; } // Тип блюда

    [Required]
    public double DishWeight { get; set; } // Вес блюда

    public string CookingOrder { get; set; } // Порядок приготовления

    public int Calories { get; set; } // Количество калорий

    public int Carbohydrates { get; set; } // Количество углеводов

    // Связь с Ingredient (Таблица №2)
    public ICollection<Ingredient> Ingredients { get; set; }
}