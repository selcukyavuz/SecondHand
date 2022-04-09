using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.DataAccess
{
    public interface IAthleteDataAccess
    {
        public List<Athlete> GetAthlete();
        public Athlete GetAthlete(int id);
        public Athlete InsertAthlete(Athlete athlete);
        public Athlete UpdateAthlete(Athlete athlete);
        public bool DeleteAthlete(int id);
   }
}