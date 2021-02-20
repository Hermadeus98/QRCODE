using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class GF_Monobehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            Game.OnGameStartCallback += OnGameStart;
            Game.OnGameEndCallback += OnGameEnd;
            Game.OnGamePauseOnCallback += OnGamePauseOn;
            Game.OnGamePauseOffCallback += OnGamePauseOff;
        }

        protected virtual void OnDisable()
        {
            Game.OnGameStartCallback -= OnGameStart;
            Game.OnGameEndCallback -= OnGameEnd;
            Game.OnGamePauseOnCallback -= OnGamePauseOn;
            Game.OnGamePauseOffCallback -= OnGamePauseOff;
        }

        private void Start() => OnStart();

        public virtual void OnGameStart() { }
        public virtual void OnGameEnd() { }

        public virtual void OnGamePauseOn() { }
        public virtual void OnGamePauseOff() { }

        public virtual void OnStart() { }
    }
}