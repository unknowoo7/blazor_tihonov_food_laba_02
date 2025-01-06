using System.Data;
using laba_02.Databse;
using Microsoft.EntityFrameworkCore;

namespace laba_02.Services;

public interface IDishService
{
  public List<Dish> GetDishes();
  List<Ingredient> GetIngredientByDishId(int id);
  List<Product> GetProductsByIngredientId(int id);
  Task<bool> DeleteProductAndIngredientsAsync(int productId);
}

public class DishService : IDishService
{
  readonly DatabaseContext _dbContext;

  public DishService(DatabaseContext dbContext)
  {
    _dbContext = dbContext;
  }

  public List<Dish> GetDishes()
  {
    try
    {
      return _dbContext.Dishes.ToList();
    }
    catch (Exception e)
    {
      throw new Exception(e.ToString());
    }
  }

  public List<Ingredient> GetIngredientByDishId(int id)
  {
    try
    {
      List<Ingredient> ingredient = _dbContext.Ingredients.Where(i => i.DishId == id).ToList();
      return ingredient;
    }
    catch (Exception e)
    {
      throw new Exception(e.ToString());
    }
  }

  public List<Product> GetProductsByIngredientId(int id)
  {
    try
    {
      List<Product> products = _dbContext.Ingredients
        .Where(i => i.DishId == id)
        .Join(
          _dbContext.Products,
          ingredient => ingredient.ProductId,
          product => product.ProductId,
          (ingredient, product) => product
        )
        .ToList();
      return products;
    }
    catch (Exception e)
    {
      throw new Exception(e.ToString());
    }
  }

  /// <summary>
  /// Удаляет продукт и все связанные с ним ингредиенты.
  /// </summary>
  /// <param name="productId">ID продукта.</param>
  /// <returns>True, если удаление успешно; иначе false.</returns>
  public async Task<bool> DeleteProductAndIngredientsAsync(int productId)
  {
    try
    {
      var product = await _dbContext.Products.FindAsync(productId);
      if (product == null)
      {
        return false;
      }

      var ingredients = _dbContext.Ingredients
        .Where(i => i.ProductId == productId)
        .ToList();

      _dbContext.Ingredients.RemoveRange(ingredients);

      _dbContext.Products.Remove(product);

      await _dbContext.SaveChangesAsync();

      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ошибка при удалении продукта и ингредиентов: {ex.Message}");
      return false;
    }
  }
}