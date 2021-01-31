using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    [System.Serializable]
    public class ClampedFloat : ClampedValue<float>
    {
        public ClampedFloat()
        {
            value = 0f;
            minValue = 0f;
            maxValue = 1f;
        }

        public ClampedFloat(float minValue, float maxValue)
        {
            if (minValue >= maxValue)
            {
                float newMax = minValue;
                minValue = maxValue;
                maxValue = newMax;
            }

            this.minValue = minValue;
            this.maxValue = maxValue;
            value = minValue;
        }

        public ClampedFloat(float minValue, float maxValue, float value)
        {
            if (minValue >= maxValue)
            {
                float newMax = minValue;
                minValue = maxValue;
                maxValue = newMax;
            }

            if (value < minValue)
                value = minValue;
            else if (value > maxValue)
                value = maxValue;

            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = value;
        }

        public float SetValue(float newValue) => value = Mathf.Clamp(newValue, minValue, maxValue);

        public float SetValueAndOverrideClamping(float newValue)
        {
            if(newValue > maxValue)
            {
                maxValue = newValue;
                value = newValue;
            }
            else
            {
                minValue = newValue;
                value = newValue;
            }

            return value;
        }
    }
}