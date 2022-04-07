using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.Events
{
    public class DetailedAthleteCreatedEvent
    {
        public DetailedAthleteCreatedEvent(Guid id, DetailedAthlete detailedAthlete)
        {
            Id = id;
            DetailedAthlete = detailedAthlete;
        }

        public Guid Id { get; set; }
        public DetailedAthlete DetailedAthlete { get; set; }
    }
}
