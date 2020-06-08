namespace specification_and_notification
{
    public class IsPassengerPhoneNumberProvided : ISpecification<Passenger>
    {
        public bool IsSatisfiedBy(Passenger passenger)
        {
            return !string.IsNullOrWhiteSpace(passenger.PhoneNumber);
        }
    }
}