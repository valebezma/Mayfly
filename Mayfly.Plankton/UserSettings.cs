using Mayfly.Wild;

namespace Mayfly.Plankton
{
    public static class UserSettings
    {
        private static ReaderUserSettings settings;

        public static ReaderUserSettings ReaderSettings
        {
            get
            {
                if (settings == null) {
                    settings = new ReaderUserSettings(".pcd", "Plankton");
                }

                return settings;
            }
        }
    }
}