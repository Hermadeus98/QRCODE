using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class ScriptTest : GF_Monobehaviour
    {
        float a;
        bool b;

        public bool f;

        protected override void OnEnable()
        {
            base.OnEnable();
            GF_Event.AddListener<float>(GF_EVENT_ENUM.GF_EVENT_TEST_01, GetFloat);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GF_Event.RemoveListener<float>(GF_EVENT_ENUM.GF_EVENT_TEST_01, GetFloat);
        }

        protected override void OnStart()
        {
            base.OnStart();
            //GF_InputManager.GetPlayerInput<GF_InputHook>(2, GF_INPUT_EVENT.TEST_01).AddOnDownListener(JeDebug);
            //GF_InputManager.GetPlayerInput<GF_InputHook>(1, GF_INPUT_EVENT.TEST_01).AddOnDownListener(JeDebug2);
        }

        private void Update()
        {
            if (f)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GF_Event.Broadcast<float>(GF_EVENT_ENUM.GF_EVENT_TEST_01, OnRetFloat);
            }
        }

        void OnRetFloat(float f)
        {
            Debug.Log(f);
        }

        float GetFloat()
        {
            return 50f;
        }

        void JeDebug()
        {
            Debug.Log("AIEAIEAIE");
        }

        void JeDebug2()
        {
            Debug.Log("AIEAIEAIE  2");
        }
    }
}