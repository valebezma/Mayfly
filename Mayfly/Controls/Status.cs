using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public partial class Status : Component
    {
        #region Properties

        public ToolStripStatusLabel StatusLog { get; set; }

        public int MaximalInterval { get; set; }

        public string Default { get; set; }

        private List<string> StatusMessages = new List<string>();

        public int StatusMessagesCount;

        #endregion

        #region Constructors

        public Status()
        {
            InitializeComponent();
            MaximalInterval = 2000;
        }

        #endregion

        #region Methods

        public void Message(string format, params object[] values)
        {
            Message(string.Format(format, values));
        }

        public void Message(string statusMessage)
        {
            StatusMessages.Add(statusMessage);
            StatusMessagesCount = StatusMessages.Count;

            if (!logger.IsBusy)
            {
                StatusLog.Visible = true;
                StatusLog.Text = StatusMessages[0];
                logger.RunWorkerAsync();
            }
        }

        public void Message(string[] statusMessages)
        {
            StatusMessages.AddRange(statusMessages);
            StatusMessagesCount = StatusMessages.Count;

            if (!logger.IsBusy)
            {
                StatusLog.Visible = true;
                StatusLog.Text = StatusMessages[0];
                logger.RunWorkerAsync();
            }
        }

        #endregion

        #region Interface logics

        private void logger_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(MaximalInterval / StatusMessagesCount);
            StatusMessages.RemoveAt(0);
        }

        private void logger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (StatusMessages.Count > 0)
            {
                StatusLog.Text = StatusMessages[0];
                logger.RunWorkerAsync();
            }
            else
            {
                if (Default != null)
                {
                    StatusLog.Text = Default;
                }
                else
                {
                    StatusLog.Text = string.Empty;
                    StatusLog.Visible = false;
                }
            }
        }

        #endregion
    }
}
