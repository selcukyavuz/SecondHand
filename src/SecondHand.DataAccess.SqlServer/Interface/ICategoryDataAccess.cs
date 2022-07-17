namespace SecondHand.DataAccess.SqlServer.Interface;

using SecondHand.Models.Advertisement;

public interface ICategoryDataAccess
{
    public List<Category> GetCategory();
    public Category GetCategory(int id);
    public Category InsertCategory(Category category);
    public Category UpdateCategory(Category category);
    public bool DeleteCategory(int id);
}
