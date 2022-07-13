using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;

namespace Mayfly.Benthos
{
    public class SubstrateSample
    {
        public double Boulder { get; set; }

        public double Gravel { get; set; }

        public double Cobble { get; set; }

        public double Sand { get; set; }

        public double Silt { get; set; }

        public double Clay { get; set; }



        public double Phytal { get; set; }

        public double Living { get; set; }

        public double Wood { get; set; }

        public double Sapropel { get; set; }

        public double Debris { get; set; }

        public double CPOM { get; set; }

        public double FPOM { get; set; }


        public bool IsAvailable
        {
            get
            {
                if (!IsBoulderNull()) return true;
                if (!IsGravelNull()) return true;
                if (!IsCobbleNull()) return true;
                if (!IsSandNull()) return true;
                if (!IsSiltNull()) return true;
                if (!IsClayNull()) return true;

                if (!IsPhytalNull()) return true;
                if (!IsLivingNull()) return true;
                if (!IsWoodNull()) return true;
                if (!IsDebrisNull()) return true;
                if (!IsSapropelNull()) return true;
                if (!IsFPOMNull()) return true;
                if (!IsCPOMNull()) return true;

                return false;
            }
        }

        public string Protocol
        {
            get
            {
                string result = string.Empty;

                result += "BLD:" + (IsBoulderNull() ? "-" : Boulder.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "GRV:" + (IsGravelNull() ? "-" : Gravel.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "CBL:" + (IsCobbleNull() ? "-" : Cobble.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "SND:" + (IsSandNull() ? "-" : Sand.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "SLT:" + (IsSiltNull() ? "-" : Silt.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "CLY:" + (IsClayNull() ? "-" : Clay.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";

                result += "PHT:" + (IsPhytalNull() ? "-" : Phytal.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "LIV:" + (IsLivingNull() ? "-" : Living.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "WOD:" + (IsWoodNull() ? "-" : Wood.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "SPR:" + (IsSapropelNull() ? "-" : Sapropel.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "DBR:" + (IsDebrisNull() ? "-" : Debris.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "COM:" + (IsCPOMNull() ? "-" : CPOM.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
                result += "FOM:" + (IsFPOMNull() ? "-" : FPOM.ToString(string.Empty, CultureInfo.InvariantCulture)) + ";";
               
                return result;
            }
        }



        public SubstrateType Type 
        {
            get
            {
                return Ferre(Sand, Silt, Clay);
            }
        }

        public string TypeName 
        {
            get
            {
                return TypeTrivia(Type);
            }
        }



        public SubstrateSample() {

            Boulder = double.NaN;
            Gravel = double.NaN;
            Cobble = double.NaN;
            Sand = double.NaN;
            Silt = double.NaN;
            Clay = double.NaN;
            Phytal = double.NaN;
            Living = double.NaN;
            Wood = double.NaN;
            Sapropel = double.NaN;
            Debris = double.NaN;
            CPOM = double.NaN;
            FPOM = double.NaN;
        }

        public SubstrateSample(double sand, double silt, double clay) : this() {

            double s = sand + silt + clay;

            Sand = sand / s;
            Silt = silt / s;
            Clay = clay / s;
        }

        public SubstrateSample(SubstrateType type) : this()
        {
            switch (type)
            {
                case SubstrateType.Sand:
                    Sand = .92;
                    Silt = .04;
                    Clay = .04;
                    break;
                case SubstrateType.LoamySand:
                    Sand = .83;
                    Silt = .11;
                    Clay = .06;
                    break;
                case SubstrateType.SandyLoam:
                    Sand = .66;
                    Silt = .22;
                    Clay = .12;
                    break;
                case SubstrateType.SandyClayLoam:
                    Sand = .60;
                    Silt = .12;
                    Clay = .28;
                    break;
                case SubstrateType.Loam:
                    Sand = .41;
                    Silt = .39;
                    Clay = .20;
                    break;
                case SubstrateType.SiltLoam:
                    Sand = .22;
                    Silt = .65;
                    Clay = .13;
                    break;
                case SubstrateType.Silt:
                    Sand = .06;
                    Silt = .88;
                    Clay = .06;
                    break;
                case SubstrateType.SandyClay:
                    Sand = .52;
                    Silt = .06;
                    Clay = .42;
                    break;
                case SubstrateType.ClayLoam:
                    Sand = .33;
                    Silt = .33;
                    Clay = .34;
                    break;
                case SubstrateType.SiltyClayLoam:
                    Sand = .10;
                    Silt = .56;
                    Clay = .34;
                    break;
                case SubstrateType.SiltyClay:
                    Sand = .06;
                    Silt = .46;
                    Clay = .48;
                    break;
                case SubstrateType.Clay:
                    Sand = .18;
                    Silt = .18;
                    Clay = .64;
                    break;
            }
        }

        public SubstrateSample(string protocolized) : this()
        {
            if (string.IsNullOrWhiteSpace(protocolized)) return;

            string[] parameters = protocolized.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                string[] fields = parameter.Split(new char[] { ':' });

                switch (fields[0])
                {
                    case "BLD":
                        Boulder = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "GRV":
                        Gravel = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "CBL":
                        Cobble = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "SND":
                        Sand = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "SLT":
                        Silt = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "CLY":
                        Clay = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "PHT":
                        Phytal = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "LIV":
                        Living = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "WOD":
                        Wood = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "SPR":
                        Sapropel = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "DBR":
                        Debris = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "COM":
                        CPOM = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "FOM":
                        FPOM = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                }
            }
        }
        


        public bool IsBoulderNull() { return  double.IsNaN(Boulder); }

        public bool IsGravelNull() { return  double.IsNaN(Gravel); }

        public bool IsCobbleNull() { return  double.IsNaN(Cobble); }

        public bool IsSandNull() { return  double.IsNaN(Sand); }

        public bool IsSiltNull() { return  double.IsNaN(Silt); }

        public bool IsClayNull() { return  double.IsNaN(Clay); }



        public bool IsPhytalNull() { return  double.IsNaN(Phytal); }

        public bool IsLivingNull() { return  double.IsNaN(Living); }

        public bool IsWoodNull() { return  double.IsNaN(Wood); }

        public bool IsSapropelNull() { return  double.IsNaN(Sapropel); }

        public bool IsDebrisNull() { return  double.IsNaN(Debris); }

        public bool IsCPOMNull() { return  double.IsNaN(CPOM); }

        public bool IsFPOMNull() { return  double.IsNaN(FPOM); }



        public Report.Table GetReport()
        {
            ResourceManager resources = new ResourceManager(typeof(BenthosCard));

            Report.Table table1 = new Report.Table(resources.GetString("tabPageSubstrate.Text"));

            table1.StartRow();
            if (this.IsBoulderNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxBoulder.Text"));
            }
            else
            {
                if (this.Boulder == 0) table1.AddCellPrompt(resources.GetString("checkBoxBoulder.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxBoulder.Text"), this.Boulder + "%");
            }

            if (this.IsCobbleNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxCobble.Text"));
            }
            else
            {
                if (this.Cobble == 0) table1.AddCellPrompt(resources.GetString("checkBoxCobble.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxCobble.Text"), this.Cobble + "%");
            }

            if (this.IsGravelNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxGravel.Text"));
            }
            else
            {
                if (this.Gravel == 0) table1.AddCellPrompt(resources.GetString("checkBoxGravel.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxGravel.Text"), this.Gravel + "%");
            }

            if (this.IsSandNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxSand.Text"));
            }
            else
            {
                if (this.Sand == 0) table1.AddCellPrompt(resources.GetString("checkBoxSand.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxSand.Text"), this.Sand + "%");
            }

            if (this.IsSiltNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxSilt.Text"));
            }
            else
            {
                if (this.Silt == 0) table1.AddCellPrompt(resources.GetString("checkBoxSilt.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxSilt.Text"), this.Silt + "%");
            }

            if (this.IsClayNull())
            {
                table1.AddCellPromptEmpty(resources.GetString("checkBoxClay.Text"));
            }
            else
            {
                if (this.Clay == 0) table1.AddCellPrompt(resources.GetString("checkBoxClay.Text"), Constants.Check);
                else table1.AddCellPrompt(resources.GetString("checkBoxClay.Text"), this.Clay + "%");
            }
            table1.EndRow();

            table1.StartRow();
            table1.AddCellPrompt(Resources.Reports.Card.Substrate_1, string.IsNullOrEmpty(this.TypeName) ? Constants.Null : this.TypeName, 6);
            table1.EndRow();

            table1.StartRow();
            if (this.IsPhytalNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxPhytal.Text"), Constants.Null, 3);
            }
            else
            {
                if (this.Phytal == 0) table1.AddCellPrompt(resources.GetString("checkBoxPhytal.Text"), Constants.Check, 3);
                else table1.AddCellPrompt(resources.GetString("checkBoxPhytal.Text"), this.Phytal + "%", 3);
            }

            if (this.IsLivingNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxLiving.Text"), Constants.Null, 3);
            }
            else
            {
                if (this.Living == 0) table1.AddCellPrompt(resources.GetString("checkBoxLiving.Text"), Constants.Check, 3);
                else table1.AddCellPrompt(resources.GetString("checkBoxLiving.Text"), this.Living + "%", 3);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsWoodNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxWood.Text"), Constants.Null, 2);
            }
            else
            {
                if (this.Wood == 0) table1.AddCellPrompt(resources.GetString("checkBoxWood.Text"), Constants.Check, 2);
                else table1.AddCellPrompt(resources.GetString("checkBoxWood.Text"), this.Wood + "%", 2);
            }

            if (this.IsCPOMNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxCPOM.Text"), Constants.Null, 2);
            }
            else
            {
                if (this.CPOM == 0) table1.AddCellPrompt(resources.GetString("checkBoxCPOM.Text"), Constants.Check, 2);
                else table1.AddCellPrompt(resources.GetString("checkBoxCPOM.Text"), this.CPOM + "%", 2);
            }

            if (this.IsFPOMNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxFPOM.Text"), Constants.Null, 2);
            }
            else
            {
                if (this.FPOM == 0) table1.AddCellPrompt(resources.GetString("checkBoxFPOM.Text"), Constants.Check, 2);
                else table1.AddCellPrompt(resources.GetString("checkBoxFPOM.Text"), this.FPOM + "%", 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsSapropelNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxSapropel.Text"), Constants.Null, 2);
            }
            else
            {
                if (this.Sapropel == 0) table1.AddCellPrompt(resources.GetString("checkBoxSapropel.Text"), Constants.Check, 2);
                else table1.AddCellPrompt(resources.GetString("checkBoxSapropel.Text"), this.Sapropel + "%", 2);
            }

            if (this.IsDebrisNull())
            {
                table1.AddCellPrompt(resources.GetString("checkBoxDebris.Text"), Constants.Null, 4);
            }
            else
            {
                if (this.Debris == 0) table1.AddCellPrompt(resources.GetString("checkBoxDebris.Text"), Constants.Check, 4);
                else table1.AddCellPrompt(resources.GetString("checkBoxDebris.Text"), this.Debris + "%", 4);
            }
            table1.EndRow();

            return table1;
        }



        public static SubstrateType Ferre(double sand, double silt, double clay)
        {
            if (double.IsNaN(sand)) sand = 0;
            if (double.IsNaN(silt)) silt = 0;
            if (double.IsNaN(clay)) clay = 0;

            double total = sand + silt + clay;
            sand /= total;
            silt /= total;
            clay /= total;

            if (sand > 0.85 &&
                (clay <= (0.666 * sand - 0.566)))
            {
                return SubstrateType.Sand;
            }

            if (clay <= (0.5 * sand - 0.35))
            {
                return SubstrateType.LoamySand;
            }

            if (silt > 0.275 &&
                silt < 0.50 &&
                clay > 0.075 &&
                clay < 0.275 &&
                sand < 0.525)
            {
                return SubstrateType.Loam;
            }

            if (silt < 0.5 &&
                clay < 0.2)
            {
                return SubstrateType.SandyLoam;
            }

            if (silt > 0.8 &&
                clay < 0.875)
            {
                return SubstrateType.Silt;
            }

            if (silt > 0.5 &&
                clay < 0.275)
            {
                return SubstrateType.SiltLoam;
            }

            if (clay > 0.2 &&
                clay < 0.35 &&
                silt < 0.275 &&
                sand > 0.45)
            {
                return SubstrateType.SandyClayLoam;
            }

            if (clay > 0.35 &&
                sand > 0.45)
            {
                return SubstrateType.SandyClay;
            }

            if (clay > 0.275 &&
                clay < 0.4 &&
                sand > 0.2 &&
                sand < 0.45)
            {
                return SubstrateType.ClayLoam;
            }

            if (clay > 0.275 &&
                clay < 0.4 &&
                sand < 0.2)
            {
                return SubstrateType.SiltyClayLoam;
            }

            if (silt > 0.4 &&
                clay > 0.4)
            {
                return SubstrateType.SiltyClay;
            }

            if (clay > 0.4 &&
                silt < 0.4 &&
                sand < 0.45)
            {
                return SubstrateType.Clay;
            }

            return SubstrateType.None;
        }

        public static SubstrateType FromName(string textureName)
        {
            return (SubstrateType)Enum.Parse(System.Type.GetType("Mayfly.Benthos.SubstrateType"), textureName);
        }

        public static string TypeTrivia(SubstrateType type)
        {
            ResourceManager resources = new ResourceManager(typeof(BenthosCard));
            return resources.GetString("ItemSub" + type.ToString() + ".Text");
        }
    }

    public enum SubstrateType
    {
        None,
        Sand,
        LoamySand,
        SandyLoam,
        SandyClayLoam,
        Loam,
        SiltLoam,
        SandyClay,
        ClayLoam,
        Silt,
        SiltyClayLoam,
        SiltyClay,
        Clay
    }
}
