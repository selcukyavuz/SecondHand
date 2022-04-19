namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.Models.Adversitement;

public interface IAdDataAccess
{
    public List<Ad> GetAd();
    public Ad GetAd(int id);
    public Ad InsertAd(Ad Ad);
    public Ad UpdateAd(Ad Ad);
    public bool DeleteAd(int id);
}
