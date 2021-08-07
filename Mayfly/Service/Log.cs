using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mayfly
{
    public abstract class Log
    {
        private static void Write(UserActionEventArgs e)
        {
            if (!UserSettings.ShareDiagnostics) return;
            Uri uri = Server.GetUri(Server.ServerHttps, "php/software/log.php");
            Dictionary<string, string> logParameters = new Dictionary<string, string>
            {
                { "logevent", e.GetLogLine() },
                { "login", "mayfly-logger" },
                { "password", "qe4-nsw-wv8-WrC" }
            };
            try { Server.GetText(uri, logParameters); } catch { }
        }

        public static void Write(string message)
        {
            Write(new UserActionEventArgs(message));
        }

        public static void Write(string format, params object[] values)
        {
            Write(string.Format(format, values));
        }

        public static void Write(EventType eventType, string message)
        {
            Write(new UserActionEventArgs(message, eventType));
        }

        public static void Write(EventType eventType, string format, params object[] values)
        {
            Write(eventType, string.Format(format, values));
        }

        public static void Write(Exception e)
        {
            Write(EventType.ExceptionThrown, string.Format("Exception is thrown: {0}", e.Message));
        }

        public static void WriteAppStarted()
        {
            Write(EventType.ApplicationStarted, string.Empty);
            Server.CheckUpdates(UserSettings.Product);
            Service.ResetUICulture();
            Licensing.InspectLicensesExpiration();
        }

        public static void WriteAppEnded()
        {
            Write(EventType.ApplicationEnded, string.Empty);
        }

        public static EventType GetType(string eventCode)
        {
            EventType eventtype = EventType.CommonEvent;

            switch (eventCode)
            {
                case "eoa":
                    eventtype = EventType.ApplicationEnded;
                    break;
                case "soa":
                    eventtype = EventType.ApplicationStarted;
                    break;
                case "sow":
                    eventtype = EventType.WizardStarted;
                    break;
                case "eow":
                    eventtype = EventType.WizardEnded;
                    break;
                case "rep":
                    eventtype = EventType.ReportBuilt;
                    break;
                case "err":
                    eventtype = EventType.ExceptionThrown;
                    break;
                case "not":
                    eventtype = EventType.NotificationShown;
                    break;
                case "nor":
                    eventtype = EventType.NotificationReact;
                    break;
                case "mtn":
                    eventtype = EventType.Maintenance;
                    break;
            }

            return eventtype;
        }

        public static string ToString(EventType eventType)
        {
            string eventcode = "cev";

            switch (eventType)
            {
                case EventType.Maintenance:
                    eventcode = "mtn";
                    break;
                case EventType.ApplicationEnded:
                    eventcode = "eoa";
                    break;
                case EventType.ApplicationStarted:
                    eventcode = "soa";
                    break;
                case EventType.WizardStarted:
                    eventcode = "sow";
                    break;
                case EventType.WizardEnded:
                    eventcode = "eow";
                    break;
                case EventType.ReportBuilt:
                    eventcode = "rep";
                    break;
                case EventType.ExceptionThrown:
                    eventcode = "err";
                    break;
                case EventType.NotificationShown:
                    eventcode = "not";
                    break;
                case EventType.NotificationReact:
                    eventcode = "nor";
                    break;
            }

            return eventcode;
        }
    }

    public class UserActionEventArgs
    {
        public string User { get; set; }

        public string HardwareID { get; set; }

        public DateTime EventTime { get; set; }

        public string Feature { get; set; }

        public string Version { get; set; }

        public string Product { get; set; }

        public int ProcessID { get; set; }

        public EventType Type { get; set; }

        public string EventDescription { get; set; }

        private UserActionEventArgs()
        {
            HardwareID = Hardware.HardwareID;
            EventTime = DateTime.UtcNow;
            Product = UserSettings.Product;
            Feature = Process.GetCurrentProcess().ProcessName;
            Version = EntryAssemblyInfo.Version;
            User = UserSettings.Username;
            ProcessID = Process.GetCurrentProcess().Id;
            Type = EventType.AllEvents;
            EventDescription = string.Empty;
        }

        public UserActionEventArgs(string message, EventType type)
            : this()
        {
            EventDescription = message;
            Type = type;
        }

        public UserActionEventArgs(string message)
            : this(message, EventType.CommonEvent)
        { }

        public string GetLogLine()
        {
            return GetLogLine(';');
        }

        public string GetLogLine(char delimiter)
        {
            return string.Format("{0:s}" + delimiter + "{1}" + delimiter + "{2}" + delimiter + "{3}" +
                delimiter + "{4}" + delimiter + "{5:000000}" + delimiter + "{6}" + delimiter + "{7}" + delimiter + "{8}",
                EventTime, Product, Feature, Version, User, ProcessID, Log.ToString(Type), EventDescription, HardwareID);
        }
    }

    public enum EventType
    {
        AllEvents,
        CommonEvent,
        Maintenance,
        WizardStarted,
        WizardEnded,
        ReportBuilt,
        ApplicationStarted,
        ApplicationEnded,
        NotificationShown,
        NotificationReact,
        ExceptionThrown
    }
}
