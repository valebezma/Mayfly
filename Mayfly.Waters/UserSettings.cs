using System.Windows.Forms;
using static Mayfly.UserSettings;
using System.IO;

namespace Mayfly.Waters
{
    public static class UserSettings
    {
        public static string FeatureKey {
            get {
                return GetFeatureKey("Waters");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".wtr");

        public static int SearchItemsCount {
            get { return (int)GetValue(FeatureKey, nameof(SearchItemsCount), 15); }
            set { SetValue(FeatureKey, nameof(SearchItemsCount), value); }
        }
    }
}
