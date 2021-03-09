using UnityEngine;

namespace PrefsUGUI
{
    public interface IPrefsParameters
    {
        string AggregationName { get; }
        string FileName { get; }
    }

    public class PrefsParameters : IPrefsParameters
    {
        public string AggregationName { get; private set; }
        public string FileName { get; private set; }


        public PrefsParameters(string aggregationName, string fileName)
        {
            this.AggregationName = aggregationName;
            this.FileName = fileName;
        }

        public static implicit operator PrefsParameters((string aggregationName, string fileName) tupple)
            => new PrefsParameters(tupple.aggregationName, tupple.fileName);

        public static implicit operator (string aggregationName, string fileName)(PrefsParameters prefsParams)
            => (prefsParams.AggregationName, prefsParams.FileName);
    }

    [DisallowMultipleComponent]
    [AddComponentMenu("PrefsUGUI/Prefs Parameters Entity")]
    public class PrefsParametersEntity : MonoBehaviour, IPrefsParameters
    {
        public string AggregationName
            => string.IsNullOrEmpty(this.aggregationName) == true ? GetDefaultName() : this.aggregationName;
        public string FileName
            => string.IsNullOrEmpty(this.fileName) == true ? GetDefaultName() : this.fileName;

        [SerializeField]
        private string aggregationName = "";
        [SerializeField]
        private string fileName = "";


        private void Reset()
        {
            this.aggregationName = GetDefaultName();
            this.fileName = GetDefaultName();
        }

        public static string GetDefaultName()
            => Application.productName;
    }
}
