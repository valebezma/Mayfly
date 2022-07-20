using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using Mayfly.Extensions;

namespace Mayfly.Software
{
    public partial class About : Form
    {
        string product;



        public About()
        {
            InitializeComponent();

            product = UserSettings.Product;

            labelVersion.ResetFormatted(EntryAssemblyInfo.Version);
            labelCopyright.ResetFormatted(DateTime.Today, EntryAssemblyInfo.Copyright);
            label1.ResetFormatted(EntryAssemblyInfo.Title);
        }

        public About(Image banner) 
            : this()
        {
            pictureLogo.Image = banner;

            try
            {
                Icon icon = Service.GetIcon(Application.ExecutablePath, 0, new Size(256, 256));
                if (icon == null) return;
                Bitmap icon_bmp = new Icon(icon, pictureAppIcon.Size).ToBitmap();
                pictureAppIcon.Image = icon_bmp;

                panel1.BackColor = Service.GetAverageColor(icon_bmp);
            }
            catch { }
        }



        private void SetIcon(Icon icon)
        {
            if (icon == null) return;
            pictureAppIcon.Image = new Icon(icon, pictureAppIcon.Size).ToBitmap();
        }

        public void SetPowered(Image image, string remark)
        {
            picturePowered.Image = image;
            labelPowered.Text = remark;
        }



        private void buttonLic_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.OpenFeatures();
            settings.ShowDialog();
        }

        private void buttonFeedback_Click(object sender, EventArgs e)
        {
            Server.SendEmail(Server.GetEmail("feedback"), 
                string.Format(Resources.Interface.FeedbackSubject, EntryAssemblyInfo.Title, UserSettings.Username),
                string.Format(Resources.Interface.FeedbackBody, UserSettings.Username, EntryAssemblyInfo.Title));
        }

        private void buttonUpdates_Click(object sender, EventArgs e)
        {
            Server.DoUpdates(product);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
