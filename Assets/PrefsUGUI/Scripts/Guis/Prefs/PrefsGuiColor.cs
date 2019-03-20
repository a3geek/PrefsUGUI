using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiColor : PrefsGuiVectorBase<Color>
    {
        protected override int ElementCount => 4;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;

        [SerializeField]
        protected RawImage preview = null;


        protected override void SetFields()
        {
            base.SetFields();
            this.preview.color = this.GetValue();
        }

        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector4FromField());

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void Reset()
        {
            base.Reset();
            this.preview = GetComponentInChildren<RawImage>();
        }
    }
}
