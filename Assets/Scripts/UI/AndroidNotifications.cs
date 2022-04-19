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

        string title = "Hunger level: " + pet.Hunger.Value + " /100";
        string messageText = "Your pet" + pet.Name + " is hungry!";
        string smallIcon = "smalltesticon";
        string largeIcon = "largetesticon";
        float fireDelay = 5f;


        AndroidNotification testNotification = BuildNotification(title, messageText, smallIcon, largeIcon, fireDelay);
        PushNotification(testNotification);
    }

    public AndroidNotification BuildNotification(string title, string messageText, string smallIcon, string largeIcon = null, float fireTimeDelay = 0)
    {
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
