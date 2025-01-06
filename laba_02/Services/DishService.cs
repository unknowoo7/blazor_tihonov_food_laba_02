using laba_02.Databse;

namespace laba_02.Services;

public interface IDishService
{
  public List<Dish> GetDishes();
  public Dish GetDishById(int id);
  public Task<List<Ingredient>> GetIngredientByDishId(int id);
  public Task<List<Product>> GetProductsByIngredientId(int id);
  public Task<Product> SaveProductAsync(int dishId, string productName, string unit);
  public Task<bool> UpdateProductAsync(int productId, string productName, string unit,  double volume);
  public Task<bool> DeleteProductAndIngredientsAsync(int productId);
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
  
  public Dish GetDishById(int id)
  {
    try
    {
      return _dbContext.Dishes.FindAsync(id).Result;
    }
    catch (Exception e)
    {
      throw new Exception(e.ToString());
    }
  }

  public async Task<List<Ingredient>> GetIngredientByDishId(int id)
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

  public async Task<List<Product>> GetProductsByIngredientId(int id)
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

  public async Task<Product> SaveProductAsync(int dishId, string productName, string unit)
  {
    try
    {
      var product = new Product
      {
        ProductName = productName,
        UnitOfMeasurement = unit
      };
      
      var newProduct = _dbContext.Products.Add(product);
      
      await _dbContext.SaveChangesAsync();

      return newProduct.Entity;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ошибка при сохранении продукта и ингредиента: {ex.Message}");
      return null;
    }
  }

  public async Task<bool> UpdateProductAsync(int productId, string productName, string unit, double volume)
  {
    try
    {
      var product = await _dbContext.Products.FindAsync(productId);
      var ingredient = await _dbContext.Ingredients.FindAsync(productId);
      
      if (product == null || ingredient == null)
      {
        return false;
      }
      
      product.ProductName = productName;
      product.UnitOfMeasurement = unit;
      ingredient.ProductVolume = volume;
      
      await _dbContext.SaveChangesAsync();

      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ошибка при обновлении продукта: {ex.Message}");
      return false;
    }
  }

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