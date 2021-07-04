using Mayfly;

namespace Mayfly.Waters
{
    public class AppPaths
    {
        public static string CrossSectionStream
        {
            get
            {
                return Service.LocalizedPath(@"Interface\CrossSectionStream.ini");
            }
        }

        public static string BiotopStream
        {
            get
            {
                return Service.LocalizedPath(@"Interface\BiotopStream.ini");
            }
        }

        public static string Bank
        {
            get
            {
                return Service.LocalizedPath(@"Interface\Bank.ini");
            }
        }

        public static string CrossSectionTank
        {
            get
            {
                return Service.LocalizedPath(@"Interface\CrossSectionTank.ini");
            }
        }

        public static string BiotopTank
        {
            get
            {
                return Service.LocalizedPath(@"Interface\BiotopTank.ini");
            }
        }
    }
}
