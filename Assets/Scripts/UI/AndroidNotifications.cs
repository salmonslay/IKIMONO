using System.Collections;
using System.Collections.Generic;
using IKIMONO.Pet;
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

        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = "TestChannel",
            Name = "Test Channel",
            Importance = Importance.High,
            Description = "Testing Notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
    }

    public AndroidNotification BuildNotification(string title, string messageText, string largeIcon = null, float fireTimeDelay = 0, string smallIcon = "icon_app_small")
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(messageText))
        {
            // TODO: Johan?
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
        if (string.IsNullOrEmpty(notification.Title) || string.IsNullOrEmpty(notification.Text))
        {
            return;
        }

        var notificationId = AndroidNotificationCenter.SendNotification(notification, "TestChannel");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(notificationId);
            //AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "TestChannel");
        }
    }

    public void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

}
