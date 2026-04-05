using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        RegisterChannel();
    }

    void RegisterChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "game_channel",
            Name = "Game Alerts",
            Importance = Importance.High,
            Description = "Smart gameplay reminders",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ScheduleSmartNotification();
        }
    }

    void OnApplicationQuit()
    {
        ScheduleSmartNotification();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }
    }

    void ScheduleSmartNotification()
    {
        AndroidNotificationCenter.CancelAllNotifications();

        // 🔹 Load saved data (GameOver se aaya hua)
        int lastScore = PlayerPrefs.GetInt("LAST_SCORE", 0);
        string lastTimeStr = PlayerPrefs.GetString("LAST_PLAY_TIME", "");

        DateTime lastPlayTime = DateTime.Now;

        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            DateTime.TryParse(lastTimeStr, out lastPlayTime);
        }

        TimeSpan timeAway = DateTime.Now - lastPlayTime;

        string title = "";
        string message = "";

        // SMART MESSAGE LOGIC
        if (timeAway.TotalMinutes < 10)
        {
            title = "You left mid-run";
            message = $"You scored {lastScore}. Think you can do better?";
        }
        else if (timeAway.TotalHours < 6)
        {
            title = "Ready for another run?";
            message = $"Your last score was {lastScore}. Beat it now.";
        }
        else
        {
            title = "We miss you";
            message = $"Your best run is waiting. Come back and beat {lastScore}.";
        }

        var notification = new AndroidNotification();

        notification.Title = title;
        notification.Text = message;

        // TIME SET (change kar sakta hai)
        notification.FireTime = DateTime.Now.AddMinutes(5);

        notification.ShouldAutoCancel = true;

        AndroidNotificationCenter.SendNotification(notification, "game_channel");
    }
}