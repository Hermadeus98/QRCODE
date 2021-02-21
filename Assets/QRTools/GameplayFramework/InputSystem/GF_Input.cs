using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class GF_Input : GF_Monobehaviour
    {
        protected bool isActive = true;
        [TextArea(2, 4), SerializeField] private string description;
        [SerializeField] GF_INPUT_EVENT GF_INPUT_EVENT;

        [Range(1, 10)] [SerializeField] protected int playerIndex = 1;
        public int getPlayerIndex { get => playerIndex; }
        
        protected delegate void InputDelegate();
        protected InputDelegate inputDelegate;

        protected override void OnAwake()
        {
            GF_InputManager.AddInput(GF_INPUT_EVENT, this);
            GF_InputManager.AddPlayerInput(playerIndex, GF_INPUT_EVENT, this);
        }

        protected override void OnUpdate()
        {
            if (!isActive) return;
        }

        public bool IsActive() => isActive;
        public bool SetInputActive(bool value) => isActive = value;

    }
}