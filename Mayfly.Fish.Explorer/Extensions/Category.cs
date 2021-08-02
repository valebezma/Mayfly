using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public static class CategoryExtensions
    {
        public static int GetTotalQuantity(this IEnumerable<Category> categories)
        {
            int result = 0;

            foreach (Category category in categories)
            {
                result += category.Quantity;
            }

            return result;
        }

        public static double GetTotalMass(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Mass;
            }

            return result;
        }

        public static double GetTotalAbundance(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Abundance;
            }

            return result;
        }

        public static double GetTotalBiomass(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Biomass;
            }

            return result;
        }
    }
}
