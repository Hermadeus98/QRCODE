using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Debugging.Tests
{
    public class DebuggerTest : SerializedMonoBehaviour
    {
        
        [Button]
        public void TestDebuggingWindow()
        {
            QRDebug.DebugText("Test Debugging", FrenchPallet.CARROT, "Ceci est un debug at : " + Time.time.ToString());
        }
        
        [Button]
        public void TestLog()
        {
            QRDebug.Log("Test Debugging", FrenchPallet.CARROT, "Ceci est un debug.");
        }
    }
}
