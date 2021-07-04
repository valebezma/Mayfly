using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Controls
{
    public partial class ProcessDisplay : Status
    {
        public ToolStripProgressBar ProgressBar 
        {
            get;

            set; 
        }

        public ProcessLook Look { get; set; }



        public ProcessDisplay()
        {

        }

        public ProcessDisplay(IContainer container)
        {
            container.Add(this);
        }


        delegate void TextEventHandler(ToolStripStatusLabel label, string text);

        public void SetStatus(ToolStripStatusLabel label, string text)
        {
            if (label.Owner.InvokeRequired)
            {
                TextEventHandler visibilitySetter = new TextEventHandler(SetStatus);
                label.Owner.Invoke(visibilitySetter, new object[] { label, text });
            }
            else
            {
                StatusLog.Text = text;
                StatusLog.Visible = true;
            }
        }

        public void SetStatus(string text)
        {
            SetStatus(StatusLog, text);
        }

        public void ResetStatus()
        {           
            if (StatusLog != null)
            {
                SetStatus(StatusLog, Default);
            }

            if (Look != null)
            {
                Look.SetStatus(Default);
            }

        }

        delegate void EventHandler();

        delegate void StatusSetter(int i, string s);

        public void StartProcessing(int max, string process)
        {
            if (ProgressBar == null) return;

            if (ProgressBar.Owner.InvokeRequired)
            {
                StatusSetter setter = new StatusSetter(StartProcessing);
                ProgressBar.Owner.Invoke(setter, new object[] { max, process });
            }
            else
            {
                ProgressBar.Style = ProgressBarStyle.Marquee;

                if (ProgressBar.Control.FindForm() != null)
                {
                    Taskbar.SetProgressState(ProgressBar.Control.FindForm().Handle,
                        ThumbnailProgressState.Indeterminate);
                }

                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = max;
                ProgressBar.Visible = true;

                if (ProgressBar.ProgressBar.FindForm() != null)
                    ProgressBar.ProgressBar.FindForm().Cursor = Cursors.AppStarting;

                if (Look != null)
                {
                    Look.StartProcessing(max, process);
                }

                Default = process;

                ResetStatus();
            }
        }

        public void ShowLook()
        {
            if (Look == null)
            {
                Look = new ProcessLook(this);
                Look.Disposed += Look_Disposed;
            }

            Look.Show();
            Look.BringToFront();
        }

        void Look_Disposed(object sender, EventArgs e)
        {
            Look = null;
        }

        public void StopProcessing()
        {
            if (ProgressBar != null)
            {
                if (ProgressBar.Control.FindForm() != null)
                {
                    Taskbar.SetProgressState(ProgressBar.Control.FindForm().Handle,
                        ThumbnailProgressState.NoProgress);
                }

                //StatusLoading.Maximum = 100;
                ProgressBar.Value = 0;
                ProgressBar.ProgressBar.FindForm().Cursor = Cursors.Default;
                ProgressBar.Visible = false;
            }

            if (StatusLog != null)
            {
                Default = string.Empty;
                StatusLog.Visible = false;
            }
        }

        public void HideLook()
        {
            Look.Close();
        }

        public void SetProgress(int value)
        {
            if (Look != null)
            {
                if (ProgressBar != null)
                {
                    if (ProgressBar.Style == ProgressBarStyle.Marquee)
                    {
                        ProgressBar.Style = ProgressBarStyle.Continuous;
                        Taskbar.SetProgressState(ProgressBar.Control.FindForm().Handle,
                            ThumbnailProgressState.Normal);
                    }

                    ProgressBar.Value = value;

                    Taskbar.SetProgressValue(ProgressBar.Control.FindForm().Handle,
                        (ulong)ProgressBar.Value, (ulong)ProgressBar.Maximum);
                }

                Look.SetProgress(value);
            }
        }
    }
}