namespace Mayfly.Reader
{
    public abstract class UserSettingPaths
    {
        public static string Path
        {
            get
            {
                return UserSettingInterface.GetFeatureKey("Mayfly.Reader");
            }
        }

        public static void InitializeUserSettings()
        {
            Wild.UserSettingPaths.Initialize();

            UserSettingInterface.InitializeRegistry(UserSettingPaths.Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] { 
                    new UserSetting(Wild.UserSettingPaths.Water, 0),
                    new UserSetting(Wild.UserSettingPaths.FixTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoIncreaseTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoDecreaseTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoLogOpen, false),
                    new UserSetting(Wild.UserSettingPaths.BreakBeforeIndividuals, true),
                    new UserSetting(Wild.UserSettingPaths.BreakBetweenSpecies, false),
                    new UserSetting(Wild.UserSettingPaths.OddCardStart, true),
                    new UserSetting(Wild.UserSettingPaths.AddtVars, new string[0]),
                    new UserSetting(Species.UserSettingPaths.RecentItemsCount, 10)
                });
        }
    }
}
