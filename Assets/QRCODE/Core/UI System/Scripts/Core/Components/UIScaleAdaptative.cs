using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCODE.UI{
    public class UIScaleAdaptative : MonoBehaviour
    {
        public float scaleMultiplier = 1f;
        public Vector2 clampSize = new Vector2();
        
        private void Update()
        {
            var dist = Vector3.Distance(Camera.main.transform.position, transform.position);

            dist = Mathf.Clamp(dist, clampSize.x, clampSize.y);
            
            transform.localScale = Vector3.one * scaleMultiplier * dist;
        }
    }
}
