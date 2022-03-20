using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using QRCode.UI;
using TMPro;
using UnityEngine;

namespace QRCode.Debugging
{
    public class DebuggingWindow : UIView
    {
        [SerializeField] private QRDebugSettings qrDebugSettings;
        [SerializeField] private TextMeshProUGUI textMesh;
        
        private Queue<string> messages = new Queue<string>();
        private Coroutine hideCoroutine;
        
        public override void Init()
        {
            base.Init();
            DontDestroyOnLoad(this);
            textMesh.color = qrDebugSettings.textColor;
            textMesh.fontSize = qrDebugSettings.textSize;
        }
        
        public void Debug(object message)
        {
            Show();
            
            messages.Enqueue( message + "\n" );
            SetText();
            
            if(hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideDelayed());

            if (messages.Count >= qrDebugSettings.maxDebugText)
                messages.Dequeue();
        }

        private IEnumerator HideDelayed()
        {
            yield return new WaitForSeconds(qrDebugSettings.hideDelay);
            Hide();
            if(qrDebugSettings.clearTextOnHide)
                messages.Clear();
        }

        private void SetText()
        {
            var messageToShow = string.Empty;

            for (int i = 0; i < messages.Count; i++)
                messageToShow += messages.ElementAt(i);
            
            textMesh.SetText(messageToShow);
        }
        
        public static DebuggingWindow CreateInstance()
        {
            var settings = Resources.Load<QRDebugSettings>("Data/Debugging Settings");
            var window = Instantiate(settings.DebuggingWindowPrefab);
            window.gameObject.name = "Canvas - Debugging Window";
            return window;
        }
    }
}
