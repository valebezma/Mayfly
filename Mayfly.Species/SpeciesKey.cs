using Mayfly.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Mayfly.Species
{
    public partial class SpeciesKey
    {
        partial class TaxonRow : IComparable<TaxonRow>, IFormattable
        {
            public TaxonomicRank TaxonomicRank
            {
                get { return new TaxonomicRank(Rank); }
                set { Rank = value.Value; }
            }

            

            public TaxonRow[] Hierarchy
            {
                get
                {
                    List<TaxonRow> result = new List<TaxonRow>();

                    TaxonRow parent = this.TaxonRowParent;

                    while (parent != null)
                    {
                        result.Insert(0, parent);
                        parent = parent.TaxonRowParent;
                    }

                    return result.ToArray();
                }
            }

            public string NameReport
            {
                get
                {
                    string result = string.Format("<span class='latin'>{0}</span>", Name);

                    foreach (string serviceWord in new string[] { "gr.", "morpha", "ex gr.", "nov.", "sp.", "sp. n." })
                    {
                        result = result.Replace(" " + serviceWord + " ",
                            "<span class='latin-inside'> " + serviceWord + " </span>");
                    }

                    return string.IsNullOrWhiteSpace(result) ? Resources.Interface.UnidentifiedTitle : result;
                }
            }

            public string CommonName
            {
                get
                {
                    return IsLocalNull() ? Name : Local.GetLocalizedValue();
                }
            }

            public string SevereName
            {
                get
                {
                    return IsHigher ? 
                        string.Format("{0} {1}", this?.TaxonomicRank.Name, Name) :
                        Name;
                }
            }

            public string FullName
            {
                get
                {
                    return IsHigher ? 
                        string.Format("{0} {1}{2}", this?.TaxonomicRank.Name, Name, IsLocalNull() ? string.Empty : " (" + Local.GetLocalizedValue() + ")") :
                        string.Format("{0} {1} {2}", (IsLocalNull() ? string.Empty : Local.GetLocalizedValue()), Name, IsReferenceNull() ? string.Empty : Reference).Trim();
                }
            }

            public string FullNameReport
            {
                get
                {
                    return string.Format("{0} {1} {2}",
                        IsLocalNull() ? string.Empty : Local.GetLocalizedValue(),
                        NameReport,
                        IsReferenceNull() ? string.Empty : Reference).Trim();
                }
            }

            public string InterfaceString
            {
                get
                {
                    //return FullName;
                    int wealth = this.GetSpeciesRows(true).Length;
                    return wealth > 0 ? string.Format("{0} ({1})", this.SevereName, wealth) : this.SevereName;
                }
            }

            public string SortableString
            {
                get
                {
                    string result = string.Empty;

                    foreach (TaxonRow parent in Hierarchy)
                    {
                        result += string.Format("{0:000} {1:000} ",
                            parent.Rank, (parent.IsIndexNull() ? 999 : parent.Index));
                    }

                    return result + string.Format("{0:000} {1:000}",
                        Rank, (IsIndexNull() ? 999 : Index));
                }
            }



            public bool HasChildren
            {
                get
                {
                    return this.GetTaxonRows().Length > 0;
                }
            }

            //public bool IsSpecies
            //{
            //    get { return Rank >= 90; }
            //}

            public bool IsHigher
            {
                get { return Rank < 90; }
            }

            public bool IsValid
            {
                get
                {
                    return IsTaxIDNull() || (TaxonRowParent.Rank < Rank); // If parent - higher taxon - return itself
                }
            }

            public TaxonRow HigherParent
            {
                get
                {
                    if (ValidRecord.IsTaxIDNull()) return null;
                    TaxonRow parent = ValidRecord.TaxonRowParent;
                    while (!parent.IsTaxIDNull() && !parent.IsHigher) parent = parent.TaxonRowParent;
                    return parent.IsHigher ? parent : null;
                }
            }

            public TaxonRow ValidRecord
            {
                get
                {
                    if (IsValid) return this;
                    return TaxonRowParent.ValidRecord; // return ValidSpecies for superior species row
                }
            }

            public TaxonRow MajorSynonym
            {
                get
                {
                    return IsTaxIDNull() ? null : (TaxonRowParent.Rank == Rank ? TaxonRowParent : null);
                }
            }

            public TaxonRow[] MinorSynonyms
            {
                get
                {
                    List<TaxonRow> result = new List<TaxonRow>();

                    foreach (TaxonRow taxonRow in GetTaxonRows())
                    {
                        if (taxonRow.Rank == this.Rank) result.Add(taxonRow);
                    }

                    return result.ToArray();
                }
            }

            public TaxonRow[] Synonyms
            {
                get
                {
                    List<TaxonRow> result = new List<TaxonRow>();
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

                    foreach (TaxonRow tr in Hierarchy)
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



            public TaxonRow GetParentTaxon(TaxonomicRank rank)
            {
                foreach (TaxonRow taxonRow in Hierarchy)
                {
                    if (taxonRow.Rank == rank.Value)
                    {
                        return taxonRow;
                    }
                }

                return null;
            }

            public TaxonRow[] GetAllTaxonRows()
            {
                List<TaxonRow> result = new List<TaxonRow>();

                foreach (TaxonRow taxonRow in GetTaxonRows())
                {
                    result.Add(taxonRow);
                    result.AddRange(taxonRow.GetAllTaxonRows());
                }

                return result.ToArray();
            }

            public bool Includes(TaxonRow taxonRow, bool searchChildren)
            {
                return Array.IndexOf(searchChildren ? GetAllTaxonRows() : GetTaxonRows(), taxonRow) > -1;
            }

            public TaxonRow[] GetSpeciesRows(bool searchChildren)
            {
                List<TaxonRow> result = new List<TaxonRow>();

                foreach (TaxonRow taxonRow in searchChildren ? GetAllTaxonRows() : GetTaxonRows())
                {
                    if (!taxonRow.IsValid) continue;
                    if (!taxonRow.IsHigher) result.Add(taxonRow);
                }

                return result.ToArray();
            }

            public bool IsSynonymyAvailable(TaxonRow taxonRow)
            {
                //if (taxonRow.IsHigher) return false;

                foreach (TaxonRow parentTaxonRow in Hierarchy) {
                    if (parentTaxonRow.Includes(taxonRow.ValidRecord, false)) return true;
                }

                return false;
            }

            public bool IsSynonymyAvailable(TaxonRow[] taxonRows)
            {
                foreach (TaxonRow taxonRow in taxonRows)
                {
                    if (!this.IsSynonymyAvailable(taxonRow)) return false;
                }

                return true;
            }

            public bool Validate(string name)
            {
                if (Name == name) return true;

                foreach (TaxonRow minorSynonym in MinorSynonyms)
                {
                    if (minorSynonym.Validate(name)) return true;
                }

                return false;
            }

            public string GetFullPath(string format)
            {
                return Hierarchy.Merge(" > ", format);
            }


            public int CompareTo(TaxonRow other)
            {
                if (!IsHigher && !other.IsHigher) return Name.CompareTo(other.Name);
                else return this.SortableString.CompareTo(other.SortableString);
            }


            public override string ToString()
            {
                return Name;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                if (string.IsNullOrEmpty(format)) format = string.Empty;

                switch (format.ToLowerInvariant())
                {
                    case "c":
                        return CommonName;

                    case "s":
                        return SevereName;

                    case "f":
                        return FullName;

                    case "i":
                        return InterfaceString;

                    default:
                        return Name;
                }
            }

            public string ToString(string format)
            {
                return ToString(format, CultureInfo.CurrentCulture);
            }
        }

        partial class TaxonDataTable
        {
            public TaxonRow NewTaxonRow(TaxonomicRank rank, string name)
            {
                TaxonRow result = NewTaxonRow();
                result.Rank = rank.Value;
                result.Name = name;
                return result;
            }

            public TaxonRow NewSpeciesRow(string name)
            {
                return NewTaxonRow(TaxonomicRank.Species, name);
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

            public bool DoesLeadTo(TaxonRow taxonRow)
            {
                bool result = false;

                foreach (StateRow stateRow in this.AvailableStates)
                {
                    result |= stateRow.DoesLeadTo(taxonRow);
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

            public string ShortDescription
            {
                get
                {
                    string result = string.Empty;

                    if (!this.IsTaxIDNull())
                    {
                        result += this.TaxonRow.FullName + ": ";
                    }

                    if (this.IsGotoNull())
                    {
                        if (this.IsTaxIDNull())
                        {
                            result += "Final State";
                        }
                    }
                    else
                    {
                        result += "To step " + Goto;
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

                if (GetSpeciesRows().Length > 0)
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

            foreach (TaxonRow speciesRow in GetSpeciesRows())
            {
                if (speciesRow.Name.EndsWith("sp.")) continue;

                TaxonRow destRow = key.FindBySpecies(speciesRow.Name);

                if (destRow == null)
                {
                    destRow = key.Taxon.NewTaxonRow(speciesRow.TaxonomicRank, speciesRow.Name);
                    if (!speciesRow.IsDescriptionNull()) destRow.Description = speciesRow.Description;
                    if (!speciesRow.IsReferenceNull()) destRow.Reference = speciesRow.Reference;
                    if (!speciesRow.IsLocalNull()) destRow.Local = speciesRow.Local;

                    if (inspect)
                    {
                        EditTaxon addSpecies = new EditTaxon(destRow);

                        if (addSpecies.ShowDialog() == DialogResult.OK)
                        {
                            key.Taxon.AddTaxonRow(destRow);
                        }
                    }
                    else
                    {
                        key.Taxon.AddTaxonRow(destRow);
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

        //public SpeciesRow[] CloseRelatives(SpeciesRow speciesRow)
        //{
        //    List<SpeciesRow> result = new List<SpeciesRow>();

        //    string genus = Genus(speciesRow.Name);

        //    foreach (SpeciesRow currentSpeciesRow in Species.Rows)
        //    {
        //        if (Genus(currentSpeciesRow.Species) == genus &&
        //            !result.Contains(currentSpeciesRow))
        //        {
        //            result.Add(currentSpeciesRow);
        //        }
        //    }

        //    return result.ToArray();
        //}

        public static string SpecificName(string species)
        {
            string[] values = species.GetLongWords(4);

            if (values.Length > 1)
            {
                return values[values.Length -1];
            }
            else
            {
                return null;
            }
        }



        public TaxonRow[] GetTaxonRows(bool rootOnly, bool derivatedOnly, bool populatedOnly, bool higherOnly, TaxonomicRank rank)
        {
            if (higherOnly && rank != null && rank.Value > 89)
                throw new ArgumentException("Rank specified are misfit higherOnly argument");

            List<TaxonRow> result = new List<TaxonRow>();

            foreach (TaxonRow taxonRow in Taxon)
            {
                if (rootOnly && !taxonRow.IsTaxIDNull()) continue;
                if (derivatedOnly && !taxonRow.HasChildren) continue;
                if (populatedOnly && taxonRow.GetSpeciesRows(false).Length == 0) continue;
                if (higherOnly && !taxonRow.IsHigher) continue;
                if (rank != null && taxonRow.Rank != rank.Value) continue;

                result.Add(taxonRow);
            }
            result.Sort();
            return result.ToArray();
        }

        public TaxonRow[] GetHigherTaxonRows(bool populatedOnly)
        {
            return GetTaxonRows(false, false, populatedOnly, true, null);
        }

        public TaxonRow[] GetRootTaxonRows()
        {
            return GetTaxonRows(true, false, false, false, null);
        }

        public TaxonRow[] GetRootHigherTaxonRows()
        {
            return GetTaxonRows(true, false, false, true, null);
        }

        public TaxonRow[] GetRootSpeciesRows()
        {
            return GetTaxonRows(true, false, false, false, TaxonomicRank.Species);
        }

        public TaxonRow[] GetDerivatedTaxonRows()
        {
            return GetTaxonRows(false, true, false, false, null);
        }

        public TaxonRow[] GetTaxonRows(TaxonomicRank rank)
        {
            return GetTaxonRows(false, false, false, false, rank);
        }

        public TaxonRow[] GetSpeciesRows()
        {
            return GetTaxonRows(false, false, false, false, TaxonomicRank.Species);
        }



        public TaxonRow[] GetVaria(TaxonomicRank rank)
        {
            TaxonRow[] rankedTaxons = GetTaxonRows(rank);

            List<TaxonRow> result = new List<TaxonRow>();

            foreach (TaxonRow spcRow in GetSpeciesRows())
            {
                if (spcRow.TaxonRowParent == null)
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

        public TaxonRow[] GetSpeciesNamed(string name)
        {
            List<TaxonRow> result = new List<TaxonRow>();
            string[] words = name.Split(' ');
            string spcName = words[words.Length - 1];
            if (!spcName.Contains("."))
            {
                foreach (TaxonRow speciesRow in GetSpeciesRows())
                {
                    if (speciesRow.Name.Contains(spcName))
                    {
                        result.Add(speciesRow);
                    }
                }
            }
            return result.ToArray();
        }

        public TaxonRow[] GetSpeciesNameContaining(string name)
        {
            List<TaxonRow> result = new List<TaxonRow>();

            foreach (TaxonRow speciesRow in GetSpeciesRows())
            {
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
                foreach (TaxonRow taxonRow in Taxon)
                {
                    if (!taxonRow.IsReferenceNull())
                    {
                        if (!result.Contains(taxonRow.Reference)) result.Add(taxonRow.Reference);
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
                foreach (TaxonRow speciesRow in GetSpeciesRows())
                {
                    string genus = speciesRow.Name.Split(' ')[0];
                    if (!result.Contains(genus)) result.Add(genus);
                }
                return result.ToArray();
            }
        }

        public int ValidSpeciesCount
        {
            get
            {
                int result = 0;

                foreach (TaxonRow speciesRow in GetSpeciesRows())
                {
                    if (speciesRow.IsTaxIDNull() || speciesRow.TaxonRowParent.IsHigher)
                    {
                        result++;
                    }
                }

                return result;
            }
        }

        public int AllSpeciesCount
        {
            get
            {
                return GetSpeciesRows().Length;
            }
        }


        public int HigherTaxonCount
        {
            get
            {
                int result = 0;

                foreach (TaxonRow taxonRow in Taxon)
                {
                    if (taxonRow.IsHigher)
                    {
                        result++;
                    }
                }

                return result;
            }
        }

        public TaxonRow FindBySpecies(string value)
        {
            foreach (TaxonRow speciesRow in GetSpeciesRows())
            {
                if (speciesRow.Name == value)
                {
                    return speciesRow;
                }
            }

            return null;
        }


        public bool Contains(string species)
        {
            return FindBySpecies(species) != null;
        }

        public TaxonRow[] GetByFeatureStates(StateRow[] stateRows)
        {
            List<TaxonRow> result = new List<TaxonRow>();

            foreach (TaxonRow speciesRow in GetSpeciesRows())
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
