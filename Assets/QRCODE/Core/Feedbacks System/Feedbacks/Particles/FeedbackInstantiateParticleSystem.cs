using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackInstantiateParticleSystem : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public ParticleSystem ParticleSystemPrefab = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Transform[] InstantiateAtPositions = default;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float ScaleRatio = 1f;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool useDurationAsParticleSystemDuration = true;

        //--<Private>
        private List<ParticleSystem> particleSystemInstance = null;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackInstantiateParticleSystem() : base()
        {
            useDurationAsParticleSystemDuration = false;
            ScaleRatio = 1f;
        }
        
        public FeedbackInstantiateParticleSystem(
            MonoBehaviour owner, ParticleSystem particleSystemPrefab, Transform[] transforms)
            : base(owner)
        {
            ParticleSystemPrefab = particleSystemPrefab;
            InstantiateAtPositions = new Transform[transforms.Length];
            for (int i = 0; i < transforms.Length; i++)
            {
                InstantiateAtPositions[i] = transforms[i];
            }
            Duration = useDurationAsParticleSystemDuration ? ParticleSystemPrefab.main.duration : Duration;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            particleSystemInstance = new List<ParticleSystem>();
            
            for (int i = 0; i < InstantiateAtPositions.Length; i++)
            {
                var Instance = Object.Instantiate(
                    ParticleSystemPrefab,
                    InstantiateAtPositions[i].position,
                    Quaternion.identity);

                particleSystemInstance.Add(Instance);

                Instance.transform.localScale *= ScaleRatio;
            
                Instance?.Play();
            }
            
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(!particleSystemInstance.IsNullOrEmpty())
                particleSystemInstance.ForEach(w => w.Stop());
            return this;
        }

        public FeedbackInstantiateParticleSystem SetInstantiateAtPositions(Transform[] transforms)
        {
            InstantiateAtPositions = new Transform[transforms.Length];
            for (int i = 0; i < transforms.Length; i++)
            {
                InstantiateAtPositions[i] = transforms[i];
            }

            return this;
        }
    }
}
