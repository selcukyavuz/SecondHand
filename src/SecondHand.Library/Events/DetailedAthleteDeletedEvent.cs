using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.Events
{
    public class DetailedAthleteDeletedEvent
    {
        public DetailedAthleteDeletedEvent(long id)
        {
            Id = id ;
        }

        public long Id { get; set; }
    }
}