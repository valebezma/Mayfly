using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species.Controls
{
    public partial class DefinitionPanel : UserControl
    {
        #region Properties

        public List<TaxonomicIndex.StateRow> Selections;

        public TaxonomicIndex.StepRow CurrentStep;

        private TaxonomicIndex Key { get { return (TaxonomicIndex)CurrentStep.Table.DataSet; } }

        public List<Feature> Features;

        #endregion

        public event StateClickedEventHandler UserSelectedState;

        public event StateClickedEventHandler SpeciesDefined;

        public event DefinitionEventHandler StepChanged;

        public DefinitionPanel()
        {
            InitializeComponent();
            Features = new List<Feature>();
            Selections = new List<TaxonomicIndex.StateRow>();
        }

        #region Methods

        public void ClearFeatures()
        {
            foreach (Feature feature in Features)
            {
                feature.Collapsed += feature_Collapsed;
                feature.Collapse();
            }
        }

        private void feature_Collapsed(object sender, EventArgs e)
        {
            Controls.Remove((Feature)sender);
            Features.Remove((Feature)sender);

            if (Features.Count == 0)
            {
                AddFeatures();
            }
        }

        private void AddFeatures()
        {
            // Load new features

            if (CurrentStep == null || CurrentStep.GetFeatureRows().Length == 0)
            {
                labelError.Visible = true;
            }
            else
            {
                labelError.Visible = false;

                foreach (TaxonomicIndex.FeatureRow featureRow in
                    CurrentStep.GetFeatureRows())
                {
                    AddFeature(featureRow);
                }

                if (Features.Count == 1)
                {
                    Features[0].Expand();
                }

                //ArrangeFeatures();
            }

            if (StepChanged != null)
            {
                StepChanged.Invoke(this, new DefinitionEventArgs(CurrentStep));
            }
        }
    

        private void ArrangeFeatures()
        {
            int heightSum = 0;
            foreach (Controls.Feature feature in Features)
            {
                heightSum += feature.Height - 1;
            }

            int topSpace = (Height - heightSum) / 2;

            Features[0].Top = topSpace;

            for (int i = 1; i < Features.Count; i++)
            {
                Features[i].Top = Features[i - 1].Top + Features[i - 1].Height - 1;
            }

            foreach (Controls.Feature feature in Features)
            {
                feature.Left = Width / 2 - feature.Width / 2;
            }
        }

        public void SetFeatures(TaxonomicIndex.StepRow stepRow)
        {
            CurrentStep = stepRow;

            if (Features.Count == 0)
            {
                AddFeatures();
            }
            else
            {
                ClearFeatures();
            }

            //if (stepRow == null || stepRow.GetFeatureRows().Length == 0)
            //{
            //    labelError.Visible = true;
            //}
            //else
            //{
            //    labelError.Visible = false;

            //    foreach (SpeciesKey.FeatureRow featureRow in
            //        stepRow.GetFeatureRows())
            //    {

            //        AddFeature(featureRow);
            //    }
            //}

            //if (StepChanged != null) {

            //    StepChanged.Invoke(this, new DefinitionEventArgs(stepRow));
            //}
        }

        public void SetFeatures(TaxonomicIndex.FeatureRow featureRow)
        {
            SetFeatures(featureRow.StepRow);

            foreach (Controls.Feature feature in Features)
            {
                if (feature.FeatureRow == featureRow) {

                    feature.Expand();
                }
                else {

                    feature.Collapse();
                }
            }
        }

        private Feature AddFeature(TaxonomicIndex.FeatureRow featureRow)
        {
            Feature newFeature = new Feature(featureRow);
            newFeature.ExpandedSize = new System.Drawing.Size(this.Width - 
                this.Padding.Left - this.Padding.Right, newFeature.ExpandedSize.Height);

            newFeature.Anchor = AnchorStyles.None;
            newFeature.Expanded += newFeature_Expanded;
            newFeature.Collapsed += newFeature_Collapsed;
            newFeature.UserSelectedState += newFeature_UserSelectedState;
            newFeature.SizeChanged += newFeature_SizeChanged;

            Controls.Add(newFeature);
            Features.Add(newFeature);

            if (Features.Count > 1) newFeature.Collapse(); 
            ArrangeFeatures();

            return newFeature;
        }

        public void Restart()
        {
            SetFeatures(Key.InitialDefinitionStep);
            Selections.Clear();
        }

        public void GetBack()
        {
            GetBack(1);
        }

        public void GetBack(int steps)
        {
            for (int i = 0; i < steps; i++) {

                TaxonomicIndex.StateRow lastState = Selections.Last();
                Selections.Remove(Selections.Last());
                SetFeatures(lastState.FeatureRow);
            }
        }

        public void GetBack(TaxonomicIndex.StateRow stateRow)
        {
            while (Selections.Last() != stateRow)
            {
                Selections.RemoveAt(Selections.Count - 1);
            }
            
            Selections.RemoveAt(Selections.Count - 1);
            SetFeatures(stateRow.FeatureRow);
        }

        public void GetForward(TaxonomicIndex.StateRow stateRow)
        {
            if (CurrentStep.AvailableStates.Contains(stateRow))
            {
                newFeature_UserSelectedState(this, new StateClickedEventArgs(FindState(stateRow)));
            }
            else
            {
                foreach (TaxonomicIndex.StateRow sttRow in CurrentStep.AvailableStates)
                {
                    if (sttRow.DoesLeadTo(stateRow))
                    {
                        Selections.Add(sttRow);
                        SetFeatures(sttRow.Next);
                    }
                }
            }
        }

        public void GetTo(TaxonomicIndex.StepRow targetDefinitionRow)
        {
            while (!CurrentStep.Equals(targetDefinitionRow))
            {
                foreach (TaxonomicIndex.StateRow stateRow in
                    CurrentStep.AvailableStates)
                {
                    if (stateRow.DoesLeadTo(targetDefinitionRow))
                    {
                        Selections.Add(stateRow);
                        CurrentStep = stateRow.Next;
                        break;
                    }
                }
            }

            ClearFeatures();

            foreach (TaxonomicIndex.FeatureRow featureRow in targetDefinitionRow.GetFeatureRows())
            {
                AddFeature(featureRow);
            }

            if (StepChanged != null)
            {
                StepChanged.Invoke(this, new DefinitionEventArgs(targetDefinitionRow));
            }
        }

        public void GetTo(TaxonomicIndex.StateRow targetStateRow)
        {
            GetTo(targetStateRow, false);
        }

        public void GetTo(TaxonomicIndex.StateRow targetStateRow, bool preventClick)
        {
            while (!CurrentStep.AvailableStates.Contains(targetStateRow))
            {
                foreach (TaxonomicIndex.StateRow stateRow in 
                    CurrentStep.AvailableStates)
                {
                    if (stateRow.DoesLeadTo(targetStateRow))
                    {
                        Selections.Add(stateRow);
                        CurrentStep = stateRow.Next;
                        break;
                    }
                }
            }

            if (CurrentStep.AvailableStates.Contains(targetStateRow))
            {
                ClearFeatures();

                foreach (TaxonomicIndex.FeatureRow featureRow in
                    targetStateRow.FeatureRow.StepRow.GetFeatureRows())
                {
                    AddFeature(featureRow);
                }

                if (StepChanged != null)
                {
                    StepChanged.Invoke(this, new DefinitionEventArgs(targetStateRow.FeatureRow.StepRow));
                }

                if (!preventClick)
                {
                    newFeature_UserSelectedState(this, new StateClickedEventArgs(FindState(targetStateRow)));
                }
            }
        }

        public State FindState(TaxonomicIndex.StateRow stateRow)
        {
            foreach (Feature feature in Features)
            {
                foreach (State state in feature.States)
                {
                    if (state.StateRow == stateRow)
                    {
                        return state;
                    }
                }
            }

            return null;
        }

        #endregion

        void newFeature_SizeChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ArrangeFeatures();
        }

        private void newFeature_UserSelectedState(object sender, StateClickedEventArgs e)
        {
            if (e.NextStep == null)
            {
                if (e.IsSpeciesAttached)
                {
                    if (SpeciesDefined != null)
                    {
                        SpeciesDefined.Invoke(sender, e);
                    }
                }
            }
            else
            {
                Selections.Add(e.StateRow);
                SetFeatures(e.NextStep);
            }

            if (UserSelectedState != null)
            {
                UserSelectedState.Invoke(sender, e);
            }
        }

        private void newFeature_Collapsed(object sender, EventArgs e)
        {
            //ArrangeFeatures();
        }

        private void newFeature_Expanded(object sender, EventArgs e)
        {
            //foreach (Controls.Feature feature in Features)
            //{
            //    if (feature == (Controls.Feature)sender) continue;
            //    feature.Collapse();
            //}

            //ArrangeFeatures();
        }

        Diagnosis historyForm;

        Fits fitsForm;

        internal void ShowHistory()
        {
            if (historyForm == null)
            {
                historyForm = new Diagnosis(this);
                historyForm.SetFriendlyDesktopLocation(FindForm(), FormLocation.NextToHost);
            }

            historyForm.Show(FindForm());
        }

        internal void HideHistory()
        {
            if (historyForm != null)
            {
                historyForm.Hide();
            }
        }

        internal void ShowFits()
        {
            if (fitsForm == null)
            {
                fitsForm = new Fits(this);
                fitsForm.SetFriendlyDesktopLocation(FindForm(), FormLocation.NextToHost);
            }

            fitsForm.Show(FindForm());
        }

        internal void HideFits()
        {
            if (fitsForm != null)
            {
                fitsForm.Hide();
            }
        }

        private void DefinitionPanel_SizeChanged(object sender, EventArgs e)
        {
            foreach (Feature feature in Features)
            {
                feature.ExpandedSize = new Size(
                    this.Width - this.Padding.Left - this.Padding.Right,
                    feature.ExpandedSize.Height);

                if (feature.IsExpanded)
                {
                    feature.Size = feature.ExpandedSize;
                }
            }
        }
    }
}
