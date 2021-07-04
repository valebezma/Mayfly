using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Mayfly.Wild
{
    public class AquaState// : IFormattable
    {
        public double TemperatureBottom { get; set; }

        public double TemperatureSurface { get; set; }

        public double FlowRate { get; set; }

        public double Limpidity { get; set; }



        public double Conductivity { get; set; }

        public double DissolvedOxygen { get; set; }

        public double OxygenSaturation { get; set; }

        public double pH { get; set; }



        public int Colour { get; set; }

        public OrganolepticState Odor { get; set; }

        public OrganolepticState Sewage { get; set; }

        public OrganolepticState Foam { get; set; }

        public OrganolepticState Turbidity { get; set; }


        public AquaState()
        {
            TemperatureBottom = double.NaN;
            TemperatureSurface = double.NaN;
            FlowRate = double.NaN;
            Limpidity = double.NaN;

            Conductivity = double.NaN;
            DissolvedOxygen = double.NaN;
            OxygenSaturation = double.NaN;
            pH = double.NaN;

            Colour = -1;

            Odor = OrganolepticState.NotInvestigated;
            Sewage = OrganolepticState.NotInvestigated;
            Foam = OrganolepticState.NotInvestigated;
            Turbidity = OrganolepticState.NotInvestigated;
        }

        public AquaState(string physicals, string chemicals, string organoleptics) : this()
        {
            string[] physparameters = physicals.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in physparameters)
            {
                string[] fields = parameter.Split(new char[] { ':' });

                switch (fields[0])
                {
                    case "BTM":
                        TemperatureBottom = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "SRF":
                        TemperatureSurface = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "RAT":
                        FlowRate = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "LMP":
                        Limpidity = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                }
            }

            string[] chemparameters = chemicals.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in chemparameters)
            {
                string[] fields = parameter.Split(new char[] { ':' });

                switch (fields[0])
                {
                    case "CND":
                        Conductivity = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "OXD":
                        DissolvedOxygen = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "OXS":
                        OxygenSaturation = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "PH":
                        pH = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                }
            }

            string[] olparameters = organoleptics.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in olparameters)
            {
                string[] fields = parameter.Split(new char[] { ':' });

                switch (fields[0])
                {
                    case "CLR":
                        Colour = fields[1] == "-" ? -1 :
                            Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "ODR":
                        Odor = fields[1] == "-" ? OrganolepticState.NotInvestigated : 
                            (OrganolepticState)Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "SEW":
                        Sewage = fields[1] == "-" ? OrganolepticState.NotInvestigated : 
                            (OrganolepticState)Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "FOA":
                        Foam = fields[1] == "-" ? OrganolepticState.NotInvestigated : 
                            (OrganolepticState)Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "TRB":
                        Turbidity = fields[1] == "-" ? OrganolepticState.NotInvestigated : 
                            (OrganolepticState)Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                }
            }
        }



        public bool IsPhysicalsAvailable
        {
            get
            {
                if (!IsTemperatureBottomNull()) return true;
                if (!IsTemperatureSurfaceNull()) return true;
                if (!IsFlowRateNull()) return true;
                if (!IsLimpidityNull()) return true;

                return false;
            }
        }

        public bool IsChemicalsAvailable
        {
            get
            {
                if (!IsConductivityNull()) return true;
                if (!IsDissolvedOxygenNull()) return true;
                if (!IsOxygenSaturationNull()) return true;
                if (!IspHNull()) return true;

                return false;
            }
        }

        public bool IsOrganolepticsAvailable
        {
            get
            {
                if (!IsColourNull()) return true;
                if (!IsOdorNull()) return true;
                if (!IsSewageNull()) return true;
                if (!IsFoamNull()) return true;
                if (!IsTurbidityNull()) return true;

                return false;
            }
        }



        public string PhysicalsProtocol
        {
            get
            {
                string result = string.Empty;
                result += "BTM:" + (IsTemperatureBottomNull() ? "-" : TemperatureBottom.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "SRF:" + (IsTemperatureSurfaceNull() ? "-" : TemperatureSurface.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "RAT:" + (IsFlowRateNull() ? "-" : FlowRate.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "LMP:" + (IsLimpidityNull() ? "-" : Limpidity.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                return result;
            }
        }

        public string ChemicalsProtocol
        {
            get
            {
                string result = string.Empty;
                result += "CND:" + (IsConductivityNull() ? "-" : Conductivity.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "OXD:" + (IsDissolvedOxygenNull() ? "-" : DissolvedOxygen.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "OXS:" + (IsOxygenSaturationNull() ? "-" : OxygenSaturation.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "PH:" + (IspHNull() ? "-" : pH.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                return result;
            }
        }

        public string OrganolepticsProtocol
        {
            get
            {
                string result = string.Empty;
                result += "CLR:" + (IsColourNull() ? "-" : Colour.ToString(string.Empty)) + ";";
                result += "ODR:" + (IsOdorNull() ? "-" : ((int)Odor).ToString(string.Empty)) + ";";
                result += "SEW:" + (IsSewageNull() ? "-" : ((int)Sewage).ToString(string.Empty)) + ";";
                result += "FOA:" + (IsFoamNull() ? "-" : ((int)Foam).ToString(string.Empty)) + ";";
                result += "TRB:" + (IsTurbidityNull() ? "-" : ((int)Turbidity).ToString(string.Empty)) + ";";
                return result;
            }
        }



        public bool IsTemperatureBottomNull() { return double.IsNaN(TemperatureBottom); }

        public bool IsTemperatureSurfaceNull() { return double.IsNaN(TemperatureSurface); }

        public bool IsFlowRateNull() { return double.IsNaN(FlowRate); }

        public bool IsLimpidityNull() { return double.IsNaN(Limpidity); }



        public bool IsConductivityNull() { return double.IsNaN(Conductivity); }

        public bool IsDissolvedOxygenNull() { return double.IsNaN(DissolvedOxygen); }

        public bool IsOxygenSaturationNull() { return double.IsNaN(OxygenSaturation); }

        public bool IspHNull() { return double.IsNaN(pH); }



        public bool IsColourNull() { return Colour == -1; }

        public bool IsOdorNull() { return Odor == OrganolepticState.NotInvestigated; }

        public bool IsSewageNull() { return Sewage == OrganolepticState.NotInvestigated; }

        public bool IsFoamNull() { return Foam == OrganolepticState.NotInvestigated; }

        public bool IsTurbidityNull() { return Turbidity == OrganolepticState.NotInvestigated; }


        public void SetOrganoleptics(OrganolepticState odor, OrganolepticState sewage, OrganolepticState foam, OrganolepticState turbidity)
        {
            Odor = odor;
            Sewage = sewage;
            Foam = foam;
            Turbidity = turbidity;
        }


        public Report.Table GetReport()
        {
            Report.Table table1 = new Report.Table(Resources.Reports.Header.AquaState);

            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Controls.AquaControl));

            table1.StartRow();
            if (this.IsTemperatureBottomNull())
            {
                table1.AddCellPrompt(resources.GetString("labelTemperatureBottom.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelTemperatureBottom.Text"), this.TemperatureBottom, 2);
            }
            if (this.IsTemperatureSurfaceNull())
            {
                table1.AddCellPrompt(resources.GetString("labelTemperatureSurface.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelTemperatureSurface.Text"), this.TemperatureSurface, 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsFlowRateNull())
            {
                table1.AddCellPrompt(resources.GetString("labelFlowRate.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelFlowRate.Text"), this.FlowRate, 2);
            }
            if (this.IsLimpidityNull())
            {
                table1.AddCellPrompt(resources.GetString("labelLimpidity.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelLimpidity.Text"), this.Limpidity, 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsOdorNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxOdor.Text"));
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("checkBoxOdor.Text"),
                    this.Odor == OrganolepticState.Present ? Constants.Check : Constants.Negative);
            }
            if (this.IsSewageNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxSewage.Text"));
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("checkBoxSewage.Text"),
                    this.Sewage == OrganolepticState.Present ? Constants.Check : Constants.Negative);
            }
            if (this.IsFoamNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxFoam.Text"));
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("checkBoxFoam.Text"),
                    this.Foam == OrganolepticState.Present ? Constants.Check : Constants.Negative);
            }
            if (this.IsTurbidityNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxTurbidity.Text"));
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("checkBoxTurbidity.Text"),
                    this.Turbidity == OrganolepticState.Present ? Constants.Check : Constants.Negative);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsColourNull())
            {
                table1.AddCellPrompt(resources.GetString("labelColour.Text"), Constants.Null, 4);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelColour.Text"),
                    Service.WaterColorName(this.Colour), 4);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsConductivityNull())
            {
                table1.AddCellPrompt(resources.GetString("labelConductivity.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelConductivity.Text"), this.Conductivity, 2);
            }
            if (this.IsDissolvedOxygenNull())
            {
                table1.AddCellPrompt(resources.GetString("labelDissolvedOxygen.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelDissolvedOxygen.Text"), this.DissolvedOxygen, 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IspHNull())
            {
                table1.AddCellPrompt(resources.GetString("labelpH.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelpH.Text"), this.pH, 2);
            }
            if (this.IsOxygenSaturationNull())
            {
                table1.AddCellPrompt(resources.GetString("labelOxygenSaturation.Text"), Constants.Null, 2);
            }
            else
            {
                table1.AddCellPrompt(resources.GetString("labelOxygenSaturation.Text"), this.OxygenSaturation, 2);
            }
            table1.EndRow();

            return table1;
        }
    }

    public enum OrganolepticState
    {
        Absent = 0,
        Present = 1,
        NotInvestigated = 2
    }
}