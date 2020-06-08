using System.Collections.Generic;
using System.Linq;

namespace specification_and_notification
{
    public class IsPassengerAttended : ISpecification<Passenger>
    {
        private readonly IEnumerable<Passenger> _passengers;
        public IsPassengerAttended(IEnumerable<Passenger> passengers)
        {
            _passengers = passengers;
        }
        public bool IsSatisfiedBy(Passenger passenger)
        {
            return _passengers.Any(att => att.PassengerId == passenger.AttendantPassengerId);
        }
    }
}