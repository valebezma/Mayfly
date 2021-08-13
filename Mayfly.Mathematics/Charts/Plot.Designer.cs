namespace Mayfly.Mathematics.Charts
{
    partial class Plot
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plot));
            this.contextChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextChartProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextChartAxes = new System.Windows.Forms.ToolStripMenuItem();
            this.contextChartAdjustAxes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextChartSeparate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextChartPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSizeA4 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSizeA5 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSizeA6 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextScatterplotProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotAddTrend = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotDistinguish = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextScatterplotFindValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.contextScatterplotStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotDescriptiveX = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotDescriptiveY = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotTrend = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotTrendCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotHistogramX = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotApart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextScatterplotUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogram = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextHistogramProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramAddFit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramDistinguish = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramCopyValues = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextHistogramCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextHistogramCopyDatapoints = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextHistogramStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramDescriptive = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramApart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHistogramUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEvaluation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextEvalProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEvalDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.contextEvalStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEvalPointDescriptive = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEvalApart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEvalUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFunctor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextFunctorProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextChart.SuspendLayout();
            this.contextScatterplot.SuspendLayout();
            this.contextHistogram.SuspendLayout();
            this.contextEvaluation.SuspendLayout();
            this.contextFunctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextChart
            // 
            this.contextChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextChartProperties,
            this.contextChartAxes,
            this.contextChartAdjustAxes,
            this.toolStripSeparator2,
            this.contextChartSeparate,
            this.toolStripSeparator4,
            this.contextChartPrint});
            this.contextChart.Name = "contextMenuStrip_chart";
            resources.ApplyResources(this.contextChart, "contextChart");
            this.contextChart.Opening += new System.ComponentModel.CancelEventHandler(this.contextChart_Opening);
            // 
            // contextChartProperties
            // 
            resources.ApplyResources(this.contextChartProperties, "contextChartProperties");
            this.contextChartProperties.Name = "contextChartProperties";
            this.contextChartProperties.Click += new System.EventHandler(this.contextChartProperties_Click);
            // 
            // contextChartAxes
            // 
            this.contextChartAxes.Name = "contextChartAxes";
            resources.ApplyResources(this.contextChartAxes, "contextChartAxes");
            this.contextChartAxes.Click += new System.EventHandler(this.contextChartAxes_Click);
            // 
            // contextChartAdjustAxes
            // 
            this.contextChartAdjustAxes.Name = "contextChartAdjustAxes";
            resources.ApplyResources(this.contextChartAdjustAxes, "contextChartAdjustAxes");
            this.contextChartAdjustAxes.Click += new System.EventHandler(this.contextChartAdjustAxes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // contextChartSeparate
            // 
            this.contextChartSeparate.CheckOnClick = true;
            this.contextChartSeparate.Name = "contextChartSeparate";
            resources.ApplyResources(this.contextChartSeparate, "contextChartSeparate");
            this.contextChartSeparate.CheckedChanged += new System.EventHandler(this.contextChartSeparate_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // contextChartPrint
            // 
            this.contextChartPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextSizeA4,
            this.contextSizeA5,
            this.contextSizeA6});
            this.contextChartPrint.Name = "contextChartPrint";
            resources.ApplyResources(this.contextChartPrint, "contextChartPrint");
            // 
            // contextSizeA4
            // 
            this.contextSizeA4.Name = "contextSizeA4";
            resources.ApplyResources(this.contextSizeA4, "contextSizeA4");
            this.contextSizeA4.Click += new System.EventHandler(this.contextSizeA4_Click);
            // 
            // contextSizeA5
            // 
            this.contextSizeA5.Name = "contextSizeA5";
            resources.ApplyResources(this.contextSizeA5, "contextSizeA5");
            this.contextSizeA5.Click += new System.EventHandler(this.contextSizeA5_Click);
            // 
            // contextSizeA6
            // 
            this.contextSizeA6.Name = "contextSizeA6";
            resources.ApplyResources(this.contextSizeA6, "contextSizeA6");
            this.contextSizeA6.Click += new System.EventHandler(this.contextSizeA6_Click);
            // 
            // contextScatterplot
            // 
            this.contextScatterplot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextScatterplotProperties,
            this.contextScatterplotAddTrend,
            this.contextScatterplotDistinguish,
            this.contextScatterplotDelete,
            this.toolStripSeparator3,
            this.contextScatterplotFindValue,
            this.toolStripSeparator5,
            this.contextScatterplotStatistics,
            this.contextScatterplotTransform,
            this.contextScatterplotApart,
            this.contextScatterplotUpdate});
            this.contextScatterplot.Name = "contextMenuStripRegression";
            resources.ApplyResources(this.contextScatterplot, "contextScatterplot");
            this.contextScatterplot.Opening += new System.ComponentModel.CancelEventHandler(this.contextScatterplot_Opening);
            // 
            // contextScatterplotProperties
            // 
            resources.ApplyResources(this.contextScatterplotProperties, "contextScatterplotProperties");
            this.contextScatterplotProperties.Name = "contextScatterplotProperties";
            this.contextScatterplotProperties.Click += new System.EventHandler(this.contextScatterplotProperties_Click);
            // 
            // contextScatterplotAddTrend
            // 
            this.contextScatterplotAddTrend.Name = "contextScatterplotAddTrend";
            resources.ApplyResources(this.contextScatterplotAddTrend, "contextScatterplotAddTrend");
            this.contextScatterplotAddTrend.Click += new System.EventHandler(this.contextScatterplotAddTrend_Click);
            // 
            // contextScatterplotDistinguish
            // 
            this.contextScatterplotDistinguish.CheckOnClick = true;
            this.contextScatterplotDistinguish.Name = "contextScatterplotDistinguish";
            resources.ApplyResources(this.contextScatterplotDistinguish, "contextScatterplotDistinguish");
            this.contextScatterplotDistinguish.CheckedChanged += new System.EventHandler(this.contextScatterplotDistinguish_CheckedChanged);
            // 
            // contextScatterplotDelete
            // 
            resources.ApplyResources(this.contextScatterplotDelete, "contextScatterplotDelete");
            this.contextScatterplotDelete.Name = "contextScatterplotDelete";
            this.contextScatterplotDelete.Click += new System.EventHandler(this.contextScatterplotDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // contextScatterplotFindValue
            // 
            this.contextScatterplotFindValue.Name = "contextScatterplotFindValue";
            resources.ApplyResources(this.contextScatterplotFindValue, "contextScatterplotFindValue");
            this.contextScatterplotFindValue.Click += new System.EventHandler(this.contextScatterplotFindValue_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // contextScatterplotStatistics
            // 
            this.contextScatterplotStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextScatterplotDescriptiveX,
            this.contextScatterplotDescriptiveY,
            this.contextScatterplotTrend,
            this.contextScatterplotTrendCompare});
            this.contextScatterplotStatistics.Name = "contextScatterplotStatistics";
            resources.ApplyResources(this.contextScatterplotStatistics, "contextScatterplotStatistics");
            // 
            // contextScatterplotDescriptiveX
            // 
            this.contextScatterplotDescriptiveX.Name = "contextScatterplotDescriptiveX";
            resources.ApplyResources(this.contextScatterplotDescriptiveX, "contextScatterplotDescriptiveX");
            this.contextScatterplotDescriptiveX.Click += new System.EventHandler(this.contextScatterplotDescriptiveX_Click);
            // 
            // contextScatterplotDescriptiveY
            // 
            this.contextScatterplotDescriptiveY.Name = "contextScatterplotDescriptiveY";
            resources.ApplyResources(this.contextScatterplotDescriptiveY, "contextScatterplotDescriptiveY");
            this.contextScatterplotDescriptiveY.Click += new System.EventHandler(this.contextScatterplotDescriptiveY_Click);
            // 
            // contextScatterplotTrend
            // 
            this.contextScatterplotTrend.Name = "contextScatterplotTrend";
            resources.ApplyResources(this.contextScatterplotTrend, "contextScatterplotTrend");
            this.contextScatterplotTrend.Click += new System.EventHandler(this.contextScatterplotTrend_Click);
            // 
            // contextScatterplotTrendCompare
            // 
            this.contextScatterplotTrendCompare.Name = "contextScatterplotTrendCompare";
            resources.ApplyResources(this.contextScatterplotTrendCompare, "contextScatterplotTrendCompare");
            this.contextScatterplotTrendCompare.Click += new System.EventHandler(this.contextScatterplotTrendCompare_Click);
            // 
            // contextScatterplotTransform
            // 
            this.contextScatterplotTransform.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextScatterplotMerge,
            this.contextScatterplotSplit,
            this.contextScatterplotHistogramX});
            this.contextScatterplotTransform.Name = "contextScatterplotTransform";
            resources.ApplyResources(this.contextScatterplotTransform, "contextScatterplotTransform");
            // 
            // contextScatterplotMerge
            // 
            this.contextScatterplotMerge.Name = "contextScatterplotMerge";
            resources.ApplyResources(this.contextScatterplotMerge, "contextScatterplotMerge");
            this.contextScatterplotMerge.Click += new System.EventHandler(this.contextScatterplotMerge_Click);
            // 
            // contextScatterplotSplit
            // 
            this.contextScatterplotSplit.Name = "contextScatterplotSplit";
            resources.ApplyResources(this.contextScatterplotSplit, "contextScatterplotSplit");
            this.contextScatterplotSplit.Click += new System.EventHandler(this.contextScatterplotSplit_Click);
            // 
            // contextScatterplotHistogramX
            // 
            this.contextScatterplotHistogramX.Name = "contextScatterplotHistogramX";
            resources.ApplyResources(this.contextScatterplotHistogramX, "contextScatterplotHistogramX");
            this.contextScatterplotHistogramX.Click += new System.EventHandler(this.contextScatterplotHistogramX_Click);
            // 
            // contextScatterplotApart
            // 
            this.contextScatterplotApart.Name = "contextScatterplotApart";
            resources.ApplyResources(this.contextScatterplotApart, "contextScatterplotApart");
            this.contextScatterplotApart.Click += new System.EventHandler(this.contextItemScatterplotApart_Click);
            // 
            // contextScatterplotUpdate
            // 
            resources.ApplyResources(this.contextScatterplotUpdate, "contextScatterplotUpdate");
            this.contextScatterplotUpdate.Name = "contextScatterplotUpdate";
            this.contextScatterplotUpdate.Click += new System.EventHandler(this.contextScatterplotUpdate_Click);
            // 
            // contextHistogram
            // 
            this.contextHistogram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextHistogramProperties,
            this.contextHistogramAddFit,
            this.contextHistogramDistinguish,
            this.contextHistogramDelete,
            this.contextHistogramCopyValues,
            this.toolStripSeparator1,
            this.contextHistogramStatistics,
            this.contextHistogramTransform,
            this.contextHistogramApart,
            this.contextHistogramUpdate});
            this.contextHistogram.Name = "contextMenuStripHistogram";
            resources.ApplyResources(this.contextHistogram, "contextHistogram");
            this.contextHistogram.Opening += new System.ComponentModel.CancelEventHandler(this.contextHistogram_Opening);
            // 
            // contextHistogramProperties
            // 
            resources.ApplyResources(this.contextHistogramProperties, "contextHistogramProperties");
            this.contextHistogramProperties.Name = "contextHistogramProperties";
            this.contextHistogramProperties.Click += new System.EventHandler(this.contextHistogramProperties_Click);
            // 
            // contextHistogramAddFit
            // 
            this.contextHistogramAddFit.Name = "contextHistogramAddFit";
            resources.ApplyResources(this.contextHistogramAddFit, "contextHistogramAddFit");
            this.contextHistogramAddFit.Click += new System.EventHandler(this.contextHistogramAddFit_Click);
            // 
            // contextHistogramDistinguish
            // 
            this.contextHistogramDistinguish.CheckOnClick = true;
            this.contextHistogramDistinguish.Name = "contextHistogramDistinguish";
            resources.ApplyResources(this.contextHistogramDistinguish, "contextHistogramDistinguish");
            this.contextHistogramDistinguish.CheckedChanged += new System.EventHandler(this.contextHistogramDistinguish_CheckedChanged);
            // 
            // contextHistogramDelete
            // 
            resources.ApplyResources(this.contextHistogramDelete, "contextHistogramDelete");
            this.contextHistogramDelete.Name = "contextHistogramDelete";
            this.contextHistogramDelete.Click += new System.EventHandler(this.contextHistogramDelete_Click);
            // 
            // contextHistogramCopyValues
            // 
            this.contextHistogramCopyValues.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextHistogramCopyAll,
            this.ContextHistogramCopyDatapoints});
            this.contextHistogramCopyValues.Name = "contextHistogramCopyValues";
            resources.ApplyResources(this.contextHistogramCopyValues, "contextHistogramCopyValues");
            this.contextHistogramCopyValues.Click += new System.EventHandler(this.contextHistogamCopyValues_Click);
            // 
            // ContextHistogramCopyAll
            // 
            this.ContextHistogramCopyAll.Name = "ContextHistogramCopyAll";
            resources.ApplyResources(this.ContextHistogramCopyAll, "ContextHistogramCopyAll");
            this.ContextHistogramCopyAll.Click += new System.EventHandler(this.contextHistogamCopyValues_Click);
            // 
            // ContextHistogramCopyDatapoints
            // 
            this.ContextHistogramCopyDatapoints.Name = "ContextHistogramCopyDatapoints";
            resources.ApplyResources(this.ContextHistogramCopyDatapoints, "ContextHistogramCopyDatapoints");
            this.ContextHistogramCopyDatapoints.Click += new System.EventHandler(this.ContextHistogramCopyDatapoints_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // contextHistogramStatistics
            // 
            this.contextHistogramStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextHistogramDescriptive});
            this.contextHistogramStatistics.Name = "contextHistogramStatistics";
            resources.ApplyResources(this.contextHistogramStatistics, "contextHistogramStatistics");
            // 
            // contextHistogramDescriptive
            // 
            this.contextHistogramDescriptive.Name = "contextHistogramDescriptive";
            resources.ApplyResources(this.contextHistogramDescriptive, "contextHistogramDescriptive");
            // 
            // contextHistogramTransform
            // 
            this.contextHistogramTransform.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextHistogramSplit});
            this.contextHistogramTransform.Name = "contextHistogramTransform";
            resources.ApplyResources(this.contextHistogramTransform, "contextHistogramTransform");
            // 
            // contextHistogramSplit
            // 
            this.contextHistogramSplit.Name = "contextHistogramSplit";
            resources.ApplyResources(this.contextHistogramSplit, "contextHistogramSplit");
            this.contextHistogramSplit.Click += new System.EventHandler(this.contextHistogramSplit_Click);
            // 
            // contextHistogramApart
            // 
            this.contextHistogramApart.Name = "contextHistogramApart";
            resources.ApplyResources(this.contextHistogramApart, "contextHistogramApart");
            this.contextHistogramApart.Click += new System.EventHandler(this.contextHistogramApart_Click);
            // 
            // contextHistogramUpdate
            // 
            resources.ApplyResources(this.contextHistogramUpdate, "contextHistogramUpdate");
            this.contextHistogramUpdate.Name = "contextHistogramUpdate";
            this.contextHistogramUpdate.Click += new System.EventHandler(this.contextHistogramUpdate_Click);
            // 
            // contextEvaluation
            // 
            this.contextEvaluation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextEvalProperties,
            this.contextEvalDelete,
            this.toolStripSeparator6,
            this.contextEvalStatistics,
            this.contextEvalApart,
            this.contextEvalUpdate});
            this.contextEvaluation.Name = "contextMenuStripHistogram";
            resources.ApplyResources(this.contextEvaluation, "contextEvaluation");
            // 
            // contextEvalProperties
            // 
            resources.ApplyResources(this.contextEvalProperties, "contextEvalProperties");
            this.contextEvalProperties.Name = "contextEvalProperties";
            // 
            // contextEvalDelete
            // 
            resources.ApplyResources(this.contextEvalDelete, "contextEvalDelete");
            this.contextEvalDelete.Name = "contextEvalDelete";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // contextEvalStatistics
            // 
            this.contextEvalStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextEvalPointDescriptive});
            this.contextEvalStatistics.Name = "contextEvalStatistics";
            resources.ApplyResources(this.contextEvalStatistics, "contextEvalStatistics");
            // 
            // contextEvalPointDescriptive
            // 
            this.contextEvalPointDescriptive.Name = "contextEvalPointDescriptive";
            resources.ApplyResources(this.contextEvalPointDescriptive, "contextEvalPointDescriptive");
            // 
            // contextEvalApart
            // 
            this.contextEvalApart.Name = "contextEvalApart";
            resources.ApplyResources(this.contextEvalApart, "contextEvalApart");
            // 
            // contextEvalUpdate
            // 
            resources.ApplyResources(this.contextEvalUpdate, "contextEvalUpdate");
            this.contextEvalUpdate.Name = "contextEvalUpdate";
            // 
            // contextFunctor
            // 
            this.contextFunctor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextFunctorProperties});
            this.contextFunctor.Name = "contextFunctor";
            resources.ApplyResources(this.contextFunctor, "contextFunctor");
            // 
            // contextFunctorProperties
            // 
            this.contextFunctorProperties.Name = "contextFunctorProperties";
            resources.ApplyResources(this.contextFunctorProperties, "contextFunctorProperties");
            this.contextFunctorProperties.Click += new System.EventHandler(this.contextFunctorProperties_Click);
            this.contextChart.ResumeLayout(false);
            this.contextScatterplot.ResumeLayout(false);
            this.contextHistogram.ResumeLayout(false);
            this.contextEvaluation.ResumeLayout(false);
            this.contextFunctor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextChart;
        private System.Windows.Forms.ToolStripMenuItem contextChartProperties;
        private System.Windows.Forms.ToolStripMenuItem contextChartAxes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextScatterplot;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextHistogram;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem contextChartAdjustAxes;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramCopyValues;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem contextChartSeparate;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotDistinguish;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotDelete;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramDistinguish;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotStatistics;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotDescriptiveX;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotDescriptiveY;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotTrend;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotTransform;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotMerge;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotSplit;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotHistogramX;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramTransform;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramSplit;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramDelete;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramStatistics;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramDescriptive;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotApart;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramApart;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotFindValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotAddTrend;
        private System.Windows.Forms.ToolStripMenuItem contextHistogramAddFit;
        private System.Windows.Forms.ContextMenuStrip contextEvaluation;
        private System.Windows.Forms.ToolStripMenuItem contextEvalProperties;
        private System.Windows.Forms.ToolStripMenuItem contextEvalDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem contextEvalStatistics;
        private System.Windows.Forms.ToolStripMenuItem contextEvalPointDescriptive;
        private System.Windows.Forms.ToolStripMenuItem contextEvalApart;
        private System.Windows.Forms.ToolStripMenuItem contextEvalUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextScatterplotTrendCompare;
        private System.Windows.Forms.ToolStripMenuItem contextChartPrint;
        private System.Windows.Forms.ToolStripMenuItem contextSizeA4;
        private System.Windows.Forms.ToolStripMenuItem contextSizeA5;
        private System.Windows.Forms.ToolStripMenuItem contextSizeA6;
        private System.Windows.Forms.ContextMenuStrip contextFunctor;
        private System.Windows.Forms.ToolStripMenuItem contextFunctorProperties;
        private System.Windows.Forms.ToolStripMenuItem ContextHistogramCopyAll;
        private System.Windows.Forms.ToolStripMenuItem ContextHistogramCopyDatapoints;
    }
}
