using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using Sirenix.OdinInspector;

namespace QRCode.Feedbacks
{
    public class FeedbackControllerVibrator : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), MinMaxSlider(0f, 1f)]
        public Vector2 frequency = new Vector2(1, 1);
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float duration = 0;


        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackControllerVibrator() : base()
        {
            frequency = new Vector2(1, 1);
        }


        public override IEnumerator Describe()
        {
            #if UNITY_INPUT_SYSTEM
            Gamepad.current.SetMotorSpeeds(frequency.x,frequency.y);
            yield return new WaitForSeconds(duration);
            Gamepad.current.SetMotorSpeeds(0, 0);
            #endif
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();

            return this;

        }

        public FeedbackControllerVibrator SetFrequency(Vector2 value)
        {
            frequency = value;
            return this;
        }

        public FeedbackControllerVibrator SetDuration(float value)
        {
            duration = value;
            return this;
        }

    }
}

