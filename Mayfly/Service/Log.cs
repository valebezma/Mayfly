using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Mayfly
{
    public abstract class Log
    {
        public static string FolderPath
        {
            get
            {
                return Path.Combine(FileSystem.UserFolder, "logs", UserSettings.Product);
            }
        }

        public static string CurrentFile
        {
            get
            {
                if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
                return Path.Combine(FolderPath, string.Format("{0:yyyy-MM-dd}.log", DateTime.Today));
            }
        }



        private static void Write(UserActionEventArgs e)
        {
            if (!UserSettings.Log) return;
            if (!File.Exists(CurrentFile)) { File.Create(CurrentFile).Close(); }
            File.AppendAllText(CurrentFile, e.GetLogLine().Replace(Environment.NewLine, " ") + Environment.NewLine);
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
            Write(EventType.ExceptionThrown, string.Format("Exception is thrown: {0}.", e.Message));
        }

        public static void WriteAppStarted()
        {
            Write(EventType.ApplicationStarted, "{0} session #{1} is started.", EntryAssemblyInfo.Title, Process.GetCurrentProcess().Id);
            Server.CheckUpdates(UserSettings.Product);
            Service.ResetUICulture();
            SendLog();
        }

        public static void WriteAppEnded()
        {
            if (UserSettings.LogSpan == 0)
            {
                File.Delete(CurrentFile);
            }
            else
            {
                Write(EventType.ApplicationEnded, "{0} session #{1} is ended.", EntryAssemblyInfo.Title, Process.GetCurrentProcess().Id);
            }
        }


        public static void ClearLog()
        {
            ClearLog(DateTime.Today.AddHours(12).AddDays(-UserSettings.LogSpan));
        }

        public static void ClearLog(DateTime safeDate)
        {
            foreach (string filename in Directory.GetFiles(FolderPath, "*.log"))
            {
                if (File.GetCreationTime(filename).Date < safeDate.Date)
                {
                    File.Delete(filename);
                }
            }
        }

        public static void SendLog()
        {
            // If send is off stop procedure
            if (!UserSettings.LogSend) return;

            // Remember when we last time send logs
            DateTime lastDgn = UserSettings.LogSentLast;

            // If it was today - stop procedure
            if ((DateTime.Today - lastDgn) == TimeSpan.Zero) return;

            // If it was earlier than today - start cycle from last departure day until today
            for (DateTime dt = lastDgn; dt < DateTime.Today; dt = dt.AddDays(1))
            {
                string logfilepath = Path.Combine(FolderPath, string.Format("{0:yyyy-MM-dd}.log", dt));
                if (File.Exists(logfilepath)) Server.UploadFileAsinc(logfilepath, Server.GetUri(Server.ServerLog, Path.GetFileName(logfilepath)));
            }

            // When ended - set new las departure value
            UserSettings.LogSentLast = DateTime.Today;
        }

        public static UserActionEventArgs[] GetEventsLog(UserActionEventArgsSearchTerms e)
        {
            List<UserActionEventArgs> result = new List<UserActionEventArgs>();

            if (Directory.Exists(FolderPath))
            foreach (string filename in Directory.GetFiles(FolderPath, "*.log"))
            {
                if (File.GetCreationTime(filename).Date.Date < e.FromDate.Date) continue;
                if (File.GetCreationTime(filename).Date.Date > e.ToDate.Date) continue;

                string[] loglines = File.ReadAllLines(filename);

                foreach (string logline in loglines)
                {
                    if (string.IsNullOrEmpty(logline)) continue;
                    UserActionEventArgs actionEvent = UserActionEventArgs.GetFromLine(logline);
                    if (e.Type != EventType.AllEvents && actionEvent.Type != e.Type) continue;
                    if (actionEvent.EventTime < e.FromDate) continue;
                    if (actionEvent.EventTime > e.ToDate) continue;
                    if (e.ProcessID != -1 && actionEvent.ProcessID != e.ProcessID) continue;
                    result.Add(actionEvent);
                }
            }

            return result.ToArray();
        }

        public static UserActionEventArgs[] GetEventsLog()
        {
            return GetEventsLog(new UserActionEventArgsSearchTerms());
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

        public DateTime EventTime { get; set; }

        public string Feature { get; set; }

        public int ProcessID { get; set; }

        public EventType Type { get; set; }

        public string EventDescription { get; set; }

        private UserActionEventArgs()
        {
            User = UserSettings.Username;
            EventTime = DateTime.Now;
            Feature = string.Format("{0} ({1})", EntryAssemblyInfo.Title, UserSettings.Product);
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
            return string.Format("{0:s}\t{1}\t{2}\t{3:000000}\t{4}\t{5}",
                EventTime, Feature, User, ProcessID, Log.ToString(Type), EventDescription);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", EventTime, EventDescription.Replace(Environment.NewLine, " "));
        }

        public static UserActionEventArgs GetFromLine(string logline)
        {
            string[] values = logline.Split('\t');

            UserActionEventArgs result = new UserActionEventArgs
            {
                EventTime = Convert.ToDateTime(values[0]),
                Feature = values[1],
                User = values[2],
                ProcessID = Convert.ToInt32(values[3]),
                Type = Log.GetType(values[4]),
                EventDescription = values[5],
            };

            return result;
        }
    }

    public class UserActionEventArgsSearchTerms
    {
        public DateTime FromDate;

        public DateTime ToDate;

        public EventType Type;

        public int ProcessID = -1;

        public UserActionEventArgsSearchTerms(DateTime from, DateTime to, EventType type)
        {
            FromDate = from;
            ToDate = to;
            Type = type;
        }

        public UserActionEventArgsSearchTerms() :
            this(DateTime.Today.AddDays(-1), DateTime.Now, EventType.AllEvents)
        {
            ProcessID = Process.GetCurrentProcess().Id;
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
