namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.Models.Adversitement;

public interface ICategoryDataAccess
{
    public List<Category> GetCategory();
    public Category GetCategory(int id);
    public Category InsertCategory(Category category);
    public Category UpdateCategory(Category category);
    public bool DeleteCategory(int id);
}
