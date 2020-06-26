namespace PrefsUGUI
{
    using Managers;
    using XmlStorage;
    using XmlStorageConsts = XmlStorage.Systems.XmlStorageConsts;

    public static class Prefs
    {
        public const char HierarchySeparator = '/';

        public static string AggregationName => PrefsManager.AggregationName;
        public static string FileName => PrefsManager.FileName;


        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            foreach (var setter in PrefsManager.StorageValueSetters)
            {
                setter?.Invoke();
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        public static void ShowGUI()
        {
            if (PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.ShowGUI();
            }
        }

        public static bool IsShowing()
            => PrefsManager.PrefsGuis != null && PrefsManager.PrefsGuis.IsShowing;

        public static void SetCanvasSize(float width, float height)
        {
            if (PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.SetCanvasSize(width, height);
            }
        }
    }
}
