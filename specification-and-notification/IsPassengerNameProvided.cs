namespace specification_and_notification
{
    public class IsPassengerNameProvided : ISpecification<Passenger>
    {
        public bool IsSatisfiedBy(Passenger passenger)
        {
            return !string.IsNullOrWhiteSpace(passenger.Name);
        }
    }
}