using SecondHand.Models.Strava;

namespace SecondHand.Library.Events
{
    public class AthleteUpdatedEvent
    {
        public AthleteUpdatedEvent(long id, Athlete athlete)
        {
            Id = id;
            Athlete = athlete;
        }

        public long Id { get; set; }
        public Athlete Athlete { get; set; }
    }
}
