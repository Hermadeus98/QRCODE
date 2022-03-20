using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace QRCode.Debugging
{
    public class DebuggingWindowText : SerializedMonoBehaviour
    {
        private TextMeshProUGUI text;
        
        private void Start()
        {
            gameObject.SetActive(false);
            text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(object message)
        {
            text.SetText(message.ToString());
        }
    }
}
