namespace specification_and_notification
{
    public class Passenger
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PassengerId { get; set; }
        public int AttendantPassengerId { get; set; }
        public PassengerType Type { get; set; }
        public enum PassengerType {
            Child04 = 1,
            Child512 = 2
        }
    }
}