using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Debugging;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Gameplay.Testing
{
    public class TurnActorTest : SerializedMonoBehaviour, ITurnActor
    {
        public bool isDead = false;
        
        [Button]
        public void Die()
        {
            isDead = true;
        }
        
        private void OnDisable()
        {
            TurnSystem.UnregisterActor(this);
        }

        public void OnBeginTurn(TurnInfo info)
        {
            QRDebug.Log("TurnSystem", FrenchPallet.GREEN_SEA, "Turn started " + gameObject.name);
        }

        public void OnEndTurn(TurnInfo info)
        {
            QRDebug.Log("TurnSystem", FrenchPallet.GREEN_SEA, "Turn ended " + gameObject.name);
        }

        public bool CanPlayTurn(TurnInfo info)
        {
            return !isDead;
        }
    }
}
