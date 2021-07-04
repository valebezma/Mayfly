using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public partial class ProcessLook : Form
    {
        new ProcessDisplay Parent { get; set; }



        public ProcessLook(ProcessDisplay display)
        {
            InitializeComponent();
            Parent = display;
        }



        public void StartProcessing(int max, string process)
        {
            SetStatus(process);
            SetProgressMax(max);
            SetProgress(0);
        }



        delegate void TextEventHandler(Label label, string text);

        public void SetStatus(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                TextEventHandler visibilitySetter = new TextEventHandler(SetStatus);
                label.Invoke(visibilitySetter, new object[] { label, text });
            }
            else
            {
                label1.Text = text;
            }
        }

        public void SetStatus(string text)
        {
            SetStatus(label1, text);
        }



        delegate void ProgressEventHandler(ProgressBar bar, int value);

        public void SetProgress(ProgressBar bar, int value)
        {
            if (bar.InvokeRequired)
            {
                ProgressEventHandler visibilitySetter = new ProgressEventHandler(SetProgress);
                bar.Invoke(visibilitySetter, new object[] { bar, value });
            }
            else
            {
                if (bar.Style == ProgressBarStyle.Marquee)
                {
                    bar.Style = ProgressBarStyle.Continuous;
                }

                bar.Value = value;
            }
        }

        public void SetProgress(int value)
        {
            SetProgress(progressBar, value);
        }


        public void SetProgressMax(ProgressBar bar, int value)
        {
            if (bar.InvokeRequired)
            {
                ProgressEventHandler visibilitySetter = new ProgressEventHandler(SetProgressMax);
                bar.Invoke(visibilitySetter, new object[] { bar, value });
            }
            else
            {
                bar.Maximum = value;
            }
        }

        public void SetProgressMax(int value)
        {
            SetProgressMax(progressBar, value);
        }
    }
}
