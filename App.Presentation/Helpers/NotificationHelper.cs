namespace App.Presentation.Helpers
{


    public class Notification
    {
        public string Text { get; set; }
        public string CssClass { get; set; }
        public static Notification Success(string Text)
        {
            Notification notification = new Notification();
            notification.Text = Text;
            notification.CssClass = NotificationCssType.success.ToString();
            return notification;
        }
        public static Notification Erorr(string Text)
        {
            Notification notification = new Notification();
            notification.Text = Text;
            notification.CssClass = NotificationCssType.danger.ToString();
            return notification;
        }
    }

   
    public enum NotificationCssType
    {
        success,
        info,
        danger,
        warning
    }
}
