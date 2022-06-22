using Mayfly.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Species
{
    public partial class SpeciesKey
    {
        partial class TaxonRow : IComparable<TaxonRow>, IFormattable
        {
            public TaxonomicRank TaxonomicRank { get { return new TaxonomicRank(Rank); } }

            public string TaxonName
            {
                get
                {
                    return IsNameNull() ? Taxon : Name.GetLocalizedValue();
                }
            }

            public string FullName => string.Format("{0} {1}", this?.TaxonomicRank.Name, TaxonName);

            public string InterfaceString
            {
                get
                {
                    int wealth = this.GetSpecies().Length;
                    return wealth > 0 ? string.Format("{0} ({1})", this.FullName, wealth) : this.FullName;
                }
            }

            public string SortableString
            {
                get
                {
                    return string.Format("{0:000} {1:000} {2}", Rank, (IsIndexNull() ? 999 : Index), TaxonName);
                }
            }

            public bool HasDerivations
            {
                get
                {
                    return this.GetParentRows("FK_Taxon_Taxon").Length > 0;
                }
            }



            public SpeciesRow[] GetSpecies()
            {
                List<SpeciesRow> result = new List<SpeciesRow>();

                result.AddRange(this.GetSpeciesRows());

                foreach (TaxonRow taxonRow in this.GetTaxonRows())
                {
                    result.AddRange(taxonRow.GetSpecies());
                }

                return result.ToArray();
            }

            public TaxonRow[] GetTaxon(bool populatedOnly)
            {
                List<TaxonRow> result = new List<TaxonRow>();

                foreach (TaxonRow taxonRow in this.GetTaxonRows())
                {
                    if (!populatedOnly || taxonRow.GetSpeciesRows().Length > 0)
                    {
                        result.Add(taxonRow);
                    }

                    result.AddRange(taxonRow.GetTaxon(populatedOnly));
                }

                return result.ToArray();
            }

            public bool Includes(TaxonRow taxonRow)
            {
                return Array.IndexOf(GetTaxon(false), taxonRow) > -1;
            }

            public bool Includes(SpeciesRow spcRow, bool searchDerivates)
            {
                if (Array.IndexOf(GetSpeciesRows(), spcRow) > -1) return true;

                if (searchDerivates)
                {
                    foreach (TaxonRow taxonRow in GetTaxon(true)) // search derivates
                    {
                        if (taxonRow.Includes(spcRow, true))
                            return true;
                    }
                }

                return false;
            }

            public bool Includes(SpeciesRow spcRow)
            {
                return Includes(spcRow, true);
            }



            public int CompareTo(TaxonRow other)
            {
                return this.SortableString.CompareTo(other.SortableString);
            }


            public override string ToString()
            {
                return FullName;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                switch (format.ToLowerInvariant())
                {
                    case "f":
                        return FullName;

                    default:
                        return TaxonName;
                }
            }
        }

        partial class SpeciesRow : IFormattable, IComparable<SpeciesRow>
        {
            public SpeciesRow MajorSynonym
            {
                get
                {
                    return this.SpeciesRowParent;
                }
            }

            public SpeciesRow[] MinorSynonyms
            {
                get
                {
                    List<SpeciesRow> result = new List<SpeciesRow>();

                    foreach (SpeciesRow spcRow in this.tableSpecies)
                    {
                        if (spcRow.SpeciesRowParent == this)
                        {
                            result.Add(spcRow);
                        }
                    }

                    return result.ToArray();
                }
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

            public string[] ToolTip
            {
                get
                {
                    List<string> result = new List<string>
                    {
                        this.FullName,
                        string.Empty
                    };

                    foreach (TaxonRow tr in GetParents())
                    {
                        result.Add(tr.FullName);
                    }

                    result.Add(string.Empty);

                    // Add all synonims???

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



            public string ShortName
            {
                get
                {
                    return IsNameNull() ? Species : Name.GetLocalizedValue();
                }
            }

            public string ScientificName
            {
                get
                {
                    return Species;
                }
            }

            public string ScientificNameReport
            {
                get
                {
                    string result = string.Format("<span class='latin'>{0}</span>", ScientificName);

                    foreach (string serviceWord in new string[] { "gr.", "morpha", "ex gr.", "nov.", "sp.", "sp. n." })
                    {
                        result = result.Replace(" " + serviceWord + " ",
                            "<span class='latin-inside'> " + serviceWord + " </span>");
                    }

                    return string.IsNullOrWhiteSpace(result) ? Resources.Interface.UnidentifiedTitle : result;
                }
            }

            public string FullName
            {
                get
                {
                    return string.Format("{0} {1} {2}", IsNameNull() ? string.Empty : Name.GetLocalizedValue(), ScientificName, IsReferenceNull() ? string.Empty : Reference).Trim();
                }
            }

            public string FullNameReport
            {
                get
                {
                    return string.Format("{0} {1} {2}", ShortName, ScientificNameReport, IsReferenceNull() ? string.Empty : Reference).Trim();
                }
            }

            public string SortableString
            {
                get
                {
                    return TaxonRow?.SortableString + ScientificName.Replace(" gr.", string.Empty);
                }
            }


            public bool Validate(string record)
            {
                if (Species == record) return true;

                foreach (SpeciesRow spcRow in MinorSynonyms)
                {
                    if (spcRow.Validate(record)) return true;
                }

                return false;
            }



            public TaxonRow[] GetParents()
            {
                List<TaxonRow> result = new List<TaxonRow>();

                foreach (TaxonRow taxonRow in ((SpeciesKey)tableSpecies.DataSet).Taxon)
                {
                    if (taxonRow.Includes(this, true))
                    {
                        result.Add(taxonRow);
                    }
                }

                result.Sort();
                return result.ToArray();
            }

            public TaxonRow GetTaxon(int rank)
            {
                foreach (TaxonRow taxonRow in GetParents())
                {
                    if (taxonRow.Rank == rank)
                    {
                        return taxonRow;
                    }
                }

                return null;
            }

            public bool IsSynonimyAvailable(SpeciesRow spcRow)
            {
                foreach (TaxonRow taxonRow in GetParents())
                {
                    if (taxonRow.Includes(spcRow, false)) return true;
                }
                return false;
            }

            public bool IsSynonimyAvailable(SpeciesRow[] spcRows)
            {
                foreach (SpeciesRow spcRow in spcRows)
                {
                    if (!this.IsSynonimyAvailable(spcRow)) return false;
                }

                return true;
            }


            public override string ToString()
            {
                return Species;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                switch (format.ToLowerInvariant())
                {
                    case "s":
                        return ShortName;

                    case "f":
                        return FullName;

                    case "l":
                        return ScientificName;

                    default:
                        return Species;
                }
            }

            public string ToString(string format)
            {
                return ToString(format, CultureInfo.CurrentCulture);
            }



            public int CompareTo(SpeciesRow other)
            {
                return string.Compare(SortableString, other.SortableString);
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

            public StateRow[] ForestandingTaxonStates
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

            public TaxonRow[] ForestandingTaxon
            {
                get
                {
                    List<TaxonRow> result = new List<TaxonRow>();

                    foreach (TaxonRow taxonRow in ((SpeciesKey)this.Table.DataSet).Taxon)
                    {
                        if (this.DoesLeadTo(taxonRow))
                        {
                            result.Add(taxonRow);
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

            public bool DoesLeadTo(TaxonRow taxonRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.DoesLeadTo(taxonRow);
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

            public bool IsInstantlyOf(TaxonRow taxonRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.TaxonRow == taxonRow;
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

            public bool DoesLeadTo(TaxonRow taxonRow)
            {
                // If related to taxonRow instantly
                if (!this.IsTaxIDNull() && this.TaxonRow == taxonRow) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxonRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(taxonRow);
            }

            public bool DoesLeadTo(SpeciesRow spcRow)
            {
                // If related to taxonRow instantly
                if (!this.IsSpcIDNull() && this.SpeciesRow == spcRow) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxonRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(spcRow);
            }

            public bool DoesLeadTo(StepRow stepRow)
            {
                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                if (this.Next.Equals(stepRow)) return true;

                // If next step follow to taxonRow
                return this.Next.DoesLeadTo(stepRow);
            }

            public bool DoesLeadTo(StateRow stateRow)
            {
                if (this.Equals(stateRow)) return true;

                // If doesn't follow next
                if (this.IsGotoNull()) return false;

                // If next step follow to taxonRow
                return ((SpeciesKey)this.Table.DataSet).Step.FindByID(this.Goto).DoesLeadTo(stateRow);
            }

            public TaxonRow[] ForestandingTaxon
            {
                get
                {
                    List<TaxonRow> result = new List<TaxonRow>();

                    foreach (TaxonRow taxonRow in ((SpeciesKey)this.Table.DataSet).Taxon)
                    {
                        if (this.DoesLeadTo(taxonRow))
                        {
                            result.Add(taxonRow);
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
                        result += this.TaxonRow.TaxonName + ": ";
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
                Report report = new Report(IO.GetFriendlyFiletypeName(UserSettings.Interface.Extension), "key.css");

                if (Species.Count > 0)
                {
                    SpeciesList(report);
                    report.BreakPage();
                }

                report.EndBranded();

                return report;
            }
        }



        public void Read(string filename)
        {
            try
            {
                this.SetAttributable();
                ReadXml(filename);
            }
            catch { Log.Write(EventType.Maintenance, "First call for {0}. File is empty and will be rewritten.", filename); }
        }

        public void SaveToFile(string filename)
        {
            File.WriteAllText(filename, GetXml());
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

                SpeciesRow destRow = key.FindBySpecies(speciesRow.Species);

                if (destRow == null)
                {
                    destRow = key.Species.NewSpeciesRow();
                    destRow.Species = speciesRow.Species;
                    if (!speciesRow.IsDescriptionNull()) destRow.Description = speciesRow.Description;
                    if (!speciesRow.IsReferenceNull()) destRow.Reference = speciesRow.Reference;
                    if (!speciesRow.IsNameNull()) destRow.Name = speciesRow.Name;

                    if (inspect)
                    {
                        EditSpecies addSpecies = new EditSpecies(destRow);

                        if (addSpecies.ShowDialog() == DialogResult.OK)
                        {
                            key.Species.AddSpeciesRow(destRow);

                            //foreach (TaxonRow taxonRow in addSpecies.SelectedTaxon)
                            //{
                            //    key.Rep.AddRepRow(taxonRow, destRow);
                            //}
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
            List<SpeciesRow> result = new List<SpeciesRow>();

            string genus = Genus(speciesRow.Species);

            foreach (SpeciesRow currentSpeciesRow in Species.Rows)
            {
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



        public TaxonRow[] GetRootTaxon()
        {
            List<TaxonRow> result = new List<TaxonRow>();
            foreach (TaxonRow taxonRow in Taxon)
            {
                if (taxonRow.IsTaxIDNull())
                    result.Add(taxonRow);
            }
            result.Sort();
            return result.ToArray();
        }

        public TaxonRow[] GetDerivatedTaxon()
        {
            List<TaxonRow> result = new List<TaxonRow>();
            foreach (TaxonRow taxonRow in Taxon)
            {
                if (taxonRow.HasDerivations) result.Add(taxonRow);
            }
            result.Sort();
            return result.ToArray();
        }

        public TaxonRow[] GetRankedTaxon(int rank)
        {
            List<TaxonRow> result = new List<TaxonRow>();
            foreach (TaxonRow taxonRow in Taxon)
            {
                if (taxonRow.Rank == rank)
                    result.Add(taxonRow);
            }
            result.Sort();
            return result.ToArray();
        }

        public SpeciesRow[] GetVaria(int rank)
        {
            TaxonRow[] rankedTaxons = GetRankedTaxon(rank);
            
            List<SpeciesRow> result = new List<SpeciesRow>();

            foreach (SpeciesRow spcRow in Species)
            {
                if (spcRow.TaxonRow == null)
                {
                    result.Add(spcRow);
                }
                else
                {
                    bool included = false;

                    foreach (TaxonRow taxonRow in rankedTaxons)
                    {
                        if (taxonRow.Includes(spcRow, true))
                        {
                            included = true;
                            break;
                        }
                    }

                    if (!included) result.Add(spcRow);
                }
            }

            result.Sort();
            return result.ToArray();
        }

        public SpeciesRow[] GetSpeciesNamed(string name)
        {
            List<SpeciesRow> result = new List<SpeciesRow>();
            string[] words = name.Split(' ');
            string spcName = words[words.Length - 1];
            if (!spcName.Contains("."))
            {
                foreach (SpeciesRow speciesRow in Species.Rows)
                {
                    if (speciesRow.Species.Contains(spcName))
                    {
                        result.Add(speciesRow);
                    }
                }
            }
            return result.ToArray();
        }

        public SpeciesRow[] GetSpeciesNameContaining(string name)
        {
            List<SpeciesRow> result = new List<SpeciesRow>();

            foreach (SpeciesRow speciesRow in Species.Rows)
            {
                //if (speciesRow.IsSpeciesNull()) continue
                if (speciesRow.FullName.ToUpperInvariant().Contains(name.ToUpperInvariant()))
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
                foreach (SpeciesRow speciesRow in Species.Rows)
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
                foreach (SpeciesRow speciesRow in Species.Rows)
                {
                    string genus = speciesRow.Species.Split(' ')[0];
                    if (!result.Contains(genus)) result.Add(genus);
                }
                return result.ToArray();
            }
        }

        public SpeciesRow FindBySpecies(string value)
        {
            foreach (SpeciesRow speciesRow in Species.Rows)
            {
                if (speciesRow.Species == value)
                {
                    return speciesRow;
                }
            }

            return null;
        }

        public SpeciesRow[] GetSorted()
        {
            return (SpeciesRow[])Species.Select(null, "Species asc");
        }

        public SpeciesRow[] GetOrphans()
        {
            List<SpeciesRow> result = new List<SpeciesRow>();

            foreach (SpeciesRow spcRow in Species)
            {
                if (spcRow.TaxonRow == null)
                    result.Add(spcRow);
            }

            result.Sort();
            return result.ToArray();
        }


        public bool Contains(string species)
        {
            return FindBySpecies(species) != null;
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

            //this.RankName.Columns["Name"].GetStrings(true);

            //Report.Table table1 = new Report.Table();

            //table1.StartHeader();
            //table1.AddHeaderCell("No", .05);
            //table1.AddHeaderCell("Species");
            //for (int i = 0; i < this.Rank.Count; i++)
            //{
            //    table1.AddHeaderCell(this.Rank[i].RankName);
            //}
            //table1.EndHeader();

            //int Counter = 1;

            //foreach (SpeciesRow speciesRow in Species.GetSorted())
            //{
            //    table1.StartRow();
            //    table1.AddCell(Counter);
            //    table1.StartCellOfClass("sp" + speciesRow.ID, speciesRow.FullName);

            //    // Insert Species taxon
            //    //string reps = string.Empty;
            //    //foreach (RepRow repRow in speciesRow.GetRepRows())
            //    //{
            //    //    reps += string.Format("{0} {1}<br>",
            //    //        repRow.TaxonRow.BaseRow.RankName,
            //    //        repRow.TaxonRow.TaxonName);
            //    //}
            //    //report.AddParagraphClass("description", reps);

            //    // Insert species description if exist
            //    //if (!speciesRow.IsDescriptionNull())
            //    //{
            //    //    report.AddParagraphClass("description", speciesRow.Description);
            //    //}
            //    table1.EndCell();


            //    for (int i = 0; i < this.Rank.Count; i++)
            //    {
            //        SpeciesKey.TaxonRow taxonRow = speciesRow.GetTaxon(this.Rank[i]);
            //        table1.AddCell(taxonRow == null ? "" : taxonRow.TaxonName);
            //    }



            //    table1.EndRow();

            //    Counter++;
            //}

            //report.AddTable(table1);
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
