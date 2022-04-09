using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.DataAccess
{
    public interface IDetailedAthleteDataAccess
    {
        public List<DetailedAthlete> GetDetailedAthlete();
        public DetailedAthlete GetDetailedAthlete(int id);
        public DetailedAthlete InsertDetailedAthlete(DetailedAthlete detailedAthlete);
        public DetailedAthlete UpdateDetailedAthlete(DetailedAthlete detailedAthlete);
        public bool DeleteDetailedAthlete(int id);
   }
}