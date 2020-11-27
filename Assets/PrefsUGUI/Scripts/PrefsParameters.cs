using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("PrefsUGUI/Prefs Parameters")]
    public class PrefsParameters : MonoBehaviour
    {
        public static readonly Func<string> DefaultNameGetter = () => Application.productName;

        public string AggregationName
            => string.IsNullOrEmpty(this.aggregationName) == true ? DefaultNameGetter() : this.aggregationName;
        public string FileName
            => string.IsNullOrEmpty(this.fileName) == true ? DefaultNameGetter() : this.fileName;

        [SerializeField]
        private string aggregationName = "";
        [SerializeField]
        private string fileName = "";


        private void Reset()
        {
            this.aggregationName = DefaultNameGetter();
            this.fileName = DefaultNameGetter();
        }
    }
}
