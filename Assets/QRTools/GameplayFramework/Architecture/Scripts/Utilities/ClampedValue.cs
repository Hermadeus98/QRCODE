using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class ClampedValue<T> where T : struct
    {
        protected T value;
        protected T minValue;
        protected T maxValue;

        public T GetMinValue => minValue;
        public T GetMaxValue => maxValue;

        public T SetMinValue(T newMinValue) => minValue = newMinValue;
        public T SetMaxValue(T newMaxValue) => maxValue = newMaxValue;

        public T GetValue() => value;
    }
}