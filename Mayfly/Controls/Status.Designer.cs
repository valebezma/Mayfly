namespace Mayfly.Controls
{
    partial class Status
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.logger = new System.ComponentModel.BackgroundWorker();
            // 
            // logger
            // 
            this.logger.DoWork += new System.ComponentModel.DoWorkEventHandler(this.logger_DoWork);
            this.logger.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.logger_RunWorkerCompleted);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker logger;
    }
}
