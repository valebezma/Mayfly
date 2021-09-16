namespace Mayfly.Waters
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Waters");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".wtr");

        public static int SearchItemsCount
        {
            get { return (int)UserSetting.GetValue(Path, nameof(SearchItemsCount), 15); }
            set { UserSetting.SetValue(Path, nameof(SearchItemsCount), value); }
        }
    }
}
