namespace SecondHand.DataAccess.SqlServer.Interface;

using SecondHand.Models.Strava;

public interface IAthleteDataAccess
{
    public List<Athlete> GetAthlete();
    public Athlete GetAthlete(int id);
    public Athlete InsertAthlete(Athlete athlete);
    public Athlete UpdateAthlete(Athlete athlete);
    public bool DeleteAthlete(int id);
}
