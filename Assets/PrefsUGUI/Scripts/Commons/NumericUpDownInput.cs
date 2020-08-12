using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Commons
{
    [Serializable]
    public class NumericUpDownInput
    {
        public float UpDownStep
        {
            get => this.upDownStep;
            set => this.upDownStep = value;
        }
        public float StepDelay
        {
            get => this.stepDelay;
            set => this.stepDelay = value;
        }

        [SerializeField, Range(0f, 10f)]
        protected float upDownStep = 0.01f;
        [SerializeField, Range(0f, 5f)]
        protected float stepDelay = 0.05f;

        protected float stepTime = -1f;


        public bool Update(InputField field, out float deltaValue)
        {
            deltaValue = 0f;

            if (field == null || field.isFocused == false || this.IsNumericType(field) == false)
            {
                return false;
            }
            if (this.stepTime >= 0f)
            {
                this.stepTime += Time.deltaTime;
                if (this.stepTime < this.stepDelay)
                {
                    return false;
                }

                this.stepTime = -1f;
            }

            int dir;
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                dir = 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                dir = -1;
            }
            else
            {
                return false;
            }

            deltaValue = dir * this.upDownStep;
            this.stepTime = 0f;

            return true;
        }

        protected virtual bool IsNumericType(InputField field)
            => field.contentType == InputField.ContentType.DecimalNumber || field.contentType == InputField.ContentType.IntegerNumber;
    }
}
