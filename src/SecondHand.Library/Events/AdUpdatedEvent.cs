using SecondHand.Models.Adversitement;

namespace SecondHand.Library.Events
{
    public class AdUpdatedEvent
    {
        public AdUpdatedEvent(long id, Ad ad)
        {
            Id = id;
            Ad = ad;
        }

        public long Id { get; set; }
        public Ad Ad { get; set; }
    }
}
