using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.Events
{
    public class DetailedAthleteCreatedEvent
    {
        public DetailedAthleteCreatedEvent(long id, DetailedAthlete detailedAthlete)
        {
            Id = id;
            DetailedAthlete = detailedAthlete;
        }

        public long Id { get; set; }
        public DetailedAthlete DetailedAthlete { get; set; }
    }
}
