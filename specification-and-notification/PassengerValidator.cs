using System.Collections.Generic;

namespace specification_and_notification
{
    public class PassengerValidator : IValidate
    {
        private readonly Passenger _passenger;
        private readonly IEnumerable<Passenger> _passengers;

        public PassengerValidator(Passenger passenger, IEnumerable<Passenger> passengers)
        {
            _passenger = passenger;
            _passengers = passengers;
        }

        public bool ValidatePassengerIsAttendedAndIsAndAdult(){
                var rule = new IsPassengerAnAdult()
                .Or(new IsPassengerAttended(_passengers))
                .And(new IsPassengerNameProvided())
                .And(new IsPassengerPhoneNumberProvided());

                var isOk = rule.IsSatisfiedBy(_passenger);

                if(!isOk) EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(
                    "Teste"
                ));

                return isOk;
        }
    }
}