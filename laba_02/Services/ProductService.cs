using laba_02.Databse;

namespace laba_02.Services;

public interface IProductService
{
  List<Product> GetProducts();
}

public class ProductService : IProductService
{
  readonly DatabaseContext _dbContext;

  public ProductService(DatabaseContext dbContext)
  {
    _dbContext = dbContext;
  }
  
  
  public List<Product> GetProducts()
  {
    try
    {
      return _dbContext.Products.ToList();
    }
    catch (Exception e)
    {
      throw new Exception(e.ToString());
    }
  }
}



