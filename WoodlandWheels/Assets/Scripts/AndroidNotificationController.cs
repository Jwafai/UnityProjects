using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotificationsController : MonoBehaviour
{
    #if UNITY_ANDROID
    private const string CHANNEL_ID = "channel_id";
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Life Ready",
            Text = "Your life is ready to play",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime,
        };
        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
    #endif
}