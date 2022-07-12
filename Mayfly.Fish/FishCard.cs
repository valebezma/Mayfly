using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Mayfly.Fish.UserSettings;
using static Mayfly.Wild.UserSettings;
using static Mayfly.UserSettings;

namespace Mayfly.Fish
{
    public class FishCard : Wild.Card
    {
        private System.Windows.Forms.Panel panelLS;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.PictureBox pictureBoxWarnExposure;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.TextBox textBoxExposure;
        private System.Windows.Forms.TextBox textBoxVelocity;
        private System.Windows.Forms.Label labelVelocity;
        private System.Windows.Forms.Label labelOperationEnd;
        private System.Windows.Forms.Label labelOperation;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.TextBox textBoxEfforts;
        private System.Windows.Forms.TextBox textBoxHook;
        private System.Windows.Forms.Label labelEfforts;
        private System.Windows.Forms.Panel panelGeoData;
        private System.Windows.Forms.TextBox textBoxExactArea;
        private System.Windows.Forms.Label labelExactArea;
        private System.Windows.Forms.TextBox textBoxArea;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.PictureBox pictureBoxWarnOpening;
        private System.Windows.Forms.Label labelOpening;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxOpening;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelMesh;
        private System.Windows.Forms.Label labelHook;
        private System.Windows.Forms.TextBox textBoxSquare;
        private System.Windows.Forms.Label labelSquare;
        private System.Windows.Forms.TextBox textBoxMesh;
        private TextBox textBoxDepth;
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

        private bool AllowEffortCalculation { get; set; }
        private bool preciseMode;
        private bool PreciseAreaMode {
            get {
                return preciseMode;
            }

            set {
                preciseMode = value;

                panelLS.Visible = !preciseMode;
                panelGeoData.Visible = preciseMode;

                //if (value)
                //{
                //    panelH.Top = 257+26;
                //}
                //else
                //{
                //    panelH.Top = 336;
                //}

            }
        }



        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FishCard));
            this.textBoxHook = new System.Windows.Forms.TextBox();
            this.textBoxExactArea = new System.Windows.Forms.TextBox();
            this.labelExactArea = new System.Windows.Forms.Label();
            this.panelGeoData = new System.Windows.Forms.Panel();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.pictureBoxWarnExposure = new System.Windows.Forms.PictureBox();
            this.labelExposure = new System.Windows.Forms.Label();
            this.textBoxExposure = new System.Windows.Forms.TextBox();
            this.textBoxVelocity = new System.Windows.Forms.TextBox();
            this.labelVelocity = new System.Windows.Forms.Label();
            this.labelOperationEnd = new System.Windows.Forms.Label();
            this.labelOperation = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.panelLS = new System.Windows.Forms.Panel();
            this.labelLength = new System.Windows.Forms.Label();
            this.pictureBoxWarnOpening = new System.Windows.Forms.PictureBox();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.textBoxOpening = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelHook = new System.Windows.Forms.Label();
            this.labelSquare = new System.Windows.Forms.Label();
            this.textBoxMesh = new System.Windows.Forms.TextBox();
            this.textBoxSquare = new System.Windows.Forms.TextBox();
            this.labelMesh = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelOpening = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.textBoxArea = new System.Windows.Forms.TextBox();
            this.labelEfforts = new System.Windows.Forms.Label();
            this.textBoxEfforts = new System.Windows.Forms.TextBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.labelDepth = new System.Windows.Forms.Label();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
            this.taskDialogTrackHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbExposure = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbAsPoly = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSinglepoint = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelTrack = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogLocationHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSinking = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRemoval = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            this.panelGeoData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).BeginInit();
            this.panelLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.textBoxDepth);
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
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            // 
            // waypointControl1
            // 
            this.waypointControl1.Changed += new System.EventHandler(this.waypointControl1_Changed);
            this.waypointControl1.LocationImported += new Mayfly.Geographics.LocationDataEventHandler(this.waypointControl1_LocationImported);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.textBoxVolume);
            this.tabPageSampler.Controls.Add(this.label17);
            this.tabPageSampler.Controls.Add(this.labelVolume);
            this.tabPageSampler.Controls.Add(this.textBoxEfforts);
            this.tabPageSampler.Controls.Add(this.textBoxHook);
            this.tabPageSampler.Controls.Add(this.labelEfforts);
            this.tabPageSampler.Controls.Add(this.panelGeoData);
            this.tabPageSampler.Controls.Add(this.textBoxArea);
            this.tabPageSampler.Controls.Add(this.labelArea);
            this.tabPageSampler.Controls.Add(this.labelLength);
            this.tabPageSampler.Controls.Add(this.pictureBoxWarnOpening);
            this.tabPageSampler.Controls.Add(this.labelOpening);
            this.tabPageSampler.Controls.Add(this.textBoxLength);
            this.tabPageSampler.Controls.Add(this.label12);
            this.tabPageSampler.Controls.Add(this.textBoxOpening);
            this.tabPageSampler.Controls.Add(this.textBoxHeight);
            this.tabPageSampler.Controls.Add(this.labelHeight);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.labelHook);
            this.tabPageSampler.Controls.Add(this.textBoxSquare);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.textBoxMesh);
            this.tabPageSampler.Controls.Add(this.panelLS);
            this.tabPageSampler.TextChanged += new System.EventHandler(this.sampler_Changed);
            this.tabPageSampler.Controls.SetChildIndex(this.panelLS, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelHook, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelHeight, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxHeight, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label12, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxLength, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.pictureBoxWarnOpening, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelLength, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.panelGeoData, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelEfforts, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxHook, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxEfforts, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelVolume, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label17, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxVolume, 0);
            // 
            // labelSampler
            // 
            resources.ApplyResources(this.labelSampler, "labelSampler");
            // 
            // comboBoxSampler
            // 
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // Logger
            // 
            this.Logger.IndividualsRequired += new System.EventHandler(this.Logger_IndividualsRequired);
            // 
            // textBoxHook
            // 
            resources.ApplyResources(this.textBoxHook, "textBoxHook");
            this.textBoxHook.Name = "textBoxHook";
            this.textBoxHook.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxHook.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // textBoxExactArea
            // 
            resources.ApplyResources(this.textBoxExactArea, "textBoxExactArea");
            this.textBoxExactArea.Name = "textBoxExactArea";
            this.textBoxExactArea.ReadOnly = true;
            this.textBoxExactArea.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            // 
            // labelExactArea
            // 
            resources.ApplyResources(this.labelExactArea, "labelExactArea");
            this.labelExactArea.Name = "labelExactArea";
            // 
            // panelGeoData
            // 
            resources.ApplyResources(this.panelGeoData, "panelGeoData");
            this.panelGeoData.Controls.Add(this.textBoxExactArea);
            this.panelGeoData.Controls.Add(this.labelExactArea);
            this.panelGeoData.Name = "panelGeoData";
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
            // textBoxExposure
            // 
            resources.ApplyResources(this.textBoxExposure, "textBoxExposure");
            this.textBoxExposure.Name = "textBoxExposure";
            this.textBoxExposure.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxExposure.TextChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // textBoxVelocity
            // 
            resources.ApplyResources(this.textBoxVelocity, "textBoxVelocity");
            this.textBoxVelocity.Name = "textBoxVelocity";
            this.textBoxVelocity.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxVelocity.TextChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // labelVelocity
            // 
            resources.ApplyResources(this.labelVelocity, "labelVelocity");
            this.labelVelocity.Name = "labelVelocity";
            // 
            // labelOperationEnd
            // 
            resources.ApplyResources(this.labelOperationEnd, "labelOperationEnd");
            this.labelOperationEnd.Name = "labelOperationEnd";
            // 
            // labelOperation
            // 
            resources.ApplyResources(this.labelOperation, "labelOperation");
            this.labelOperation.Name = "labelOperation";
            // 
            // labelDuration
            // 
            resources.ApplyResources(this.labelDuration, "labelDuration");
            this.labelDuration.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelDuration.Name = "labelDuration";
            // 
            // panelLS
            // 
            resources.ApplyResources(this.panelLS, "panelLS");
            this.panelLS.Controls.Add(this.dateTimePickerEnd);
            this.panelLS.Controls.Add(this.dateTimePickerStart);
            this.panelLS.Controls.Add(this.pictureBoxWarnExposure);
            this.panelLS.Controls.Add(this.labelExposure);
            this.panelLS.Controls.Add(this.textBoxExposure);
            this.panelLS.Controls.Add(this.textBoxVelocity);
            this.panelLS.Controls.Add(this.labelVelocity);
            this.panelLS.Controls.Add(this.labelOperationEnd);
            this.panelLS.Controls.Add(this.labelOperation);
            this.panelLS.Controls.Add(this.labelDuration);
            this.panelLS.Name = "panelLS";
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
            // textBoxLength
            // 
            resources.ApplyResources(this.textBoxLength, "textBoxLength");
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxLength.TextChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // textBoxOpening
            // 
            resources.ApplyResources(this.textBoxOpening, "textBoxOpening");
            this.textBoxOpening.Name = "textBoxOpening";
            this.textBoxOpening.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxOpening.TextChanged += new System.EventHandler(this.sampler_Changed);
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
            // textBoxMesh
            // 
            resources.ApplyResources(this.textBoxMesh, "textBoxMesh");
            this.textBoxMesh.Name = "textBoxMesh";
            this.textBoxMesh.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxMesh.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // textBoxSquare
            // 
            resources.ApplyResources(this.textBoxSquare, "textBoxSquare");
            this.textBoxSquare.Name = "textBoxSquare";
            this.textBoxSquare.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxSquare.TextChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // labelMesh
            // 
            resources.ApplyResources(this.labelMesh, "labelMesh");
            this.labelMesh.Name = "labelMesh";
            // 
            // textBoxHeight
            // 
            resources.ApplyResources(this.textBoxHeight, "textBoxHeight");
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxHeight.TextChanged += new System.EventHandler(this.sampler_Changed);
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
            // textBoxArea
            // 
            resources.ApplyResources(this.textBoxArea, "textBoxArea");
            this.textBoxArea.Name = "textBoxArea";
            this.textBoxArea.ReadOnly = true;
            this.textBoxArea.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // labelEfforts
            // 
            resources.ApplyResources(this.labelEfforts, "labelEfforts");
            this.labelEfforts.Name = "labelEfforts";
            // 
            // textBoxEfforts
            // 
            resources.ApplyResources(this.textBoxEfforts, "textBoxEfforts");
            this.textBoxEfforts.Name = "textBoxEfforts";
            this.textBoxEfforts.ReadOnly = true;
            this.textBoxEfforts.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label17.Name = "label17";
            // 
            // textBoxVolume
            // 
            resources.ApplyResources(this.textBoxVolume, "textBoxVolume");
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.ReadOnly = true;
            this.textBoxVolume.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // textBoxDepth
            // 
            resources.ApplyResources(this.textBoxDepth, "textBoxDepth");
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxDepth.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxDepth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
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
            // FishCard
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "FishCard";
            this.OnSaved += new System.EventHandler(this.fishCard_OnSaved);
            this.OnCleared += new System.EventHandler(this.fishCard_OnCleared);
            this.OnEquipmentSelected += new Mayfly.Wild.EquipmentEventHandler(this.fishCard_OnEquipmentSelected);
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
            this.panelGeoData.ResumeLayout(false);
            this.panelGeoData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).EndInit();
            this.panelLS.ResumeLayout(false);
            this.panelLS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void clearGear()
        {
            textBoxLength.Text = string.Empty;
            textBoxOpening.Text = string.Empty;
            textBoxHeight.Text = string.Empty;
            textBoxSquare.Text = string.Empty;
            textBoxMesh.Text = string.Empty;
            textBoxHook.Text = string.Empty;
        }

        private void clearEffort()
        {
            dateTimePickerStart.Value = waypointControl1.Waypoint.TimeMark.AddHours(-12.0);
            textBoxVelocity.Text = string.Empty;
            textBoxExposure.Text = string.Empty;
            textBoxDepth.Text = string.Empty;
        }

        private void saveSamplerValues() { 

            if (SelectedSampler == null) {
                data.Solitary.SetSamplerNull();
                ReaderSettings.SelectedSampler = null;
            } else {
                data.Solitary.Sampler = SelectedSampler.ID;
                ReaderSettings.SelectedSampler = SelectedSampler;

                if (textBoxMesh.Enabled && textBoxMesh.Text.IsDoubleConvertible()) {
                    data.Solitary.Mesh = (int)double.Parse(textBoxMesh.Text);
                } else {
                    data.Solitary.SetMeshNull();
                }
            }

            if (PreciseAreaMode) {
                if (textBoxExactArea.Text.IsDoubleConvertible())
                    data.Solitary.ExactArea = 10000d * double.Parse(textBoxExactArea.Text);
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("H") && textBoxHeight.Text.IsDoubleConvertible()) {
                data.Solitary.Height = double.Parse(textBoxHeight.Text);
            } else {
                data.Solitary.SetHeightNull();
            }

            if (textBoxDepth.Text.IsDoubleConvertible()) {
                data.Solitary.Depth = double.Parse(textBoxDepth.Text);
            } else {
                data.Solitary.SetDepthNull();
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("M") && textBoxMesh.Text.IsDoubleConvertible()) {
                data.Solitary.Mesh = int.Parse(textBoxMesh.Text);
            } else {
                data.Solitary.SetMeshNull();
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("J") && textBoxHook.Text.IsDoubleConvertible()) {
                data.Solitary.Hook = (int)double.Parse(textBoxHook.Text);
            } else {
                data.Solitary.SetHookNull();
            }

            if (data.Solitary.IsExactAreaNull()) {
                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("L") && textBoxLength.Text.IsDoubleConvertible()) {
                    data.Solitary.Length = double.Parse(textBoxLength.Text);
                } else {
                    data.Solitary.SetLengthNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("O") && textBoxOpening.Text.IsDoubleConvertible()) {
                    data.Solitary.Opening = double.Parse(textBoxOpening.Text);
                } else {
                    data.Solitary.SetOpeningNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("S") && textBoxSquare.Text.IsDoubleConvertible()) {
                    data.Solitary.Square = double.Parse(textBoxSquare.Text);
                } else {
                    data.Solitary.SetSquareNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("T")) {
                    data.Solitary.Span = (int)(waypointControl1.Waypoint.TimeMark - dateTimePickerStart.Value).TotalMinutes;
                } else {
                    data.Solitary.SetSpanNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("V") && textBoxVelocity.Text.IsDoubleConvertible()) {
                    data.Solitary.Velocity = double.Parse(textBoxVelocity.Text);
                } else {
                    data.Solitary.SetVelocityNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("E") && textBoxExposure.Text.IsDoubleConvertible()) {
                    data.Solitary.Exposure = double.Parse(textBoxExposure.Text);
                } else {
                    data.Solitary.SetExposureNull();
                }
            }
        }

        private void saveGear(Survey.EquipmentRow unitRow) {

            foreach (Survey.VirtueRow indexVirtueRow in ReaderSettings.SamplersIndex.Virtue) {
                Survey.VirtueRow virtueRow = ((Survey)unitRow.Table.DataSet).Virtue.AddVirtueRow(indexVirtueRow.Name, indexVirtueRow.Notation);

                TextBox tb = tabPageSampler.Controls.Find("textBox" + virtueRow.Name, true)?[0] as TextBox;

                if (tb.Text.IsDoubleConvertible()) {
                    ((Survey)unitRow.Table.DataSet).SamplerVirtue.AddSamplerVirtueRow(unitRow, virtueRow, double.Parse(tb.Text));
                }
            }
        }

        private void setEndpoint(Waypoint waypoint)
        {
            waypointControl1.Waypoint.Latitude = waypoint.Latitude;
            waypointControl1.Waypoint.Longitude = waypoint.Longitude;

            if (!waypoint.IsTimeMarkNull)
            {
                if (data.Solitary.SamplerRow.IsPassive() && taskDialogLocationHandle.ShowDialog(this) == tdbSinking)
                {
                    dateTimePickerStart.Value = waypoint.TimeMark;
                    //sampler_Changed(dateTimePickerStart, new EventArgs());
                }
                else
                {
                    waypointControl1.Waypoint.TimeMark = waypoint.TimeMark;
                    dateTimePickerEnd.Value = waypoint.TimeMark;
                }
            }

            waypointControl1.UpdateValues();

            if (waypoint.IsNameNull) { }
            else
            {
                textBoxLabel.Text = waypoint.Name;
            }
        }



        private void fishCard_OnSaved(object sender, EventArgs e) {
            saveSamplerValues();
        }

        private void fishCard_OnCleared(object sender, EventArgs e) {
            clearGear();
            clearEffort();
        }

        private void fishCard_OnEquipmentSelected(object sender, EquipmentEventArgs e) {
            Survey.EquipmentRow unitRow = e.Row;

            foreach (Survey.SamplerVirtueRow row in unitRow.GetSamplerVirtueRows()) {
                TextBox tb = tabPageSampler.Controls.Find("textBox" + row.VirtueRow.Name, true)?[0] as TextBox;
                tb.Text = row.Value.ToString();
            }
        }

        private void fishCard_OnEquipmentSaved(object sender, EquipmentEventArgs e) {
            saveGear(e.Row);
            if (ReaderSettings.Equipment.Equipment.FindDuplicate(e.Row) == null) {
                saveGear(ReaderSettings.Equipment.Equipment.AddEquipmentRow(ReaderSettings.Equipment.Sampler.FindByID(SelectedSampler.ID)));
            }
        }

        private void waypointControl1_Changed(object sender, EventArgs e) {
            if (waypointControl1.ContainsFocus) {
                dateTimePickerEnd.Value = waypointControl1.Waypoint.TimeMark;
            }
        }

        private void waypointControl1_LocationImported(object sender, LocationDataEventArgs e) {
            if (!SelectedSampler.IsEffortFormulaNull() &&
                SelectedSampler.EffortFormula.Contains('E')) {
                ListLocation locationSelection = new ListLocation(e.Filenames);
                locationSelection.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                locationSelection.LocationSelected += new LocationEventHandler(locationData_Selected);
                locationSelection.ShowDialog(this);
            } else {
                ListWaypoints waypoints = new ListWaypoints(e.Filenames);

                if (waypoints.Count == 0) { } else if (waypoints.Count == 1) {
                    PreciseAreaMode = false;
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
                PreciseAreaMode = false;
                setEndpoint(waypoint);
            } else if (e.LocationObject is Polygon) {
                Polygon poly = (Polygon)e.LocationObject;
                PreciseAreaMode = true;
                textBoxExactArea.Text = (poly.Area / 10000d).ToString("N4");
                setEndpoint(poly.Points.Last());

                //HandlePolygon((Polygon)e.LocationObject);
            } else if (e.LocationObject is Track) {
                tdbAsPoly.Enabled = !SelectedSampler.EffortFormula.Contains("T");

                TaskDialogButton tdb = taskDialogTrackHandle.ShowDialog();

                PreciseAreaMode = tdb == tdbAsPoly;

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

                    textBoxExposure.Text = Track.TotalLength(tracks).ToString("N1");
                    if (SelectedSampler.EffortFormula.Contains("T")) dateTimePickerStart.Value = tracks[0].Points[0].TimeMark;
                    if (SelectedSampler.EffortFormula.Contains("V")) textBoxVelocity.Text = Track.AverageKmph(tracks).ToString("N3");
                } else if (tdb == tdbAsPoly) {
                    // Behave like polygon

                    double s = 0;
                    foreach (Track track in tracks) {
                        Polygon poly = new Polygon(track);
                        s += poly.Area;
                    }

                    PreciseAreaMode = true;
                    textBoxExactArea.Text = (s / 10000d).ToString("N4");
                    setEndpoint(wpts.Last());

                    //HandlePolygon(new Polygon(track));
                }
            }

            tabControl.SelectedTab = tabPageCollect;
        }


        private void Logger_IndividualsRequired(object sender, EventArgs e) {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null) {

                    Wild.Survey.LogRow logRow = Logger.SaveLogRow(gridRow);
                    Individuals individuals = null;

                    foreach (Form form in Application.OpenForms) {
                        if (!(form is Individuals)) continue;
                        if (((Individuals)form).LogRow == logRow) {
                            individuals = (Individuals)form;
                        }
                    }

                    if (individuals == null) {
                        individuals = new Individuals(logRow);
                        //individuals.SetColumns(ColumnSpecies, ColumnQuantity, ColumnMass);
                        individuals.LogLine = gridRow;
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

            sampler_ValueChanged(sender, e);

            if (!data.Solitary.IsSpanNull()) {
                labelDuration.ResetFormatted(Math.Floor(data.Solitary.Duration.TotalHours),
                    data.Solitary.Duration.Minutes, data.Solitary.Duration.TotalHours);
            }
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e) {
            dateTimePickerEnd.Value = dateTimePickerEnd.Value.AddSeconds(
                -dateTimePickerEnd.Value.Second);

            if (dateTimePickerEnd.ContainsFocus) {
                waypointControl1.SetDateTime(dateTimePickerEnd.Value);
                waypointControl1.Save();
            }

            //dateTimePickerStart.MaxDate = dateTimePickerEnd.Value;

            sampler_ValueChanged(sender, e);

            if (!data.Solitary.IsSpanNull()) {
                labelDuration.ResetFormatted(Math.Floor(data.Solitary.Duration.TotalHours),
                    data.Solitary.Duration.Minutes, data.Solitary.Duration.TotalHours);
            }
        }

        private void dateTimePickerStart_EnabledChanged(object sender, EventArgs e) {
            labelDuration.Enabled = dateTimePickerStart.Enabled;
        }

        private void sampler_ValueChanged(object sender, EventArgs e) {
            if (AllowEffortCalculation) {
                saveSamplerValues();

                if (textBoxOpening.Enabled) {
                    if (textBoxLength.Text.IsDoubleConvertible()) {
                        if (textBoxOpening.Text.IsDoubleConvertible()) {
                            if (Convert.ToDouble(textBoxOpening.Text) >=
                                Convert.ToDouble(textBoxLength.Text)) {
                                pictureBoxWarnOpening.Visible = true;
                                statusCard.Message(Resources.Interface.Messages.EffectiveError);
                            } else {
                                pictureBoxWarnOpening.Visible = false;
                            }
                        } else {
                            pictureBoxWarnOpening.Visible = true;
                            statusCard.Message(Resources.Interface.Messages.EffectiveEmpty);
                        }


                        if (textBoxExposure.Text.IsDoubleConvertible() &&
                            Convert.ToDouble(textBoxExposure.Text) < 2 * Convert.ToDouble(textBoxLength.Text) / Math.PI) {
                            pictureBoxWarnExposure.Visible = true;
                            statusCard.Message(Resources.Interface.Messages.SeinSpreadError);
                        } else {
                            pictureBoxWarnExposure.Visible = false;
                        }
                    } else {
                        pictureBoxWarnOpening.Visible = false;
                        pictureBoxWarnExposure.Visible = false;
                    }
                } else {
                    pictureBoxWarnOpening.Visible = false;
                    pictureBoxWarnExposure.Visible = false;
                }

                if (textBoxExposure.Enabled && textBoxExposure.ReadOnly) {
                    textBoxExposure.Text = data.Solitary.GetExposure().ToString("0.####");
                }

                textBoxEfforts.Text = data.Solitary.GetEffort(ExpressionVariant.Efforts).ToString("0.####");
                textBoxArea.Text = data.Solitary.GetEffort(ExpressionVariant.Square).ToString("0.####");
                textBoxVolume.Text = data.Solitary.GetEffort(ExpressionVariant.Volume).ToString("0.####");
            }

            isChanged = true;
        }

        private void samplerValue_EnabledChanged(object sender, EventArgs e) {
            if (!((TextBox)sender).Enabled) {
                ((TextBox)sender).Text = string.Empty;
            }
        }

        private void sampler_Changed(object sender, EventArgs e) {
            if (SelectedSampler == null) return;

            if (SelectedSampler.IsEffortFormulaNull()) return;

            labelLength.Enabled = textBoxLength.Enabled = SelectedSampler.EffortFormula.Contains("L");
            labelOpening.Enabled = textBoxOpening.Enabled = SelectedSampler.EffortFormula.Contains("O");
            labelHeight.Enabled = textBoxHeight.Enabled = SelectedSampler.EffortFormula.Contains("H");
            labelSquare.Enabled = textBoxSquare.Enabled = SelectedSampler.EffortFormula.Contains("S");

            labelMesh.Enabled = textBoxMesh.Enabled = SelectedSampler.EffortFormula.Contains("M");
            labelHook.Enabled = textBoxHook.Enabled = SelectedSampler.EffortFormula.Contains("J");

            //labelOperation.Enabled = maskedTextBoxOperation.Enabled = SelectedSampler.EffortFormula.Contains("T");
            labelOperation.Enabled = dateTimePickerStart.Enabled = SelectedSampler.EffortFormula.Contains("T");
            labelVelocity.Enabled = textBoxVelocity.Enabled = SelectedSampler.EffortFormula.Contains("V");
            labelExposure.Enabled = textBoxExposure.Enabled = SelectedSampler.EffortFormula.Contains("E");
            textBoxExposure.ReadOnly = SelectedSampler.EffortFormula.Contains("V") && SelectedSampler.EffortFormula.Contains("T");

            sampler_ValueChanged(sender, e);
        }

        private void buttonGear_Click(object sender, EventArgs e) {
            contextGear.Show(buttonEquipment, new Point(0, buttonEquipment.Height), ToolStripDropDownDirection.BelowRight);
        }


        private void pictureBoxWarnOpening_MouseHover(object sender, EventArgs e) {
            if (textBoxOpening.Text.IsDoubleConvertible()) {
                textBoxOpening.NotifyInstantly(Resources.Interface.Messages.FixOpening,
                    textBoxOpening.Text, textBoxLength.Text);
            } else {
                textBoxOpening.NotifyInstantly(Resources.Interface.Messages.FixEmptyOpening);
            }
        }

        private void pictureBoxWarnOpening_MouseLeave(object sender, EventArgs e) {
            //toolTipAttention.Hide(textBoxOpening);
        }

        private void pictureBoxWarnOpening_DoubleClick(object sender, EventArgs e) {
            textBoxOpening.Text = (Convert.ToDouble(textBoxLength.Text) *
                Service.DefaultOpening(SelectedSampler.ID)).ToString("N0");
        }


        private void pictureBoxWarnExposure_MouseHover(object sender, EventArgs e) {
            textBoxExposure.NotifyInstantly(Resources.Interface.Messages.FixSpread,
                textBoxExposure.Text, textBoxLength.Text);
        }

        private void pictureBoxWarnExposure_MouseLeave(object sender, EventArgs e) {
            //toolTipAttention.Hide(textBoxExposure);
        }

        private void pictureBoxWarnExposure_DoubleClick(object sender, EventArgs e) {
            textBoxExposure.Text = Math.Ceiling(2 * Convert.ToDouble(textBoxLength.Text) / Math.PI).ToString("N0");
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e) {
            value_Changed(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {

        }
    }
}
