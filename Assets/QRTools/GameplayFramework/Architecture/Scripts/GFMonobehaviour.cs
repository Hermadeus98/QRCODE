using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class GF_Monobehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            GF_Game.OnGameStartCallback += OnGameStart;
            GF_Game.OnGameEndCallback += OnGameEnd;
            GF_Game.OnGamePauseOnCallback += OnGamePauseOn;
            GF_Game.OnGamePauseOffCallback += OnGamePauseOff;
        }

        protected virtual void OnDisable()
        {
            GF_Game.OnGameStartCallback -= OnGameStart;
            GF_Game.OnGameEndCallback -= OnGameEnd;
            GF_Game.OnGamePauseOnCallback -= OnGamePauseOn;
            GF_Game.OnGamePauseOffCallback -= OnGamePauseOff;
        }

        private void Awake() => OnAwake();
        private void Start() => OnStart();
        private void Update() => OnUpdate();

        protected virtual void OnGameStart() { }
        protected virtual void OnGameEnd() { }

        protected virtual void OnGamePauseOn() { }
        protected virtual void OnGamePauseOff() { }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
    }
}