namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.Models.Advertisement;

public interface IProductDataAccess
{
    public List<Product> GetProduct();
    public Product GetProduct(int id);
    public Product InsertProduct(Product category);
    public Product UpdateProduct(Product category);
    public bool DeleteProduct(int id);
}
