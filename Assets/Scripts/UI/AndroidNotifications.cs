using System.Collections;
using System.Collections.Generic;
using IKIMONO.Pet;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotifications : MonoBehaviour
{


    void Start()
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = "TestChannel",
            Name = "Test Channel",
            Importance = Importance.High,
            Description = "Testing Notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
    }

    public void PushNotificationTest()
    {
        Pet pet = Player.Instance.Pet;

        AndroidNotification notification = new AndroidNotification();
        notification.Title = "Hunger level: " + pet.Hunger.Value + " /100";
        notification.Text = "Your pet" + pet.Name + " is hungry!";
        notification.SmallIcon = "smalltesticon";
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddSeconds(5);

        var notificationId = AndroidNotificationCenter.SendNotification(notification, "TestChannel");
        Debug.Log(AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId));

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "TestChannel");
        }

    }

    public void BuildNotification(string title, string messageText, string smallIcon, string largeIcon = null, float fireTimeDelay = 0)
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = messageText;
        notification.SmallIcon = smallIcon;
        if (largeIcon != null)
            notification.LargeIcon = largeIcon;
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeDelay);
    }

    public void PushNotification(AndroidNotification notification)
    {
        var notificationId = AndroidNotificationCenter.SendNotification(notification, "TestChannel");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "TestChannel");
        }
    }

    public void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

}
