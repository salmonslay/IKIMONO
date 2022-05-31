#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotifications : MonoBehaviour
{

    public static AndroidNotifications Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Create and register notification channel.
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = "NeedsChannel",
            Name = "Needs Channel",
            Importance = Importance.High,
            Description = "Channel for Pet Needs Notifications.",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
    }

    public AndroidNotification BuildNotification(string title, string messageText, string largeIcon = null, float fireTimeDelay = 0, string smallIcon = "icon_app_small")
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(messageText))
        {
            // Return empty notification. 
            // Checked for in PushNotification script.
            return new AndroidNotification();
        }

        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = messageText;
        notification.SmallIcon = smallIcon;
        if (largeIcon != null)
            notification.LargeIcon = largeIcon;
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeDelay);

        return notification;
    }

    public void PushNotification(AndroidNotification notification)
    {
        // Check if notification is valid and return out if it is not.
        if (string.IsNullOrWhiteSpace(notification.Title) || string.IsNullOrWhiteSpace(notification.Text))
        {
            return;
        }

        // Send notification.
        var notificationId = AndroidNotificationCenter.SendNotification(notification, "NeedsChannel");

        // Check if the notification is duplicated.
        // Cancel it and send new one if it is duplicated.
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(notificationId);
            AndroidNotificationCenter.SendNotification(notification, "NeedsChannel");
        }
    }

    public void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

}
#endif