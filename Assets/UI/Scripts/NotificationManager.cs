using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        RegisterChannel();
        ScheduleNotification();
    }

    void RegisterChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "game_channel",
            Name = "Game Alerts",
            Importance = Importance.High,
            Description = "Gameplay reminders and rewards",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void ScheduleNotification()
    {
        var notification = new AndroidNotification();

        notification.Title = "Your run is waiting";
        notification.Text = "Your last score is still unbeaten. Jump back in.";

        notification.FireTime = DateTime.Now.AddHours(1);

        // notification.SmallIcon = "icon_0";  
        // notification.LargeIcon = "icon_0";

        notification.Color = new Color(0.1f, 0.6f, 1f);

        notification.ShouldAutoCancel = true;

        AndroidNotificationCenter.SendNotification(notification, "game_channel");
    }
}