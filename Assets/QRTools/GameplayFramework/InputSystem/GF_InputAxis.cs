using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class GF_InputAxis : GF_Input
    {
        [SerializeField] GF_AxisType GF_AxisType = GF_AxisType.GF_SIMPLE;

        public KeyCode
            keyboardKeyCodePositive,
            keyboardKeyCodeNegative;

        [SerializeField] string axisName;

        public FloatUnityEvent OnAxisChange;

        public bool invert = false;

        float value;
        public float GetValue 
        {
            get => invert ? -value : value;
        }


        protected override void OnStart()
        {
            base.OnStart();

            switch (GF_AxisType)
            {
                case GF_AxisType.GF_SIMPLE:
                    inputDelegate += SimpleAxis;
                    break;
                case GF_AxisType.GF_AXIS_RAW:
                    inputDelegate += AxisRaw;
                    break;
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            inputDelegate?.Invoke();
        }

        void SimpleAxis()
        {
            if (!string.IsNullOrEmpty(axisName))
                value = Input.GetAxis(axisName);

            if (keyboardKeyCodePositive != KeyCode.None && keyboardKeyCodeNegative != KeyCode.None)
            {
                if (Input.GetKey(keyboardKeyCodePositive))
                {
                    value = Mathf.Lerp(value, 1f, 1f);
                    if (value > 0.99)
                        value = 1f;
                }
                else if (Input.GetKey(keyboardKeyCodeNegative))
                {
                    value = Mathf.Lerp(value, -1f, 1f);
                    if (value < -0.99)
                        value = -1f;
                }
            }

            Debug.Log(GetValue);
        }

        void AxisRaw()
        {
            if (!string.IsNullOrEmpty(axisName))
                value = Input.GetAxisRaw(axisName);

            if (Input.GetKey(keyboardKeyCodePositive))
                value = 1f;
            else if (Input.GetKey(keyboardKeyCodeNegative))
                value = -1f;
        }
    }

    public enum GF_AxisType
    {
        GF_SIMPLE,
        GF_AXIS_RAW
    }
}