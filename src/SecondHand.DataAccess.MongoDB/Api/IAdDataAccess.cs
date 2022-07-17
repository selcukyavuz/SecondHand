namespace SecondHand.DataAccess.MongoDB.Api;

using SecondHand.Models.Advertisement;

public interface IAdDataAccess
{
    public List<Ad> GetAd();
    public Ad GetAd(int id);
    public Ad InsertAd(Ad Ad);
    public Ad UpdateAd(Ad Ad);
    public bool DeleteAd(int id);
}