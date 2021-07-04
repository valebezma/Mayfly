using Mayfly;
using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using System;
using System.Windows.Forms;

namespace QuizManager
{
    public partial class MainForm : Form
    {
        Quiz Data;

        Quiz.RoundRow selectedRoundRow;

        public bool IsChanged { get; set; }

        private string fileName;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? FileSystem.GetNewFileCaption(UserSettings.Interface.Extension) : value, EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
            }
        }


        public MainForm()
        {
            InitializeComponent();

            Data = new Quiz();
        }

        private void LoadValues(string fileName)
        {
            Data = new Quiz();
            Data.ReadXml(fileName);
            LoadValues();
            FileName = fileName;

            IsChanged = false;
        }

        private void LoadValues()
        {
            textBoxGameTitle.Text = Data.Solitary.IsTitleNull() ? string.Empty : Data.Solitary.Title;

            foreach (Quiz.GameContentRow contentRow in Data.Solitary.GetGameContentRows())
            {
                if (contentRow.StageType == 0)
                {
                    AddRoundButton(contentRow.RoundRow);
                }
            }
        }

        private void SaveValues()
        {
            Quiz.GameRow gameRow = Data.Solitary;

            gameRow.Title = textBoxGameTitle.Text;

            //SaveRound();
            
        }

        private void SaveRound()
        {
            if (selectedRoundRow.GetGameContentRows().Length == 0)
            {
                Data.GameContent.AddGameContentRow(Data.Solitary, selectedRoundRow, 0, Data.GameContent.Count + 1);
            }

            selectedRoundRow.Title = textBoxRoundTitle.Text;
            selectedRoundRow.GettingTime = TimeSpan.FromSeconds((double)numericUpDownRoundTime.Value);

            foreach (Control button in flowLayoutPanelRounds.Controls)
            {
                if (!(button is RadioButton)) continue;

                if (button.Tag == selectedRoundRow)
                {
                    button.Text = selectedRoundRow.Title;
                    break;
                }
            }
        }

        private void Save(string fileName)
        {
            SaveValues();
            Data.WriteToFile(fileName);

            //statusCard.Message(Wild.Resources.Messages.Saved);
            FileName = fileName;
            IsChanged = false;
        }

        private DialogResult CheckAndSave()
        {
            DialogResult result = DialogResult.None;

            if (IsChanged)
            {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave)
                {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    result = DialogResult.OK;
                }
                else if (tdbPressed == tdbDiscard)
                {
                    IsChanged = false;
                    result = DialogResult.No;
                }
                else if (tdbPressed == tdbCancelClose)
                {
                    result = DialogResult.Cancel;
                }
            }

            return result;
        }

        private RadioButton AddRoundButton(Quiz.RoundRow roundRow)
        {
            RadioButton button = new RadioButton();
            button.Appearance = Appearance.Button;
            button.Text = roundRow.Title;
            button.Tag = roundRow;
            button.Click += buttonRound_Click;
            button.AutoSize = true;

            flowLayoutPanelRounds.Controls.Add(button);
            flowLayoutPanelRounds.SetFlowBreak(button, true);

            flowLayoutPanelRounds.Controls.Remove(buttonAddRound);
            flowLayoutPanelRounds.Controls.Add(buttonAddRound);

            return button;
        }

        void buttonRound_Click(object sender, EventArgs e)
        {
            selectedRoundRow = (Quiz.RoundRow)((RadioButton)sender).Tag;

            UpdateRound(selectedRoundRow);
        }

        private void UpdateRound(Quiz.RoundRow roundRow)
        {
            splitContainerContent.Panel2.Enabled = roundRow != null;

            textBoxRoundTitle.Text = roundRow.IsTitleNull() ? string.Empty : roundRow.Title;
            numericUpDownRoundTime.Value = roundRow.IsGettingTimeNull() ? 60 : (int)roundRow.GettingTime.TotalSeconds;

            while (flowLayoutPanelTasks.Controls.Count > 1)
            {
                flowLayoutPanelTasks.Controls.RemoveAt(0);
            }

            foreach (Quiz.RoundContentRow roundContentRow in roundRow.GetRoundContentRows())
            {
                AddTaskButton(roundContentRow.TaskRow);
            }
        }

        private Button AddTaskButton(Quiz.TaskRow taskRow)
        {
            Button button = new Button();
            button.Text = taskRow.Title;
            button.Tag = taskRow;
            button.Click += buttonTask_Click;
            button.AutoSize = true;

            flowLayoutPanelTasks.Controls.Add(button);
            flowLayoutPanelTasks.SetFlowBreak(button, true);

            flowLayoutPanelTasks.Controls.Remove(buttonAddTask);
            flowLayoutPanelTasks.Controls.Add(buttonAddTask);

            return button;
        }

        void buttonTask_Click(object sender, EventArgs e)
        {
            Task task = new Task((Quiz.TaskRow)((Button)sender).Tag);
            task.SetFriendlyDesktopLocation((Button)sender);
            task.FormClosed += task_FormClosed;
            task.Show(this);
        }

        void task_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Button button in flowLayoutPanelTasks.Controls)
            {
                if (button.Tag == ((Task)sender).TaskRow)
                {
                    button.Text = ((Task)sender).TaskRow.Title;
                    break;
                }
            }
        }



        private void value_Changed(object sender, EventArgs e)
        {
            SaveValues();
        }

        private void textBoxGameTitle_TextChanged(object sender, EventArgs e)
        {
            //buttonAddRound.Enabled = !string.IsNullOrWhiteSpace(textBoxGameTitle.Text);
            value_Changed(sender, e);
        }

        private void buttonAddRound_Click(object sender, EventArgs e)
        {
            // Create table record and load it to flowRounds;
            Quiz.RoundRow roundRow = Data.Round.NewRoundRow();
            roundRow.Title = string.Format("Round {0}", Data.Round.Count + 1);
            Data.Round.AddRoundRow(roundRow);

            selectedRoundRow = roundRow;
            RadioButton newButton = AddRoundButton(selectedRoundRow);
            newButton.Focus();
            newButton.PerformClick();

            textBoxRoundTitle.Focus();
            //UpdateRound(selectedRoundRow);
            //splitContainerContent.Panel2.Enabled = true;
        }

        private void round_Changed(object sender, EventArgs e)
        {
            SaveRound();
        }

        private void buttonAddTask_Click(object sender, EventArgs e)
        {
            // Create table record and load it to flowRounds;
            Quiz.TaskRow taskRow = Data.Task.NewTaskRow();

            int taskNo = Data.Task.Count + 1;

            taskRow.Title = string.Format("Task {0}", taskNo);
            taskRow.FullText = string.Format("Full text of task {0}", taskNo);
            taskRow.ShortText  = string.Format("Short text of task {0}", taskNo);
            taskRow.Cost = 1;

            Data.Task.AddTaskRow(taskRow);

            Data.RoundContent.AddRoundContentRow(selectedRoundRow, taskRow, taskNo - 1, TimeSpan.FromSeconds(60));

            Button newButton = AddTaskButton(taskRow);
            newButton.Focus();
            newButton.PerformClick();
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                menuItemSaveAs_Click(sender, e);
            }
            else
            {
                Save(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveValues();

            UserSettings.Interface.SaveDialog.FileName =
                FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
                Data.SuggestedName);

            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (UserSettings.Interface.OpenDialog.FileName == FileName)
                {
                    //statusCard.Message(Wild.Resources.Messages.AlreadyOpened);
                }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadValues(UserSettings.Interface.OpenDialog.FileName);
                    }
                }
            }
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
