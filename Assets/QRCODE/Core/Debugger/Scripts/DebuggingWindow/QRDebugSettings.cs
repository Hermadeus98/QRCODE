using System.Collections;
using System.Collections.Generic;
using QRCode.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Debugging
{
    [CreateAssetMenu(menuName = "QRCode/Debugging/Debugging Settings", fileName = "DebuggingSettings")]
    public class QRDebugSettings : ScriptableObjectSingleton<QRDebugSettings>
    {
        public DebuggingWindow DebuggingWindowPrefab;

        public int maxDebugText = 12;
        public int textSize = 12;
        public Color textColor;

        public float hideDelay = 4f;
        public bool clearTextOnHide = true;

        public string debugFileName = "/QRDebugFile";
    }
}
