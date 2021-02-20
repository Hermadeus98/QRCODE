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
            GF_Event<ScriptTest, float, bool>.AddListener(GF_EVENT_ENUM.GF_EVENT_TEST_01, OnSpeedChange);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GF_Event<ScriptTest, float, bool>.RemoveListener(GF_EVENT_ENUM.GF_EVENT_TEST_01, OnSpeedChange);
        }

        private void Update()
        {
            if (f)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GF_Event<ScriptTest, float, bool>.Broadcast(GF_EVENT_ENUM.GF_EVENT_TEST_01, this, 50, true);
            }
        }

        void OnSpeedChange(ScriptTest st, float flo, bool b)
        {
            Debug.Log(a.ToString() + b.ToString() + st.name);
        }
    }
}