namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.Models.Adversitement;

public interface IMarkDataAccess
{
    public List<Mark> GetMark();
    public Mark GetMark(int id);
    public Mark InsertMark(Mark category);
    public Mark UpdateMark(Mark category);
    public bool DeleteMark(int id);
}
