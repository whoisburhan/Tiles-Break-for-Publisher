using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

namespace GS.AA
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        public string[] NotificationTexts;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            AndroidNotificationCenter.CancelAllNotifications();

            ChannelSetUp();
        }

        private void ChannelSetUp()
        {
            var channel1 = new AndroidNotificationChannel()
            {
                Id = "reminder_1",
                Name = "Reminder1",
                Importance = Importance.High,
                Description = "Reminder notification 1"
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel1);
        }

        private void SetNotifications()
        {
            #region Notification
            var notification1 = new AndroidNotification();
            notification1.Title = "Hi! Its Sarah....";
            notification1.Text = NotificationTexts[Random.Range(0,NotificationTexts.Length)];
            notification1.FireTime = System.DateTime.Now.AddHours(12); 
            //notification1.FireTime = System.DateTime.Now.AddMinutes(1);
            notification1.LargeIcon = "sarah_0";


            var id1 = AndroidNotificationCenter.SendNotification(notification1, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id1) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id1);
                id1 = AndroidNotificationCenter.SendNotification(notification1, "reminder_1");
            }
            #endregion

            #region Notification
            var notification2 = new AndroidNotification();
            notification2.Title = "Hi! Its Sarah....";
            notification2.Text = NotificationTexts[Random.Range(0, NotificationTexts.Length)];
            notification2.FireTime = System.DateTime.Now.AddHours(24); 
           // notification2.FireTime = System.DateTime.Now.AddMinutes(2);
            notification2.LargeIcon = "sarah_0";


            var id2 = AndroidNotificationCenter.SendNotification(notification2, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id2) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id2);
                id2 = AndroidNotificationCenter.SendNotification(notification2, "reminder_1");
            }
            #endregion

            #region Notification
            var notification3 = new AndroidNotification();
            notification3.Title = "Hi! Its Sarah....";
            notification3.Text = NotificationTexts[Random.Range(0, NotificationTexts.Length)];
            notification3.FireTime = System.DateTime.Now.AddHours(48); 
           // notification3.FireTime = System.DateTime.Now.AddMinutes(3);
            notification3.LargeIcon = "sarah_0";


            var id3 = AndroidNotificationCenter.SendNotification(notification3, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id3) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id3);
                id3 = AndroidNotificationCenter.SendNotification(notification3, "reminder_1");
            }
            #endregion

            #region Notification
            var notification4 = new AndroidNotification();
            notification4.Title = "Hi! Its Sarah....";
            notification4.Text = NotificationTexts[Random.Range(0, NotificationTexts.Length)];
            notification4.FireTime = System.DateTime.Now.AddHours(72); 
           // notification4.FireTime = System.DateTime.Now.AddMinutes(4);
            notification4.LargeIcon = "sarah_0";


            var id4 = AndroidNotificationCenter.SendNotification(notification4, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id4) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id4);
                id4 = AndroidNotificationCenter.SendNotification(notification4, "reminder_1");
            }
            #endregion

            #region Notification
            var notification5 = new AndroidNotification();
            notification5.Title = "Hi! Its Sarah....";
            notification5.Text = NotificationTexts[Random.Range(0, NotificationTexts.Length)];
            notification5.FireTime = System.DateTime.Now.AddHours(120); 
           // notification5.FireTime = System.DateTime.Now.AddMinutes(5);
            notification5.LargeIcon = "sarah_0";


            var id5 = AndroidNotificationCenter.SendNotification(notification5, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id5) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id5);
                id5 = AndroidNotificationCenter.SendNotification(notification5, "reminder_1");
            }
            #endregion

            #region Notification
            var notification6 = new AndroidNotification();
            notification6.Title = "Hi! Its Sarah....";
            notification6.Text = NotificationTexts[Random.Range(0, NotificationTexts.Length)];
            notification6.FireTime = System.DateTime.Now.AddHours(168); 
           // notification6.FireTime = System.DateTime.Now.AddMinutes(6);
            notification6.LargeIcon = "sarah_0";


            var id6 = AndroidNotificationCenter.SendNotification(notification6, "reminder_1");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id6) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelScheduledNotification(id6);
                id6 = AndroidNotificationCenter.SendNotification(notification6, "reminder_1");
            }
            #endregion

        }

        private void OnApplicationQuit()
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidNotificationCenter.CancelAllNotifications();
            SetNotifications();
        }

        private void OnApplicationPause(bool pause)
        {
            if (Application.platform != RuntimePlatform.Android) return;

            if (pause)
            {
                AndroidNotificationCenter.CancelAllNotifications();
                SetNotifications();
            }
            else
            {
                AndroidNotificationCenter.CancelAllNotifications();
            }
        }
    }
}