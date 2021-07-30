using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Wild
{
    public class Category : IComparable
    {
        public string Name { get; set; }

        double index;

        public double Index
        {
            get { return index; }

            set
            {
                index = value;
                this.Abundance = this.Quantity / value; // Math.Round(this.Quantity / value, 0);
                this.Biomass = this.Mass / value;
            }
        }



        public int SamplesCount { get; set; }

        public Composition Parent { get; internal set; }

        // Natural

        public int Quantity { get; set; }

        public double Mass { get; set; }

        // Relative

        public double Abundance { get; set; }

        public double Biomass { get; set; }

        // Fractions

        public double Juveniles { get; private set; }

        public double Males { get; private set; }

        public double Females { get; private set; }

        // Samples

        public Sample AbundanceSample { get; set; }

        public Sample LengthSample { get; set; }

        public Sample MassSample { get; set; }



        public Category()
        {
            Name = string.Empty;
            MassSample = new Sample();
            LengthSample = new Sample();
            //Biomass = Abundance = Juveniles = Males = Females = double.NaN;
        }

        public Category(string name)
            : this()
        {
            Name = name;
        }

        public Category(string name, int quantity, double mass)
            : this(name)
        {
            Quantity = quantity;
            Mass = mass;
        }



        public virtual Category GetEmptyCopy()
        {
            return new Category(this.Name);
        }

        public void Reset()
        {
            Quantity = 0;

            Mass =
            Juveniles =
            Males =
            Females =
            0;
        }

        public void SetSexualComposition(double juveniles, double males, double females)
        {
            double total = juveniles + males + females;

            Juveniles = juveniles / total;
            Males = males / total;
            Females = females / total;
        }

        public void SetSexualComposition(int juveniles, int males, int females)
        {
            int total = juveniles + males + females;

            Juveniles = (double)juveniles / (double)total;
            Males = (double)males / (double)total;
            Females = (double)females / (double)total;
        }

        public string GetSexualComposition()
        {
            return double.IsNaN(Juveniles + Males + Females) ? Constants.Null : string.Format("{0:P0} : {1:P0} : {2:P0}", Juveniles, Males, Females);
            //return double.IsNaN(Juveniles + Males + Females) ? Constants.Null : string.Format("{0:P0} ← {1:P0} → {2:P0}", Males, Juveniles, Females);
        }

        public override string ToString()
        {
            return Name +": " + (Quantity > 0 ? string.Format("{0} inds. / {1:N3} inds./UE ({2:P1})", Quantity, Abundance, AbundanceFraction) : "None");
        }

        public static ValueVariant GetValueVariant(bool quantitative, bool pereffort, bool fraction)
        {
            if (fraction)
            {
                if (quantitative) return ValueVariant.AbundanceFraction;
                else return ValueVariant.BiomassFraction;
            }
            else if (pereffort)
            {
                if (quantitative) return ValueVariant.Abundance;
                else return ValueVariant.Biomass;
            }
            else
            {
                if (quantitative) return ValueVariant.Quantity;
                else return ValueVariant.Mass;
            }
        }

        public object GetValue(ValueVariant vv)
        {
            object value = null;

            switch (vv)
            {
                case ValueVariant.Quantity:
                    value = this.Quantity;
                    break;

                case ValueVariant.Mass:
                    value = this.Mass;
                    break;

                case ValueVariant.Abundance:
                    value = this.Abundance;
                    break;

                case ValueVariant.Biomass:
                    value = this.Biomass;
                    break;

                case ValueVariant.AbundanceFraction:
                    value = this.AbundanceFraction;
                    break;

                case ValueVariant.BiomassFraction:
                    value = this.BiomassFraction;
                    break;

                case ValueVariant.Occurrence:
                    value = this.Occurrence;
                    break;

                case ValueVariant.Dominance:
                    value = this.Dominance;
                    break;
            }

            return value;
        }





        public double AbundanceFraction
        {
            get
            {
                return this.Abundance == 0 ? 0 : this.Abundance / Parent.TotalAbundance;
            }
        }

        public double BiomassFraction
        {
            get
            {
                return this.Biomass == 0 ? 0 : this.Biomass / Parent.TotalBiomass;
            }
        }



        public double DensityIndex
        {
            get
            {
                return Math.Sqrt(this.Abundance * this.Biomass);
            }
        }

        public double Dominance
        {
            get
            {
                switch (Wild.UserSettings.Dominance)
                {
                    case 0:
                        return this.De_Vries_1937();
                    case 1:
                        return this.Zenkevich_Brotskaya_1937();
                    case 2:
                        return this.Zenkevich_Brotskaya_1939();
                    case 3:
                        return this.Mordukhai_Boltovskii_1940();
                    case 4:
                        return this.Arnoldi_1941();
                    case 5:
                        return this.Mordukhai_Boltovskii_1948();
                    case 6:
                        return this.Balog_1958();
                    case 7:
                        return this.Sanders_1960();
                    case 8: // (Sanders, 1960) in scale of 100%
                        return this.Sanders_1960() / (double)Parent.SamplesCount / 10;
                    case 9:
                        return this.Petrov_1961();
                    case 10:
                        return this.Kownacki_1971();
                    case 11:
                        return this.Kownacki_1971_B();
                    case 12:
                        return this.Mordukhai_Boltovskii_1975();
                    case 13:
                        return this.Iogansen_Faizova_1978();
                    case 14:
                        return this.Dedyu_1990();
                    case 15:
                        return this.Shcherbina_1993();
                    case 16:
                        return this.Shitikov_et_al_2003();
                    default:
                        return 1;
                }
            }
        }

        public double Occurrence
        {
            get
            {
                return (double)this.SamplesCount / (double)Parent.SamplesCount;
            }
        }



        #region Dominance indices

        public double De_Vries_1937()
        {
            // De Vries M. Methods used in plant sociology and agricultural botanical Grassland research // Herbage Rev. 1937. V. 5.
            //int s = 0;
            //foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            //{
            //    if (logRow.CardRow.AbundanceRating(speciesRow) <= 3) s++;
            //}
            //return (double)s / (double)Parent.SamplesCount;

            return double.NaN;
        }

        public double Zenkevich_Brotskaya_1937()
        {
            // Зенкевич Л.А. Броцкая В.А. Материалы по экологии руководящих форм бентоса Баренцова моря // Уч. записки МГУ. Зоология. 1937. вып. 13, № 3
            return Math.Sqrt(this.Abundance * this.Biomass);
        }

        /// <summary>
        /// Most popular
        /// </summary>
        /// <param name="speciesRow"></param>
        /// <returns></returns>
        public double Zenkevich_Brotskaya_1939()
        {
            // Броцкая В.А. Зенкевич Л.А. Количественный учет донной фауны Баренцева моря // Тр. ВНИРО. 1939. т. 4
            return Math.Sqrt(100 * this.Occurrence * this.Biomass);
        }

        /// <summary>
        /// Modification of (Zenkevich-Brotskaya, 1939)
        /// </summary>
        /// <param name="speciesRow"></param>
        /// <returns></returns>
        public double Mordukhai_Boltovskii_1940()
        {
            // Мордухай-Болтовской Ф.Д. Состав и распределение донной фауны в водоемах дельты Дона // Тр. АзЧерНИРО. 1940. Вып. 12. Ч. 2.
            return 100 * this.Occurrence * Math.Sqrt(this.Biomass);
        }

        public double Arnoldi_1941()
        {
            // Арнольди Л.В. Материалы по количественному изучению зообентоса в Черном море // Тр. ЗИН АН СССР. 1941. т. 7, вып. 2.
            return Math.Pow(this.Biomass *
                this.Abundance *
                this.Occurrence, 1.0 / 3.0);
        }

        /// <summary>
        /// Modification of (Zenkevich-Brotskaya, 1939)
        /// </summary>
        /// <param name="speciesRow"></param>
        /// <returns></returns>
        public double Mordukhai_Boltovskii_1948()
        {
            // Мордухай-Болтовской Ф.Д. Материалы по гидробиологии Миусского лимана // Уч. зап. РГУ. 1948
            return Math.Sqrt(this.Mordukhai_Boltovskii_1940());
        }

        public double Balog_1958()
        {
            return this.AbundanceFraction;
        }

        public double Sanders_1960()
        {
            // Sanders H.L. Benthic studies in Buzzards Bay. III. The soft-bottom cenosis. //Limnol. Oceanogr. 1960. V. 5. № 2.                    
            //double r = 0;
            //for (int i = 10; i > 0; i--)
            //{
            //    int cardsCount = 0;

            //    foreach (Data.CardRow cardRow in ((Data)speciesRow.Table.DataSet).Card)
            //    {
            //        if (cardRow.AbundanceRating(speciesRow) == i)
            //            cardsCount++;
            //    }

            //    r += i * cardsCount;
            //}
            //return r;

            return double.NaN;
        }

        public double Petrov_1961()
        {
            // Петров К.М. Биоценозы рыхлых грунтов черноморской части подводного склона Таманского полуострова // Зоол. журн. 1961. т. 40, вып. 3
            return Math.Sqrt(100 * this.Occurrence * this.Abundance);
        }

        public double Kownacki_1971()
        {
            // Kownacki A. Taxocens of Chironomidae in streams of the Polish High Tatra Mts // Acta hydrobiol. 1971. V. 13, № 4.
            return 10000 * this.Occurrence * this.AbundanceFraction;
        }

        public double Kownacki_1971_B()
        {
            return 100 * this.Occurrence * this.BiomassFraction;
        }

        public double Mordukhai_Boltovskii_1975()
        {
            return this.Occurrence * Math.Sqrt(this.BiomassFraction);
        }

        public double Iogansen_Faizova_1978()
        {
            // Иоганзен Б.Г., Файзова Л.В. Об определении показателей встречаемости, обилия, биомассы и их соотношения у некоторых гидробионтов // Тр. ВГБО. 1978. т. 22.
            return Math.Sqrt(this.Biomass *
                this.Abundance) /
                (100 * this.Occurrence);
        }

        public double Dedyu_1990()
        {
            // Дедю И. И. Экологический энциклопедический словарь. – Кишинев: Гл. ред. Молдав. Сов. Энциклопедии, 1990. – 408 с.                    
            return this.DensityIndex;
        }

        public double Shcherbina_1993()
        {
            // Щербина Г. Х. 1993. Годовая динамика макрозообентоса открытого мелководья Волжского плеса Рыбинского водохранилища // Зооценозы водоемов бассейна Верхней Волги в условиях антропогенного воздействия. СПб.: Гидрометеоиздат, С. 108–144.
            return Math.Pow(
                100 * this.BiomassFraction *
                100 * this.AbundanceFraction *
                100 * this.Occurrence, 1.0 / 3.0);
        }

        public double Shitikov_et_al_2003()
        {
            return 100 * this.Occurrence * this.DensityIndex /
                Math.Sqrt(this.Abundance * this.Biomass);
        }

        #endregion

        #region IComparable

        public int CompareTo(object o)
        {
            if (o is Category)
            {
                return this.CompareTo((Category)o);
            }
            else
            {
                return -1;
            }
        }

        public int CompareTo(Category cat)
        {
            return cat.Dominance.CompareTo(this.Dominance);
        }

        #endregion
    }

    public enum ValueVariant
    {
        Quantity,
        Mass,
        Abundance,
        Biomass,
        AbundanceFraction,
        BiomassFraction,
        Occurrence,
        Dominance
    }
}
