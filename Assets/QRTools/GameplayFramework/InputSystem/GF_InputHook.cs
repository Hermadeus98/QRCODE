using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameplayFramework
{
    public class GF_InputHook : GF_Input
    {
        [SerializeField] GF_InputType GF_InputType = GF_InputType.GF_SIMPLE;

        public KeyCode
            keyboardKeyCode,
            XBOXControllerKeyCode;

        [SerializeField] UnityEvent OnDown, OnUp, OnCurrent;

        [Header("Multi Click Options")]
        [SerializeField] float timeBetweenTwoClick = .2f;
        [SerializeField] UnityEvent OnMultiClick;
        [SerializeField, Range(2, 10)] int clickCount = 2;

        [Header("Long Click Options")]
        [SerializeField] float longClickTime = .5f;
        [SerializeField] UnityEvent OnLongClick;
        [SerializeField] FloatUnityEvent OnLongClickPassed;

        private bool isDown = false;
        private float elapsedTime;
        private int m_clickCount = 0;
        private bool canClick = false;

        public bool IsDown { get => IsDown; }

        protected override void OnStart()
        {
            base.OnStart();
            switch (GF_InputType)
            {
                case GF_InputType.GF_SIMPLE:
                    inputDelegate += SimpleInput;
                    break;
                case GF_InputType.GF_DOUBLE_CLICK:
                    clickCount = 2;
                    inputDelegate += MultiClick;
                    break;
                case GF_InputType.GF_MULTI_CLICK:
                    inputDelegate += MultiClick;
                    break;
                case GF_InputType.GF_LONG_CLICK:
                    canClick = true;
                    inputDelegate += LongClick;
                    break;
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            inputDelegate?.Invoke();
        }

        void SimpleInput()
        {
            if (Input.GetKeyDown(keyboardKeyCode) || Input.GetKeyDown(XBOXControllerKeyCode))
            {
                isDown = true;
                OnDown?.Invoke();
            }

            if (Input.GetKeyUp(keyboardKeyCode) || Input.GetKeyUp(XBOXControllerKeyCode))
            {
                isDown = false;
                OnUp?.Invoke();
            }

            if (Input.GetKey(keyboardKeyCode) || Input.GetKey(XBOXControllerKeyCode))
            {
                OnCurrent?.Invoke();
            }
        }

        void MultiClick()
        {
            if (elapsedTime > 0)
            {
                canClick = true;
                elapsedTime -= GF_Time.gameplayDeltaTime;
            }
            else
            {
                canClick = false;
                m_clickCount = 0;
            }

            if ((Input.GetKeyDown(keyboardKeyCode) || Input.GetKeyDown(XBOXControllerKeyCode)))
            {
                isDown = true;
                OnDown?.Invoke();

                m_clickCount++;

                if (!canClick)
                    elapsedTime = timeBetweenTwoClick;

                if(m_clickCount == clickCount && canClick)
                {
                    OnMultiClick?.Invoke();
                    m_clickCount = 0;
                    elapsedTime = 0;
                }
            }

            if (Input.GetKeyUp(keyboardKeyCode) || Input.GetKeyUp(XBOXControllerKeyCode))
            {
                isDown = false;
                OnUp?.Invoke();
            }

            if (Input.GetKey(keyboardKeyCode) || Input.GetKey(XBOXControllerKeyCode))
            {
                OnCurrent?.Invoke();
            }
        }

        void LongClick()
        {
            if (Input.GetKeyDown(keyboardKeyCode) || Input.GetKeyDown(XBOXControllerKeyCode))
            {
                isDown = true;
                OnDown?.Invoke();

                elapsedTime = longClickTime;
            }

            if (Input.GetKeyUp(keyboardKeyCode) || Input.GetKeyUp(XBOXControllerKeyCode))
            {
                isDown = false;
                OnUp?.Invoke();
                canClick = true;
            }

            if (Input.GetKey(keyboardKeyCode) || Input.GetKey(XBOXControllerKeyCode))
            {
                OnCurrent?.Invoke();

                elapsedTime -= GF_Time.gameplayDeltaTime;

                if(elapsedTime > 0 && canClick)
                {
                    float normalisedTimer = elapsedTime / longClickTime;
                    OnLongClickPassed?.Invoke(normalisedTimer);
                }

                if(elapsedTime < 0 && canClick)
                {
                    OnLongClick?.Invoke();
                    canClick = false;
                }
            }
        }

        public void AddOnDownListener(UnityAction call) => OnDown.AddListener(call);
        public void AddOnupListener(UnityAction call) => OnUp.AddListener(call);
        public void AddOnCurrentListener(UnityAction call) => OnCurrent.AddListener(call);

        public void RemoveOnDownListener(UnityAction call) => OnDown.RemoveListener(call);
        public void RemoveOnupListener(UnityAction call) => OnUp.RemoveListener(call);
        public void RemoveOnCurrentListener(UnityAction call) => OnCurrent.RemoveListener(call);
    }

    public enum GF_InputType
    {
        GF_SIMPLE,
        GF_DOUBLE_CLICK,
        GF_MULTI_CLICK,
        GF_LONG_CLICK
    }
}