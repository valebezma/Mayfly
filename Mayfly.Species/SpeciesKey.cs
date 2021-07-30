using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Mayfly.Extensions;
using Mayfly.Species.Systematics;
using System.Windows.Forms;

namespace Mayfly.Species
{
    public partial class SpeciesKey
    {
        partial class TaxaRow : IComparable<TaxaRow>
        {
            public SpeciesRow[] GetSpecies()
            {
                List<SpeciesRow> result = new List<SpeciesRow>();

                foreach (RepRow repRow in this.GetRepRows())
                {
                    result.Add(repRow.SpeciesRow);
                }

                return result.ToArray();
            }

            public string FullName => string.Format("{0} {1}", this.BaseRow.Base, this.Taxon);

            public bool Includes(string species)
            {
            //    return Includes(species, false);
            //}

            //public bool Includes(string species, bool suggest)
            //{
                foreach (RepRow repRow in this.GetRepRows())
                {
                    if (repRow.SpeciesRow.Species == species)
                        return true;

                    //if (suggest && Genus(repRow.SpeciesRow.Species) == Genus(species))
                    //    return true;
                }

                return false;
            }

            public int CompareTo(TaxaRow other)
            {
                if (!this.IsTaxonNull() && !other.IsTaxonNull())
                {
                    return this.Taxon.CompareTo(other.Taxon);
                }
                else
                {
                    return 0;
                }
            }
        }

        partial class SpeciesDataTable
        {
            public SpeciesRow[] GetSpeciesNamed(string name)
            {
                List<SpeciesRow> result = new List<SpeciesRow>();
                string[] words = name.Split(' ');
                string spcName = words[words.Length - 1];
                if (!spcName.Contains("."))
                {
                    foreach (SpeciesRow speciesRow in Rows)
                    {
                        if (!speciesRow.IsSpeciesNull())
                        {
                            if (speciesRow.Species.Contains(spcName))
                            {
                                result.Add(speciesRow);
                            }
                        }
                    }
                }
                return result.ToArray();
            }

            public SpeciesRow[] GetSpeciesNameContaining(string name)
            {
                List<SpeciesRow> result = new List<SpeciesRow>();

                foreach (SpeciesRow speciesRow in Rows)
                {
                    //if (speciesRow.IsSpeciesNull()) continue
                    if (speciesRow.FullNameReport.ToUpperInvariant().Contains(name.ToUpperInvariant()))
                    {
                        result.Add(speciesRow);
                    }
                }

                return result.ToArray();
            }

            public string[] References
            {
                get
                {
                    List<string> result = new List<string>();
                    foreach (SpeciesRow speciesRow in Rows)
                    {
                        if (!speciesRow.IsReferenceNull())
                        {
                            if (!result.Contains(speciesRow.Reference)) result.Add(speciesRow.Reference);
                        }
                    }
                    return result.ToArray();
                }
            }

            public string[] Genera
            {
                get
                {
                    List<string> result = new List<string>();
                    foreach (SpeciesRow speciesRow in Rows)
                    {
                        if (!speciesRow.IsSpeciesNull())
                        {
                            string genus = speciesRow.Species.Split(' ')[0];
                            if (!result.Contains(genus)) result.Add(genus);
                        }
                    }
                    return result.ToArray();
                }
            }

            public SpeciesRow FindBySpecies(string value)
            {
                foreach (SpeciesRow speciesRow in Rows)
                {
                    if (speciesRow.IsSpeciesNull())
                    {
                        continue;
                    }

                    if (speciesRow.Species == value)
                    {
                        return speciesRow;
                    }
                }

                return null;
            }

            public SpeciesRow[] GetSorted()
            {
                return (SpeciesRow[])this.Select(null, "Species asc");
            }

        }

        partial class BaseRow
        {
            public SpeciesRow[] Varia
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SpeciesRow speciesRow in ((SpeciesKey)this.Table.DataSet).Species)
                    {
                        if (speciesRow.GetTaxon(this) == null) result.Add(speciesRow);
                    }

                    return result.ToArray();
                }
            }
        }

        partial class StepRow
        {
            public StateRow[] AvailableStates
            {
                get
                {
                    List<StateRow> result = new List<StateRow>();

                    foreach (FeatureRow featureRow in this.GetFeatureRows())
                    {
                        foreach (StateRow stateRow in featureRow.GetStateRows())
                        {
                            result.Add(stateRow);
                        }
                    }

                    return result.ToArray();
                }
            }

            public StateRow[] FollowingStates
            {
                get
                {
                    List<StateRow> result = new List<StateRow>();

                    foreach (FeatureRow featureRow in this.GetFeatureRows())
                    {
                        foreach (StateRow stateRow in featureRow.GetStateRows())
                        {
                            if (!result.Contains(stateRow))
                            {

                                result.Add(stateRow);
                            }

                            if (!stateRow.IsGotoNull())
                            {

                                result.AddRange(stateRow.Next.FollowingStates);
                            }
                        }
                    }

                    return result.ToArray();
                }
            }

            public StateRow[] ForestandingTaxaStates
            {
                get
                {
                    List<StateRow> result = new List<StateRow>();

                    foreach (StateRow stateRow in this.FollowingStates)
                    {
                        if (stateRow.IsTaxIDNull()) continue;
                        if (!result.Contains(stateRow))
                            result.Add(stateRow);
                    }

                    return result.ToArray();
                }
            }

            public StateRow[] ForestandingSpeciesStates
            {
                get
                {
                    List<StateRow> result = new List<StateRow>();

                    foreach (StateRow stateRow in this.FollowingStates)
                    {
                        if (stateRow.IsSpcIDNull()) continue;
                        if (!result.Contains(stateRow))
                            result.Add(stateRow);
                    }

                    return result.ToArray();
                }
            }

            public TaxaRow[] ForestandingTaxa
            {
                get
                {
                    List<TaxaRow> result = new List<TaxaRow>();

                    foreach (TaxaRow taxaRow in ((SpeciesKey)this.Table.DataSet).Taxa)
                    {
                        if (this.DoesLeadTo(taxaRow))
                        {
                            result.Add(taxaRow);
                        }
                    }

                    return result.ToArray();
                }
            }

            public SpeciesRow[] ForestandingSpecies
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SpeciesRow speciesRow in ((SpeciesKey)this.Table.DataSet).Species)
                    {
                        if (this.DoesLeadTo(speciesRow))
                        {
                            result.Add(speciesRow);
                        }
                    }

                    return result.ToArray();
                }
            }

            public bool DoesLeadTo(TaxaRow taxaRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.DoesLeadTo(taxaRow);
                }

                return result;
            }

            public bool DoesLeadTo(SpeciesRow spcRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.DoesLeadTo(spcRow);
                }

                return result;
            }

            public bool DoesLeadTo(StateRow stateRow)
            {
                bool result = false;

                foreach (StateRow sttRow in this.AvailableStates)
                {
                    result |= sttRow.DoesLeadTo(stateRow);
                }

                return result;
            }

            public bool DoesLeadTo(StepRow stepRow)
            {
                bool result = false;

                foreach (StateRow sttRow in this.AvailableStates)
                {
                    //if (sttRow.GetNext() == stepRow) return true;

                    result |= sttRow.DoesLeadTo(stepRow);
                }

                return result;
            }

            public bool IsInstantlyOf(TaxaRow taxaRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.TaxaRow == taxaRow;
                }

                return result;
            }

            public bool IsInstantlyOf(SpeciesRow spcRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.SpeciesRow == spcRow;
                }

                return result;
            }
        }

        partial class SpeciesRow
        {
            public SpeciesRow MajorSynonym
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SynonymyRow synRow in this.GetSynonymyRowsByFK_Species_Synonymy())
                    {
                        result.Add(synRow.SpeciesRowBySpecies_Synonymy);
                        //result.AddRange(synRow.SpeciesRowBySpecies_Synonymy.GetMajorSynonyms());
                    }

                    if (result.Count == 1)
                    {
                        return result[0];
                    }
                    else //if (result.Count > 1)
                    {
                        //throw new InvalidDataException(
                        //    string.Format("There are two or more major synonyms for {0}", this.Species));
                        return null;
                    }
                    //else if (result.Count == 0)
                    //    return null;
                }
            }

            public string[] ToolTip
            {
                get
                {
                    List<string> result = new List<string>();

                    result.Add(this.FullName);
                    result.Add(string.Empty);

                    foreach (RepRow repRow in this.GetRepRows())
                    {
                        result.Add(repRow.TaxaRow.FullName);
                    }

                    result.Add(string.Empty);

                    if (this.IsDescriptionNull())
                    {
                        result.Add(Resources.Interface.DescriptionNull);
                    }
                    else
                    {
                        result.Add(this.Description);
                    }

                    return result.ToArray();
                }
            }

            /// <summary>
            /// Return full species Name including scientific name, local name and reference
            /// </summary>
            public string FullName
            {
                get
                {
                    string result = string.Empty;

                    if (!this.IsSpeciesNull())
                    {
                        result += this.Species;
                    }

                    if (!this.IsLocalNull())
                    {
                        result += " (" + this.Local + ")";
                    }

                    if (!this.IsReferenceNull())
                    {
                        result += " " + this.Reference;
                    }

                    return string.IsNullOrWhiteSpace(result) ? Resources.Interface.UnidentifiedTitle : result;
                }
            }

            /// <summary>
            /// Return full species Name including scientific name, local name and reference formatted for HTML
            /// </summary>
            public string FullNameReport
            {
                get
                {
                    string result = this.ScientificNameReport;

                    if (!this.IsLocalNull())
                    {
                        result += " (" + this.Local + ")";
                    }

                    if (!this.IsReferenceNull())
                    {
                        result += " " + this.Reference;
                    }

                    return result;
                }
            }

            /// <summary>
            /// Returns Scientifica name formatted for HTML
            /// </summary>
            public string ScientificNameReport
            {
                get
                {
                    string result = string.Empty;

                    if (!this.IsSpeciesNull())
                    {
                        result += "<span class='latin'>" + this.Species + "</span>";
                    }

                    foreach (string serviceWord in new string[] { "gr.", "morpha", "ex gr.", "nov.", "sp.", "sp. n." })
                    {
                        result = result.Replace(" " + serviceWord + " ",
                            "<span class='latin-inside'> " + serviceWord + " </span>");
                    }

                    return string.IsNullOrWhiteSpace(result) ? Resources.Interface.UnidentifiedTitle : result;
                }
            }

            /// <summary>
            /// Returns Local name or Scientific (if species has no local name) formatted for HTML
            /// </summary>
            public string ShortNameReport
            {
                get
                {
                    if (this.IsLocalNull()) { return ScientificNameReport; }
                    return this.Local;
                }
            }

            public TaxaRow GetTaxon(BaseRow baseRow)
            {
                foreach (TaxaRow taxaRow in baseRow.GetTaxaRows())
                {
                    if (taxaRow.Includes(this.Species))
                    {
                        return taxaRow;
                    }
                }

                return null;
            }

            public void RemoveFrom(TaxaRow taxaRow)
            {
                ((SpeciesKey)this.Table.DataSet).Rep.Select(
                    string.Format("TaxID = {0} AND SpcID = {1}",
                    taxaRow.ID, this.ID))[0].Delete();
            }

            public SpeciesRow[] Synonyms
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();
                    if (this.MajorSynonym != null) result.Add(this.MajorSynonym);
                    result.AddRange(this.MinorSynonyms);
                    return result.ToArray();
                }
            }

            public SpeciesRow[] MinorSynonyms
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SynonymyRow synRow in this.GetSynonymyRowsBySpecies_Synonymy())
                    {
                        result.Add(synRow.SpeciesRowByFK_Species_Synonymy);
                        result.AddRange(synRow.SpeciesRowByFK_Species_Synonymy.MinorSynonyms);
                    }


                    return result.ToArray();
                }
            }
        }

        partial class StateRow
        {
            public StepRow Next
            {
                get
                {
                    if (this.IsGotoNull()) return null;
                    return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto);
                }
            }

            public bool DoesLeadTo(TaxaRow taxaRow)
            {
                // If related to taxaRow instantly
                if (!this.IsTaxIDNull() && this.TaxaRow == taxaRow) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxaRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(taxaRow);
            }

            public bool DoesLeadTo(SpeciesRow spcRow)
            {
                // If related to taxaRow instantly
                if (!this.IsSpcIDNull() && this.SpeciesRow == spcRow) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxaRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(spcRow);
            }

            public bool DoesLeadTo(StepRow stepRow)
            {
                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                if (this.Next.Equals(stepRow)) return true;

                // If next step follow to taxaRow
                return this.Next.DoesLeadTo(stepRow);
            }

            public bool DoesLeadTo(StateRow stateRow)
            {
                if (this.Equals(stateRow)) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxaRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(stateRow);
            }

            public TaxaRow[] ForestandingTaxa
            {
                get
                {
                    List<TaxaRow> result = new List<TaxaRow>();

                    foreach (TaxaRow taxaRow in ((SpeciesKey)this.Table.DataSet).Taxa)
                    {
                        if (this.DoesLeadTo(taxaRow))
                        {
                            result.Add(taxaRow);
                        }
                    }

                    return result.ToArray();
                }
            }

            public SpeciesRow[] ForestandingSpecies
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SpeciesRow speciesRow in ((SpeciesKey)this.Table.DataSet).Species)
                    {
                        if (this.DoesLeadTo(speciesRow))
                        {
                            result.Add(speciesRow);
                        }
                    }

                    return result.ToArray();
                }
            }

            public string ShortDescription
            {
                get
                {
                    string result = string.Empty;

                    if (!this.IsTaxIDNull())
                    {
                        result += this.TaxaRow.Taxon + ": ";
                    }

                    if (this.IsGotoNull())
                    {
                        if (this.IsSpcIDNull())
                        {
                            result += "State doesn't lead further.";
                        }
                        else
                        {
                            result += this.SpeciesRow.Species + " ";
                        }
                    }
                    else
                    {
                        result += "К шагу " + Goto;
                    }

                    return result;
                }
            }
        }

        public StepRow InitialDefinitionStep
        {
            get
            {
                foreach (StepRow stepRow in Step)
                {
                    if (stepRow.IsEntryNull()) continue;
                    if (stepRow.Entry) return stepRow;
                }

                return null;
            }
        }

        public Report Report
        {
            get
            {
                Report report = new Report(FileSystem.GetFriendlyFiletypeName(UserSettings.Interface.Extension), "key.css");

                if (Species.Count > 0)
                {
                    SpeciesList(report);
                    report.BreakPage();
                }

                report.EndBranded();

                return report;
            }
        }


        public void Read(string fileName)
        {
            try { ReadXml(fileName); }
            catch { Log.Write(EventType.Maintenance, "First call for {0}. File is empty and will be rewritten.", fileName); }
        }

        public void SaveToFile(string fileName)
        {
            File.WriteAllText(fileName, GetXml());
        }

        /// <summary>
        /// Imports all of species records from current key to another
        /// </summary>
        /// <param name="key">Destination species key</param>
        public void ImportTo(SpeciesKey key)
        {
            ImportTo(key, false);
        }

        public void ImportTo(SpeciesKey key, bool inspect)
        {
            // It will ignore " sp." records

            foreach (SpeciesRow speciesRow in Species)
            {
                if (speciesRow.Species.EndsWith("sp.")) continue;

                SpeciesRow destRow = key.Species.FindBySpecies(speciesRow.Species);

                if (destRow == null)
                {
                    destRow = key.Species.NewSpeciesRow();
                    destRow.Species = speciesRow.Species;
                    if (!speciesRow.IsDescriptionNull()) destRow.Description = speciesRow.Description;
                    if (!speciesRow.IsReferenceNull()) destRow.Reference = speciesRow.Reference;
                    if (!speciesRow.IsLocalNull()) destRow.Local = speciesRow.Local;

                    if (inspect)
                    {
                        AddSpecies addSpecies = new AddSpecies(destRow);
                        //addSpecies.AddTaxaSelectors(key);

                        if (addSpecies.ShowDialog() == DialogResult.OK)
                        {
                            key.Species.AddSpeciesRow(destRow);

                            foreach (SpeciesKey.TaxaRow taxaRow in addSpecies.SelectedTaxa)
                            {
                                key.Rep.AddRepRow(taxaRow, destRow);
                            }
                        }
                    }
                    else
                    {
                        key.Species.AddSpeciesRow(destRow);
                    }
                }
                else
                {
                    if (!speciesRow.IsDescriptionNull() && !destRow.IsDescriptionNull() &&
                        destRow.Description != speciesRow.Description)
                    {
                        destRow.Description += System.Environment.NewLine + speciesRow.Description;
                    }
                }
            }
        }



        public static string Genus(string species)
        {
            string[] values = species.GetLongWords(4);

            if (values.Length > 0)
            {
                return values[0];
            }
            else
            {
                return null;
            }
        }

        public SpeciesRow[] CloseRelatives(SpeciesRow speciesRow)
        {
            if (speciesRow.IsSpeciesNull()) return null;

            List<SpeciesRow> result = new List<SpeciesRow>();

            string genus = Genus(speciesRow.Species);

            foreach (SpeciesRow currentSpeciesRow in Species.Rows)
            {
                if (currentSpeciesRow.IsSpeciesNull()) continue;

                if (Genus(currentSpeciesRow.Species) == genus &&
                    !result.Contains(currentSpeciesRow))
                {
                    result.Add(currentSpeciesRow);
                }
            }

            return result.ToArray();
        }

        public static string SpecificName(string species)
        {
            string[] values = species.GetLongWords(4);

            if (values.Length > 1)
            {
                return values[1];
            }
            else
            {
                return null;
            }
        }

        public TaxaRow GetTaxon(string species, BaseRow baseRow)
        {
            foreach (TaxaRow taxaRow in baseRow.GetTaxaRows())
            {
                if (taxaRow.Includes(species))
                    return taxaRow;
            }

            return null;
        }



        public bool Contains(string species)
        {
            return Species.FindBySpecies(species) != null;
        }

        public SpeciesRow[] GetByFeatureStates(StateRow[] stateRows)
        {
            List<SpeciesRow> result = new List<SpeciesRow>();

            foreach (SpeciesRow speciesRow in Species)
            {
                bool satisfy = true;

                foreach (StateRow stateRow in stateRows)
                {
                    satisfy &= stateRow.DoesLeadTo(speciesRow);
                }

                if (satisfy)
                    result.Add(speciesRow);
            }

            return result.ToArray();
        }



        private void SpeciesList(Report report)
        {
            report.AddHeader(Resources.Interface.SpeciesListCaption);

            //this.Base.Columns["Name"].GetStrings(true);

            Report.Table table1 = new Report.Table();

            table1.StartHeader();
            table1.AddHeaderCell("No", .05);
            table1.AddHeaderCell("Species");
            for(int i = 0; i < this.Base.Count; i++)
            {
                table1.AddHeaderCell(this.Base[i].Base);
            }
            table1.EndHeader();

            int Counter = 1;

            foreach (SpeciesRow speciesRow in Species.GetSorted())
            {
                table1.StartRow();
                table1.AddCell(Counter);
                table1.StartCellOfClass("sp" + speciesRow.ID, speciesRow.FullName);

                // Insert Species taxa
                //string reps = string.Empty;
                //foreach (RepRow repRow in speciesRow.GetRepRows())
                //{
                //    reps += string.Format("{0} {1}<br>",
                //        repRow.TaxaRow.BaseRow.Base,
                //        repRow.TaxaRow.Taxon);
                //}
                //report.AddParagraphClass("description", reps);

                // Insert species description if exist
                //if (!speciesRow.IsDescriptionNull())
                //{
                //    report.AddParagraphClass("description", speciesRow.Description);
                //}
                table1.EndCell();


                for (int i = 0; i < this.Base.Count; i++)
                {
                    SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(this.Base[i]);
                    table1.AddCell(taxaRow == null ? "" : taxaRow.Taxon);
                }



                table1.EndRow();

                Counter++;
            }

            report.AddTable(table1);
        }

        public bool IsKeyAvailable
        {
            get
            {
                return Step.Count > 0;
            }
        }
    }
}
