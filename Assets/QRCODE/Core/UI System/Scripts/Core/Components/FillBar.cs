using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class FillBar : UIElement
    {
        [SerializeField, Range(0, 1), OnValueChanged("Set")] private float fillAmount;

        [SerializeField] private bool retargeting = false;
        [SerializeField] private Vector2 target = new Vector2();

        [SerializeField]
        private bool invertPercent = false;
        
        [SerializeField] private bool useScale = false;
        private float maxSize = 100f;
        
        [FoldoutGroup("References")] [SerializeField]
        private Image fill;

        [BoxGroup("Settings")] [SerializeField]
        private bool useTween = true;
        [BoxGroup("Settings")]
        [SerializeField] private float tweenDuration = .2f;

        [BoxGroup("Settings")] [SerializeField]
        private Ease tweenEase = Ease.InOutSine;

        private Tween doFill;

        public override void Init()
        {
            base.Init();
            maxSize = fill.rectTransform.sizeDelta.x;
        }

        private void Set() => SetFill(fillAmount);
        
        public void SetFill(float percent)
        {
            percent = Mathf.Clamp01(percent);

            if (invertPercent)
                percent = 1 - percent;

            if (retargeting)
            {
                percent = Mathf.Lerp(target.x, target.y, percent);
            }

            if (useScale)
            {
                if (useTween)
                {
                    doFill = fill.rectTransform.DOSizeDelta(
                        new Vector2(
                            Mathf.Lerp(maxSize, 0f, percent),
                            fill.rectTransform.sizeDelta.y),
                        tweenDuration
                    ).SetEase(tweenEase);
                }
                else
                {
                    fill.rectTransform.sizeDelta = new Vector2(
                        Mathf.Lerp(maxSize, 0f, percent),
                        fill.rectTransform.sizeDelta.y
                        );
                }
            }
            else
            {
                if (useTween)
                {
                    doFill = fill.DOFillAmount(percent, tweenDuration).SetEase(tweenEase);
                }
                else
                {
                    fill.fillAmount = percent;
                }
            }
        }

        public void DoFill(float to, float duration, Action onComplete)
        {
            doFill = fill.DOFillAmount(to, duration).SetEase(tweenEase).OnComplete(onComplete.Invoke);
        }

        public void SetFillToZero(bool killTween = false)
        {
            if(killTween) doFill.Kill();
            fill.fillAmount = 0;
        }
    }
}
