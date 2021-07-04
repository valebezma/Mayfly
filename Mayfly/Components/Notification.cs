using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime;

namespace Mayfly
{
    public class Notification
    {
        private NotifyIcon TrayIcon;



        public Notification()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            if (TrayIcon != null) TrayIcon.Visible = false;
        }

        //private void InitializeComponent()
        //{
        //    TrayIcon = new NotifyIcon();
        //    TrayIcon.Icon = Resources.Interface.tray_icon;
        //    TrayIcon.Visible = true;
        //    TrayIcon.DoubleClick += TrayIcon_DoubleClick;
        //}

        public void Notify(string title, string instruction, Action a)
        {
            if (TrayIcon != null) TrayIcon_BalloonTipClosed(this, new EventArgs());

            TrayIcon = new NotifyIcon();

            TrayIcon.Icon = Resources.Interface.tray_icon;
            TrayIcon.BalloonTipTitle = title;
            TrayIcon.BalloonTipText = instruction;

            TrayIcon.DoubleClick += TrayIcon_DoubleClick;
            if (a != null)
            {
                EventHandler handler = new EventHandler((o, e) =>
                {
                    a.Invoke();
                    TrayIcon_BalloonTipClosed(this, new EventArgs());
                    Log.Write(EventType.NotificationReact, string.Format("Notification is clicked: {0}.", title));
                });
                UserSettings.GlobalNotification.TrayIcon.BalloonTipClicked += handler;
            }
            TrayIcon.BalloonTipClosed += TrayIcon_BalloonTipClosed;

            TrayIcon.Visible = true;
            TrayIcon.ShowBalloonTip(10000);

            Log.Write(EventType.NotificationShown, string.Format("Notification is shown: {0}.", title));
        }

        private void TrayIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
            TrayIcon = null;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (Application.OpenForms[0].WindowState == FormWindowState.Minimized) { Application.OpenForms[0].WindowState = FormWindowState.Normal; }
            Application.OpenForms[0].Show();
        }



        public static void ShowNotification(string title, string instruction, EventHandler handler)
        {
            UserSettings.GlobalNotification.Notify(title, instruction, () => { handler.Invoke(UserSettings.GlobalNotification, EventArgs.Empty); });
        }

        public static void ShowNotification(string title, string instruction)
        {
            UserSettings.GlobalNotification.Notify(title, instruction, null);
        }

        public static void ShowNotificationAction(string title, string instruction, Action a)
        {
            UserSettings.GlobalNotification.Notify(title, instruction, a);
        }
    }
}
