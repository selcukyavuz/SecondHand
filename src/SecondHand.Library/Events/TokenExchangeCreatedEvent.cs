using SecondHand.Library.Models;

namespace SecondHand.Library.Events
{
    public class TokenExchangeCreatedEvent
    {
        public TokenExchangeCreatedEvent(Guid id, TokenExchange tokenExchange)
        {
            Id = id;
            TokenExchange = tokenExchange;
        }

        public Guid Id { get; set; }
        public TokenExchange TokenExchange { get; set; }
    }
}
