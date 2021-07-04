using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Software
{
    public partial class Error : Form
    {
        Exception Exception;

        public Error(string message)
        {
            InitializeComponent();
            labelMessage.Text = message;
        }

        public Error(Exception exception) : this(exception.Message)
        {
            Exception = exception;

            //textBoxStackTrace.AppendText("Session log:" + Environment.NewLine);
            //foreach (UserActionEventArgs e in Log.GetEventsLog()) {
            //    textBoxStackTrace.AppendText("   " + e.ToString() + Environment.NewLine);
            //}


            //textBoxStackTrace.AppendText(Environment.NewLine);
            textBoxStackTrace.AppendText("Debugging log:" + Environment.NewLine);
            textBoxStackTrace.AppendText(Exception.StackTrace);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Server.SendEmail(Server.GetEmail("error"),
                string.Format(Resources.Interface.ErrorReportSubject, EntryAssemblyInfo.Title),
                string.Format(Resources.Interface.ErrorReportBody, EntryAssemblyInfo.Title, Exception.Message, Exception.StackTrace));
            Close();
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            Height += 200;
        }

        private void Error_SizeChanged(object sender, EventArgs e)
        {
            textBoxStackTrace.Visible = textBoxStackTrace.Height > 39;
        }

        private void textBoxStackTrace_VisibleChanged(object sender, EventArgs e)
        {
            buttonDetails.Visible = !textBoxStackTrace.Visible;
        }

        private void Error_Load(object sender, EventArgs e)
        {

        }
    }
}
