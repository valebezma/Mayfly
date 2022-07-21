using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Fish
{
    public class FishCard : Wild.Card
    {
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.PictureBox pictureBoxWarnExposure;
        private System.Windows.Forms.Label labelExposure;
        private Mayfly.Controls.NumberBox numericExposure;
        private Mayfly.Controls.NumberBox numericVelocity;
        private System.Windows.Forms.Label labelVelocity;
        private System.Windows.Forms.Label labelStarted;
        private System.Windows.Forms.Label labelDuration;
        private Mayfly.Controls.NumberBox numericVolume;
        private System.Windows.Forms.Label labelOperation;
        private System.Windows.Forms.Label labelVolume;
        private Mayfly.Controls.NumberBox numericStandards;
        private Mayfly.Controls.NumberBox numericHook;
        private System.Windows.Forms.Label labelEfforts;
        private Mayfly.Controls.NumberBox numericArea;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.PictureBox pictureBoxWarnOpening;
        private System.Windows.Forms.Label labelOpening;
        private Mayfly.Controls.NumberBox numericLength;
        private System.Windows.Forms.Label label12;
        private Mayfly.Controls.NumberBox numericOpening;
        private Mayfly.Controls.NumberBox numericHeight;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelMesh;
        private System.Windows.Forms.Label labelHook;
        private Mayfly.Controls.NumberBox numericSquare;
        private System.Windows.Forms.Label labelSquare;
        private Mayfly.Controls.NumberBox numericMesh;
        private Label labelDepth;
        private IContainer components;
        private TaskDialog taskDialogTrackHandle;
        private TaskDialog taskDialogLocationHandle;
        private TaskDialogs.TaskDialogButton tdbCancelTrack;
        private TaskDialogs.TaskDialogButton tdbSinglepoint;
        private TaskDialogs.TaskDialogButton tdbAsPoly;
        private TaskDialogs.TaskDialogButton tdbExposure;
        private TaskDialogs.TaskDialogButton tdbSinking;
        private TaskDialogs.TaskDialogButton tdbRemoval;
        private Controls.NumberBox numericDepth;
        private Controls.NumberBox numericPortions;
        private Label labelPortions;
        private Label labelEffort;
        private Label labelEnded;

        private bool AllowEffortCalculation { get; set; }
        private bool preciseMode;
        private bool preciseAreaMode {
            get {
                return preciseMode;
            }

            set {
                preciseMode = value;
                dateTimePickerStart.Enabled =
                    dateTimePickerEnd.Enabled =
                    numericExposure.Enabled =
                    !preciseMode;
            }
        }



        public FishCard() : base() {

            InitializeComponent();
            Initiate();
            if (Wild.UserSettings.SelectedDate != null) {
                dateTimePickerStart.Value =
                     dateTimePickerEnd.Value =
                     Wild.UserSettings.SelectedDate;
            }
            isChanged = false;
        }

        public FishCard(string filename) : this() {

            load(filename);
        }



        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FishCard));
            this.numericHook = new Mayfly.Controls.NumberBox();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.pictureBoxWarnExposure = new System.Windows.Forms.PictureBox();
            this.labelExposure = new System.Windows.Forms.Label();
            this.numericExposure = new Mayfly.Controls.NumberBox();
            this.numericVelocity = new Mayfly.Controls.NumberBox();
            this.labelVelocity = new System.Windows.Forms.Label();
            this.labelStarted = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.pictureBoxWarnOpening = new System.Windows.Forms.PictureBox();
            this.numericLength = new Mayfly.Controls.NumberBox();
            this.numericOpening = new Mayfly.Controls.NumberBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelHook = new System.Windows.Forms.Label();
            this.labelSquare = new System.Windows.Forms.Label();
            this.numericMesh = new Mayfly.Controls.NumberBox();
            this.numericSquare = new Mayfly.Controls.NumberBox();
            this.labelMesh = new System.Windows.Forms.Label();
            this.numericHeight = new Mayfly.Controls.NumberBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelOpening = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.numericArea = new Mayfly.Controls.NumberBox();
            this.labelEfforts = new System.Windows.Forms.Label();
            this.numericStandards = new Mayfly.Controls.NumberBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelOperation = new System.Windows.Forms.Label();
            this.numericVolume = new Mayfly.Controls.NumberBox();
            this.labelDepth = new System.Windows.Forms.Label();
            this.taskDialogTrackHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbExposure = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbAsPoly = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSinglepoint = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelTrack = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogLocationHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSinking = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRemoval = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.numericDepth = new Mayfly.Controls.NumberBox();
            this.numericPortions = new Mayfly.Controls.NumberBox();
            this.labelPortions = new System.Windows.Forms.Label();
            this.labelEffort = new System.Windows.Forms.Label();
            this.labelEnded = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.numericDepth);
            this.tabPageCollect.Controls.Add(this.labelDepth);
            this.tabPageCollect.Controls.SetChildIndex(this.labelComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelWater, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waterSelector, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waypointControl1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelTag, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.numericDepth, 0);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            // 
            // labelComments
            // 
            resources.ApplyResources(this.labelComments, "labelComments");
            // 
            // waypointControl1
            // 
            this.waypointControl1.DateTimeFormat = "yyyy-MM-dd (ddd) HH:mm";
            resources.ApplyResources(this.waypointControl1, "waypointControl1");
            this.waypointControl1.Changed += new System.EventHandler(this.waypointControl1_Changed);
            this.waypointControl1.LocationImported += new Mayfly.Geographics.LocationDataEventHandler(this.waypointControl1_LocationImported);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.numericPortions);
            this.tabPageSampler.Controls.Add(this.labelPortions);
            this.tabPageSampler.Controls.Add(this.dateTimePickerEnd);
            this.tabPageSampler.Controls.Add(this.numericVolume);
            this.tabPageSampler.Controls.Add(this.dateTimePickerStart);
            this.tabPageSampler.Controls.Add(this.pictureBoxWarnExposure);
            this.tabPageSampler.Controls.Add(this.labelEffort);
            this.tabPageSampler.Controls.Add(this.labelOperation);
            this.tabPageSampler.Controls.Add(this.labelExposure);
            this.tabPageSampler.Controls.Add(this.numericExposure);
            this.tabPageSampler.Controls.Add(this.labelVolume);
            this.tabPageSampler.Controls.Add(this.numericVelocity);
            this.tabPageSampler.Controls.Add(this.labelEnded);
            this.tabPageSampler.Controls.Add(this.labelStarted);
            this.tabPageSampler.Controls.Add(this.labelDuration);
            this.tabPageSampler.Controls.Add(this.labelVelocity);
            this.tabPageSampler.Controls.Add(this.numericStandards);
            this.tabPageSampler.Controls.Add(this.numericHook);
            this.tabPageSampler.Controls.Add(this.labelEfforts);
            this.tabPageSampler.Controls.Add(this.numericArea);
            this.tabPageSampler.Controls.Add(this.labelArea);
            this.tabPageSampler.Controls.Add(this.labelLength);
            this.tabPageSampler.Controls.Add(this.pictureBoxWarnOpening);
            this.tabPageSampler.Controls.Add(this.labelOpening);
            this.tabPageSampler.Controls.Add(this.numericLength);
            this.tabPageSampler.Controls.Add(this.label12);
            this.tabPageSampler.Controls.Add(this.numericOpening);
            this.tabPageSampler.Controls.Add(this.numericHeight);
            this.tabPageSampler.Controls.Add(this.labelHeight);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.labelHook);
            this.tabPageSampler.Controls.Add(this.numericSquare);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.numericMesh);
            this.tabPageSampler.TextChanged += new System.EventHandler(this.virtue_Changed);
            this.tabPageSampler.Controls.SetChildIndex(this.numericMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelHook, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelHeight, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericHeight, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label12, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericLength, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.pictureBoxWarnOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelLength, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelEfforts, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericHook, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericStandards, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelVelocity, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelDuration, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelStarted, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelEnded, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericVelocity, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelVolume, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelOperation, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelEffort, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.pictureBoxWarnExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.dateTimePickerStart, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericVolume, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.dateTimePickerEnd, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelPortions, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericPortions, 0);
            // 
            // labelSampler
            // 
            resources.ApplyResources(this.labelSampler, "labelSampler");
            // 
            // labelPosition
            // 
            resources.ApplyResources(this.labelPosition, "labelPosition");
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            // 
            // comboBoxSampler
            // 
            resources.ApplyResources(this.comboBoxSampler, "comboBoxSampler");
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.comboBoxSampler_SelectedIndexChanged);
            // 
            // buttonEquipment
            // 
            resources.ApplyResources(this.buttonEquipment, "buttonEquipment");
            // 
            // Logger
            // 
            this.Logger.IndividualsRequired += new System.EventHandler(this.logger_IndividualsRequired);
            // 
            // numericHook
            // 
            resources.ApplyResources(this.numericHook, "numericHook");
            this.numericHook.Maximum = 100D;
            this.numericHook.Minimum = 0D;
            this.numericHook.Name = "numericHook";
            this.numericHook.Value = -1D;
            this.numericHook.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericHook.TextChanged += new System.EventHandler(this.numericValue_TextChanged);
            // 
            // dateTimePickerEnd
            // 
            resources.ApplyResources(this.dateTimePickerEnd, "dateTimePickerEnd");
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            // 
            // dateTimePickerStart
            // 
            resources.ApplyResources(this.dateTimePickerStart, "dateTimePickerStart");
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            this.dateTimePickerStart.EnabledChanged += new System.EventHandler(this.dateTimePickerStart_EnabledChanged);
            // 
            // pictureBoxWarnExposure
            // 
            resources.ApplyResources(this.pictureBoxWarnExposure, "pictureBoxWarnExposure");
            this.pictureBoxWarnExposure.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarnExposure.Name = "pictureBoxWarnExposure";
            this.pictureBoxWarnExposure.TabStop = false;
            this.pictureBoxWarnExposure.DoubleClick += new System.EventHandler(this.pictureBoxWarnExposure_DoubleClick);
            this.pictureBoxWarnExposure.MouseLeave += new System.EventHandler(this.pictureBoxWarnExposure_MouseLeave);
            this.pictureBoxWarnExposure.MouseHover += new System.EventHandler(this.pictureBoxWarnExposure_MouseHover);
            // 
            // labelExposure
            // 
            resources.ApplyResources(this.labelExposure, "labelExposure");
            this.labelExposure.Name = "labelExposure";
            // 
            // numericExposure
            // 
            resources.ApplyResources(this.numericExposure, "numericExposure");
            this.numericExposure.Maximum = 100D;
            this.numericExposure.Minimum = 0D;
            this.numericExposure.Name = "numericExposure";
            this.numericExposure.Value = -1D;
            this.numericExposure.ValueChanged += new System.EventHandler(this.effort_Changed);
            this.numericExposure.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            // 
            // numericVelocity
            // 
            resources.ApplyResources(this.numericVelocity, "numericVelocity");
            this.numericVelocity.Maximum = 100D;
            this.numericVelocity.Minimum = 0D;
            this.numericVelocity.Name = "numericVelocity";
            this.numericVelocity.Value = -1D;
            this.numericVelocity.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericVelocity.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelVelocity
            // 
            resources.ApplyResources(this.labelVelocity, "labelVelocity");
            this.labelVelocity.Name = "labelVelocity";
            // 
            // labelStarted
            // 
            resources.ApplyResources(this.labelStarted, "labelStarted");
            this.labelStarted.Name = "labelStarted";
            // 
            // labelDuration
            // 
            resources.ApplyResources(this.labelDuration, "labelDuration");
            this.labelDuration.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelDuration.Name = "labelDuration";
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // pictureBoxWarnOpening
            // 
            resources.ApplyResources(this.pictureBoxWarnOpening, "pictureBoxWarnOpening");
            this.pictureBoxWarnOpening.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarnOpening.Name = "pictureBoxWarnOpening";
            this.pictureBoxWarnOpening.TabStop = false;
            this.pictureBoxWarnOpening.DoubleClick += new System.EventHandler(this.pictureBoxWarnOpening_DoubleClick);
            this.pictureBoxWarnOpening.MouseLeave += new System.EventHandler(this.pictureBoxWarnOpening_MouseLeave);
            this.pictureBoxWarnOpening.MouseHover += new System.EventHandler(this.pictureBoxWarnOpening_MouseHover);
            // 
            // numericLength
            // 
            resources.ApplyResources(this.numericLength, "numericLength");
            this.numericLength.Maximum = 100D;
            this.numericLength.Minimum = 0D;
            this.numericLength.Name = "numericLength";
            this.numericLength.Value = -1D;
            this.numericLength.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericLength.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // numericOpening
            // 
            resources.ApplyResources(this.numericOpening, "numericOpening");
            this.numericOpening.Maximum = 100D;
            this.numericOpening.Minimum = 0D;
            this.numericOpening.Name = "numericOpening";
            this.numericOpening.Value = -1D;
            this.numericOpening.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericOpening.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelHeight
            // 
            resources.ApplyResources(this.labelHeight, "labelHeight");
            this.labelHeight.Name = "labelHeight";
            // 
            // labelHook
            // 
            resources.ApplyResources(this.labelHook, "labelHook");
            this.labelHook.Name = "labelHook";
            // 
            // labelSquare
            // 
            resources.ApplyResources(this.labelSquare, "labelSquare");
            this.labelSquare.Name = "labelSquare";
            // 
            // numericMesh
            // 
            resources.ApplyResources(this.numericMesh, "numericMesh");
            this.numericMesh.Maximum = 100D;
            this.numericMesh.Minimum = 0D;
            this.numericMesh.Name = "numericMesh";
            this.numericMesh.Value = -1D;
            this.numericMesh.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericMesh.TextChanged += new System.EventHandler(this.numericValue_TextChanged);
            // 
            // numericSquare
            // 
            resources.ApplyResources(this.numericSquare, "numericSquare");
            this.numericSquare.Maximum = 100D;
            this.numericSquare.Minimum = 0D;
            this.numericSquare.Name = "numericSquare";
            this.numericSquare.Value = -1D;
            this.numericSquare.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericSquare.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelMesh
            // 
            resources.ApplyResources(this.labelMesh, "labelMesh");
            this.labelMesh.Name = "labelMesh";
            // 
            // numericHeight
            // 
            resources.ApplyResources(this.numericHeight, "numericHeight");
            this.numericHeight.Maximum = 100D;
            this.numericHeight.Minimum = 0D;
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Value = -1D;
            this.numericHeight.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.numericHeight.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // labelOpening
            // 
            resources.ApplyResources(this.labelOpening, "labelOpening");
            this.labelOpening.Name = "labelOpening";
            // 
            // labelArea
            // 
            resources.ApplyResources(this.labelArea, "labelArea");
            this.labelArea.Name = "labelArea";
            // 
            // numericArea
            // 
            resources.ApplyResources(this.numericArea, "numericArea");
            this.numericArea.Maximum = 100000D;
            this.numericArea.Minimum = 0D;
            this.numericArea.Name = "numericArea";
            this.numericArea.ReadOnly = true;
            this.numericArea.Value = -1D;
            this.numericArea.TextChanged += new System.EventHandler(this.numericValue_TextChanged);
            // 
            // labelEfforts
            // 
            resources.ApplyResources(this.labelEfforts, "labelEfforts");
            this.labelEfforts.Name = "labelEfforts";
            // 
            // numericStandards
            // 
            resources.ApplyResources(this.numericStandards, "numericStandards");
            this.numericStandards.Maximum = 100000D;
            this.numericStandards.Minimum = 0D;
            this.numericStandards.Name = "numericStandards";
            this.numericStandards.ReadOnly = true;
            this.numericStandards.Value = -1D;
            this.numericStandards.TextChanged += new System.EventHandler(this.numericValue_TextChanged);
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // labelOperation
            // 
            resources.ApplyResources(this.labelOperation, "labelOperation");
            this.labelOperation.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelOperation.Name = "labelOperation";
            // 
            // numericVolume
            // 
            resources.ApplyResources(this.numericVolume, "numericVolume");
            this.numericVolume.Maximum = 100000D;
            this.numericVolume.Minimum = 0D;
            this.numericVolume.Name = "numericVolume";
            this.numericVolume.ReadOnly = true;
            this.numericVolume.Value = -1D;
            this.numericVolume.TextChanged += new System.EventHandler(this.numericValue_TextChanged);
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // taskDialogTrackHandle
            // 
            this.taskDialogTrackHandle.Buttons.Add(this.tdbExposure);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbAsPoly);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbSinglepoint);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbCancelTrack);
            resources.ApplyResources(this.taskDialogTrackHandle, "taskDialogTrackHandle");
            // 
            // tdbExposure
            // 
            resources.ApplyResources(this.tdbExposure, "tdbExposure");
            // 
            // tdbAsPoly
            // 
            resources.ApplyResources(this.tdbAsPoly, "tdbAsPoly");
            // 
            // tdbSinglepoint
            // 
            resources.ApplyResources(this.tdbSinglepoint, "tdbSinglepoint");
            // 
            // tdbCancelTrack
            // 
            this.tdbCancelTrack.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // taskDialogLocationHandle
            // 
            this.taskDialogLocationHandle.Buttons.Add(this.tdbSinking);
            this.taskDialogLocationHandle.Buttons.Add(this.tdbRemoval);
            resources.ApplyResources(this.taskDialogLocationHandle, "taskDialogLocationHandle");
            // 
            // tdbSinking
            // 
            this.tdbSinking.Default = true;
            resources.ApplyResources(this.tdbSinking, "tdbSinking");
            // 
            // tdbRemoval
            // 
            resources.ApplyResources(this.tdbRemoval, "tdbRemoval");
            // 
            // numericDepth
            // 
            resources.ApplyResources(this.numericDepth, "numericDepth");
            this.numericDepth.Maximum = 100D;
            this.numericDepth.Minimum = 0D;
            this.numericDepth.Name = "numericDepth";
            this.numericDepth.Value = -1D;
            // 
            // numericPortions
            // 
            resources.ApplyResources(this.numericPortions, "numericPortions");
            this.numericPortions.Maximum = 100D;
            this.numericPortions.Minimum = 0D;
            this.numericPortions.Name = "numericPortions";
            this.numericPortions.Value = -1D;
            this.numericPortions.ValueChanged += new System.EventHandler(this.effort_Changed);
            this.numericPortions.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            // 
            // labelPortions
            // 
            resources.ApplyResources(this.labelPortions, "labelPortions");
            this.labelPortions.Name = "labelPortions";
            // 
            // labelEffort
            // 
            resources.ApplyResources(this.labelEffort, "labelEffort");
            this.labelEffort.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelEffort.Name = "labelEffort";
            // 
            // labelEnded
            // 
            resources.ApplyResources(this.labelEnded, "labelEnded");
            this.labelEnded.Name = "labelEnded";
            // 
            // FishCard
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "FishCard";
            this.OnSaved += new System.EventHandler(this.fishCard_OnSaved);
            this.OnCleared += new System.EventHandler(this.fishCard_OnCleared);
            this.OnEquipmentSaved += new Mayfly.Wild.EquipmentEventHandler(this.fishCard_OnEquipmentSaved);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void clearGear() {
            numericLength.Clear();
            numericOpening.Clear();
            numericHeight.Clear();
            numericSquare.Clear();
            numericMesh.Clear();
            numericHook.Clear();
        }

        private void clearEffort() {

            dateTimePickerStart.Value = waypointControl1.Waypoint.TimeMark.AddHours(-12.0);
            numericVelocity.Clear();
            numericExposure.Clear();
            numericDepth.Clear();
        }

        private void saveEffort() {

            if (numericDepth.IsSet) {
                data.Solitary.Depth = numericDepth.Value;
            } else {
                data.Solitary.SetDepthNull();
            }

            if (preciseAreaMode) {

                if (numericArea.IsSet)
                    data.Solitary.Effort = -10000 * numericArea.Value;

            } else {

                if (dateTimePickerEnd.Enabled) {
                    data.Solitary.Effort = (int)(waypointControl1.Waypoint.TimeMark - dateTimePickerStart.Value).TotalMinutes;
                } else if (numericExposure.Enabled && numericExposure.IsSet) {
                    data.Solitary.Effort = numericExposure.Value;
                } else if (numericPortions.Enabled && numericPortions.IsSet) {
                    data.Solitary.Effort = numericPortions.Value;
                } else {
                    data.Solitary.SetEffortNull();
                }
            }
        }

        private void setEndpoint(Waypoint waypoint) {

            waypointControl1.Waypoint.Latitude = waypoint.Latitude;
            waypointControl1.Waypoint.Longitude = waypoint.Longitude;

            if (!waypoint.IsTimeMarkNull) {
                if (data.Solitary.SamplerRow.IsPassive() && taskDialogLocationHandle.ShowDialog(this) == tdbSinking) {
                    dateTimePickerStart.Value = waypoint.TimeMark;
                } else {
                    waypointControl1.Waypoint.TimeMark = waypoint.TimeMark;
                    dateTimePickerEnd.Value = waypoint.TimeMark;
                }
            }

            waypointControl1.UpdateValues();

            if (waypoint.IsNameNull) { } else {
                textBoxLabel.Text = waypoint.Name;
            }
        }



        private void fishCard_OnCleared(object sender, EventArgs e) {

            clearGear();
            clearEffort();
        }

        private void fishCard_OnEquipmentSaved(object sender, EquipmentEventArgs e) {

            effort_Changed(sender, e);
        }

        private void fishCard_OnSaved(object sender, EventArgs e) {

        }

        private void waypointControl1_Changed(object sender, EventArgs e) {
            if (waypointControl1.ContainsFocus) {
                dateTimePickerEnd.Value = waypointControl1.Waypoint.TimeMark;
                isChanged = true;
            }
        }

        private void waypointControl1_LocationImported(object sender, LocationDataEventArgs e) {
            if (!SelectedSampler.HasVirtue("Exposure")) {
                ListLocation locationSelection = new ListLocation(e.Filenames);
                locationSelection.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                locationSelection.LocationSelected += new LocationEventHandler(locationData_Selected);
                locationSelection.ShowDialog(this);
            } else {
                ListWaypoints waypoints = new ListWaypoints(e.Filenames);

                if (waypoints.Count == 0) { } else if (waypoints.Count == 1) {
                    preciseAreaMode = false;
                    setEndpoint(waypoints.Value);
                } else {
                    waypoints.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                    waypoints.WaypointSelected += new LocationEventHandler(locationData_Selected);
                    waypoints.Show(this);
                }
            }
        }

        private void locationData_Selected(object sender, LocationEventArgs e) {

            if (e.LocationObject is Waypoint waypoint) {
                preciseAreaMode = false;
                setEndpoint(waypoint);
            } else if (e.LocationObject is Polygon poly) {
                preciseAreaMode = true;
                numericArea.Value = poly.Area / 10000d;
                setEndpoint(poly.Points.Last());

                //HandlePolygon((Polygon)e.LocationObject);
            } else if (e.LocationObject is Track) {
                tdbAsPoly.Enabled = (EffortType)SelectedSampler.EffortType == EffortType.Exposure;

                TaskDialogButton tdb = taskDialogTrackHandle.ShowDialog();

                preciseAreaMode = tdb == tdbAsPoly;

                //Track track = (Track)e.LocationObject;
                Track[] tracks = (Track[])e.LocationObjects;
                Waypoint[] wpts = Track.GetWaypoints(tracks);

                if (tdb == tdbSinglepoint) {
                    ListWaypoints waypoints = new ListWaypoints(wpts);
                    waypoints.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                    waypoints.WaypointSelected += new LocationEventHandler(locationData_Selected);
                    waypoints.Show(this);
                    //SetEndpoint(track.Points.Last());
                } else if (tdb == tdbExposure) {
                    // Just insert exposure value
                    setEndpoint(wpts.Last());

                    numericExposure.Value = Track.TotalLength(tracks);
                    if ((EffortType)SelectedSampler.EffortType == EffortType.Exposition) dateTimePickerStart.Value = tracks[0].Points[0].TimeMark;
                    if (SelectedSampler.HasVirtue("Velocity")) numericVelocity.Value = Track.AverageKmph(tracks);
                } else if (tdb == tdbAsPoly) {
                    // Behave like polygon

                    double s = 0;
                    foreach (Track track in tracks) {
                        Polygon polygon = new Polygon(track);
                        s += polygon.Area;
                    }

                    preciseAreaMode = true;
                    numericArea.Value = s / 10000d;
                    setEndpoint(wpts.Last());

                    //HandlePolygon(new Polygon(track));
                }
            }

            tabControl.SelectedTab = tabPageCollect;
        }


        private void logger_IndividualsRequired(object sender, EventArgs e) {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnDefinition.Name].Value != null) {

                    Wild.Survey.LogRow logRow = Logger.SaveLogRow(gridRow);
                    Individuals individuals = null;

                    foreach (Form form in Application.OpenForms) {
                        if (!(form is Individuals)) continue;
                        if (((Individuals)form).LogRow == logRow) {
                            individuals = (Individuals)form;
                        }
                    }

                    if (individuals == null) {
                        individuals = new Individuals(logRow) {
                            //individuals.SetColumns(ColumnSpecies, ColumnQuantity, ColumnMass);
                            LogLine = gridRow
                        };
                        individuals.SetFriendlyDesktopLocation(spreadSheetLog);
                        individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                        individuals.Show(this);
                    } else {
                        individuals.BringToFront();
                    }
                }
            }
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e) {
            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK) {
                isChanged |= individuals.ChangesWereMade;
            }
        }


        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e) {

            dateTimePickerStart.Value = dateTimePickerStart.Value.AddSeconds(
                -dateTimePickerStart.Value.Second);

            //dateTimePickerEnd.MinDate = dateTimePickerStart.Value;

            effort_Changed(sender, e);
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e) {

            dateTimePickerEnd.Value = dateTimePickerEnd.Value.AddSeconds(
                -dateTimePickerEnd.Value.Second);

            if (dateTimePickerEnd.ContainsFocus) {
                waypointControl1.SetDateTime(dateTimePickerEnd.Value);
                waypointControl1.Save();
            }

            //dateTimePickerStart.MaxDate = dateTimePickerEnd.Value;

            effort_Changed(sender, e);
        }

        private void dateTimePickerStart_EnabledChanged(object sender, EventArgs e) {
            labelDuration.Visible = dateTimePickerStart.Enabled;
        }

        private void comboBoxSampler_SelectedIndexChanged(object sender, EventArgs e) {

            if (SelectedSampler == null) return;

            labelStarted.Enabled = labelEnded.Enabled = dateTimePickerStart.Enabled = dateTimePickerEnd.Enabled =
                !SelectedSampler.IsEffortTypeNull() && SelectedSampler.EffortType == (int)EffortType.Exposition;
        }

        private void virtue_Changed(object sender, EventArgs e) {

            if (sender is NumberBox nb && !nb.ContainsFocus) return;

            if (SelectedSampler == null) return;

            saveSampler();
        }

        private void samplerValue_EnabledChanged(object sender, EventArgs e) {
            if (!((NumberBox)sender).Enabled) {
                ((NumberBox)sender).Clear();
            }
        }

        private void effort_Changed(object sender, EventArgs e) {

            saveEffort();

            numericArea.Value = data.Solitary.GetEffort(EffortExpression.Area);
            numericVolume.Value = data.Solitary.GetEffort(EffortExpression.Volume);
            numericStandards.Value = data.Solitary.GetEffort(EffortExpression.Standards);

            numericArea.Format = 
            numericVolume.Format = 
            numericStandards.Format = "N3";

            labelDuration.ResetFormatted(Math.Floor(data.Solitary.Duration.TotalHours),
                data.Solitary.Duration.Minutes, data.Solitary.Duration.TotalHours);

            isChanged = true;
        }


        private void pictureBoxWarnOpening_MouseHover(object sender, EventArgs e) {
            if (numericOpening.IsSet) {
                numericOpening.NotifyInstantly(Resources.Interface.Messages.FixOpening,
                    numericOpening.Value, numericLength.Value);
            } else {
                numericOpening.NotifyInstantly(Resources.Interface.Messages.FixEmptyOpening);
            }
        }

        private void pictureBoxWarnOpening_MouseLeave(object sender, EventArgs e) {
            //toolTipAttention.Hide(numericOpening);
        }

        private void pictureBoxWarnOpening_DoubleClick(object sender, EventArgs e) {
            numericOpening.Value = numericLength.Value * Service.DefaultOpening(SelectedSampler.ID);
        }


        private void pictureBoxWarnExposure_MouseHover(object sender, EventArgs e) {
            numericExposure.NotifyInstantly(Resources.Interface.Messages.FixSpread,
                numericExposure.Value, numericLength.Value);
        }

        private void pictureBoxWarnExposure_MouseLeave(object sender, EventArgs e) {
            //toolTipAttention.Hide(numericExposure);
        }

        private void pictureBoxWarnExposure_DoubleClick(object sender, EventArgs e) {
            numericExposure.Value = Math.Ceiling(2 * numericLength.Value / Math.PI);
        }

        private void numericValue_TextChanged(object sender, EventArgs e) {
            value_Changed(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {

        }
    }
}
