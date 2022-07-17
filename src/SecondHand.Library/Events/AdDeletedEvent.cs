using SecondHand.Models.Strava;

namespace SecondHand.Library.Events
{
    public class AdDeletedEvent
    {
        public AdDeletedEvent(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}