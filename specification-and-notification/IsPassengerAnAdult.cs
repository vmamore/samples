using static specification_and_notification.Passenger;

namespace specification_and_notification
{
    public class IsPassengerAnAdult : ISpecification<Passenger>
    {
        public bool IsSatisfiedBy(Passenger passenger)
        {
            return  (passenger.Type != PassengerType.Child04 &&
                     passenger.Type != PassengerType.Child512);
        }
    }
}