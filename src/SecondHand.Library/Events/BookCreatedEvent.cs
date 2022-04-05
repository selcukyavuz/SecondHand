using SecondHand.Library.Models;

namespace SecondHand.Library.Events
{
    public class PersonCreatedEvent
    {
        public PersonCreatedEvent(Guid id, PersonModel personModel)
        {
            Id = id;
            PersonModel = personModel;
        }

        public Guid Id { get; set; }
        public PersonModel PersonModel { get; set; }
    }
}
