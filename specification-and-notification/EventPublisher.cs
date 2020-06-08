using System;

namespace specification_and_notification
{
    public static class EventPublisher
    {
        public static event EventHandler<NotificationEventArgs> RaisedNotificationEvent;

        public static void OnRaiseNotificationEvent(NotificationEventArgs e){
            var handler = RaisedNotificationEvent;
            if(handler == null) return;
            handler(new object(), e);
        }
    }

    public class NotificationEventArgs : EventArgs {

        public NotificationEventArgs(string s){
            Message = s;
        }
        public string Message { get; set; }
    }
}