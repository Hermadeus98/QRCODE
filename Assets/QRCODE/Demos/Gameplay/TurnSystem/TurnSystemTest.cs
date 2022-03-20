using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Gameplay.Testing
{
    public class TurnSystemTest : SerializedMonoBehaviour
    {
        public ITurnActor[] actors;
        
        [Button]
        public void Play()
        {
            TurnSystem.Initialize();
            TurnSystem.RegisterActors(actors);
            
            TurnSystem.Play();
        }

        [Button]
        public void NextTurn()
        {
            TurnSystem.NextTurn();
        }
    }
}
