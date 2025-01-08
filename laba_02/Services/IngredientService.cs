using laba_02.Databse;
using laba_02.Models;

namespace laba_02.Services;

public interface IIngredientService
{
  public Task<bool> SaveIngredientAsync(int dishId, int productId, double volume);
}

public class IngredientService : IIngredientService
{
  readonly DatabaseContext _dbContext;
  
  public IngredientService(DatabaseContext dbContext)
  {
    _dbContext = dbContext;
  }
  
  public async Task<bool> SaveIngredientAsync(int dishId, int productId, double volume)
  {
    try
    {
      var ingredient = new Ingredient
      {
        DishId = dishId,
        ProductId = productId,
        ProductVolume = volume
      };
      _dbContext.Ingredients.Add(ingredient);
      
      await _dbContext.SaveChangesAsync();

      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ошибка при сохранении продукта и ингредиента: {ex.Message}");
      return false;
    }
  }
}