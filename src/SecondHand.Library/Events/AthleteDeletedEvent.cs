using SecondHand.Models.Strava;

namespace SecondHand.Library.Events
{
    public class AthleteDeletedEvent
    {
        public AthleteDeletedEvent(long id)
        {
            Id = id ;
        }

        public long Id { get; set; }
    }
}