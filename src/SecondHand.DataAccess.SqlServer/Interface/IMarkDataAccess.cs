namespace SecondHand.DataAccess.SqlServer.Interface;

using SecondHand.Models.Advertisement;

public interface IMarkDataAccess
{
    public List<Mark> GetMark();
    public Mark GetMark(int id);
    public Mark InsertMark(Mark category);
    public Mark UpdateMark(Mark category);
    public bool DeleteMark(int id);
}
