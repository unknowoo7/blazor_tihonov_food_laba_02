namespace laba_02.Services;

public interface IDishService
{
    public List<Dish> GetDishes();
}

public class DishService : IDishService
{
    public List<Dish> GetDishes()
    {
        throw new NotImplementedException();
    }
}