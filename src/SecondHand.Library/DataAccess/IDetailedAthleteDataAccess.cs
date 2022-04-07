using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.DataAccess
{
    public interface IDetailedAthleteDataAccess
    {
        public List<DetailedAthlete> GetDetailedAthlete();
        public DetailedAthlete GetDetailedAthlete(long? id);
        public DetailedAthlete InsertDetailedAthlete(DetailedAthlete detailedAthlete);
        public DetailedAthlete UpdateDetailedAthlete(DetailedAthlete detailedAthlete);
        public bool DeleteDetailedAthlete(long? id);
   }
}