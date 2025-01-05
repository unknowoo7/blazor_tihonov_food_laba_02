using laba_02.Databse;

namespace laba_02.Services;

public interface IDishService
{
    public List<Dish> GetDishes();
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
            throw new NotImplementedException();
        }
    }
}