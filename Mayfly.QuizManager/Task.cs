using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace QuizManager
{
    public partial class Task : Form
    {
        public Quiz.TaskRow TaskRow;

        public EventHandler TitleChanged { set; get; }



        private Task()
        {
            InitializeComponent();
            listViewMedia.Shine();
        }

        public Task(Quiz.TaskRow taskRow) : this()
        {
            TaskRow = taskRow;
            UpdateValues();
        }


        private void SaveValues()
        {
            TaskRow.Title = textBoxTitle.Text;
            TaskRow.FullText = textBoxFullText.Text;
            TaskRow.ShortText = textBoxShortText.Text;
            TaskRow.Answer = textBoxAnswer.Text;
            TaskRow.Cost = numericUpDownCost.Value;
        }

        private void UpdateValues()
        {
            Text = TaskRow.Title;

            textBoxTitle.Text = TaskRow.IsTitleNull() ? string.Empty : TaskRow.Title;
            textBoxFullText.Text = TaskRow.IsFullTextNull() ? string.Empty : TaskRow.FullText;
            textBoxShortText.Text = TaskRow.IsShortTextNull() ? string.Empty : TaskRow.ShortText;
            textBoxAnswer.Text = TaskRow.IsAnswerNull() ? string.Empty : TaskRow.Answer;
            numericUpDownCost.Value = TaskRow.Cost;
        
            foreach (Quiz.TaskContentRow taskContentRow in TaskRow.GetTaskContentRows())
            {
                LoadIcons(taskContentRow);
            }
        }

        private void LoadIcons(Quiz.TaskContentRow taskContentRow)
        {
            Image im = taskContentRow.MediaRow.Data.GetImage();

            Image thumbnail = im.Fit(icons.ImageSize);
            icons.Images.Add(thumbnail);

            ListViewItem li = listViewMedia.CreateItem(taskContentRow.MediaRow.ID.ToString());
            li.ImageIndex = icons.Images.Count - 1;

        }


        private void Task_Load(object sender, EventArgs e)
        {

        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            //Text = string.IsNullOrWhiteSpace(textBoxTitle.Text) ? "" : textBoxTitle.Text;
            if (TitleChanged != null) TitleChanged.Invoke(this, e);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveValues();
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (UserSettings.InterfaceMedia.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in UserSettings.InterfaceMedia.OpenDialog.FileNames)
                {
                    Image im = Image.FromFile(path);


                    Quiz.MediaRow mediaRow = ((Quiz)TaskRow.Table.DataSet).Media.NewMediaRow();
                    mediaRow.Data = im.ToByteArray();
                    ((Quiz)TaskRow.Table.DataSet).Media.AddMediaRow(mediaRow);

                    Quiz.TaskContentRow taskContentRow = ((Quiz)TaskRow.Table.DataSet).TaskContent.NewTaskContentRow();
                    taskContentRow.TaskRow = TaskRow;
                    taskContentRow.MediaRow = mediaRow;
                    ((Quiz)TaskRow.Table.DataSet).TaskContent.AddTaskContentRow(taskContentRow);

                    LoadIcons(taskContentRow);
                }
            }
        }
    }
}
