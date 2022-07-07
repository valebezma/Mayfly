using static Mayfly.UserSettings;

namespace Mayfly.Waters
{
    public static class UserSettings
    {
        public static string Path
        {
            get
            {
                return GetFeatureKey("Waters");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".wtr");

        public static int SearchItemsCount
        {
            get { return (int)GetValue(Path, nameof(SearchItemsCount), 15); }
            set { SetValue(Path, nameof(SearchItemsCount), value); }
        }
    }
}
