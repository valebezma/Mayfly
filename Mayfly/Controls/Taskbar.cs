// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Mayfly.Controls
{
    /// <summary>
    /// The primary coordinator of the Windows 7 taskbar-related activities.
    /// </summary>
    public static class Taskbar
    {
        private static ITaskbarList3 taskbarList;
        internal static ITaskbarList3 TaskbarList
        {
            get
            {
                if (taskbarList == null)
                {
                    lock (typeof(Taskbar))
                    {
                        if (taskbarList == null)
                        {
                            taskbarList = (ITaskbarList3)new CTaskbarList();
                            taskbarList.HrInit();
                        }
                    }
                }
                return taskbarList;
            }
        }

        static readonly OperatingSystem osInfo = Environment.OSVersion;

        internal static bool IsOSCompatible
        {
            get
            {
                return (osInfo.Version.Major == 6 && osInfo.Version.Minor >= 1)
                    || (osInfo.Version.Major > 6);
            }
        }

        /// <summary>
        /// Sets the progress state of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="state">The progress state.</param>
        public static void SetProgressState(IntPtr hwnd, ThumbnailProgressState state)
        {
            if(IsOSCompatible)
                TaskbarList.SetProgressState(hwnd, state);
        }
        /// <summary>
        /// Sets the progress value of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="current">The current value.</param>
        /// <param name="maximum">The maximum value.</param>
        public static void SetProgressValue(IntPtr hwnd, ulong current, ulong maximum)
        {
            if(IsOSCompatible)
                TaskbarList.SetProgressValue(hwnd, current, maximum);

            if (current == maximum)
            {
                SetProgressState(hwnd, ThumbnailProgressState.NoProgress);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}