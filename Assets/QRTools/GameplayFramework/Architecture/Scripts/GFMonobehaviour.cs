using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class GFMonobehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            Game.Instance.OnGameStartCallback += OnGameStart;
            Game.Instance.OnGameEndCallback += OnGameEnd;
            Game.Instance.OnGamePauseOnCallback += OnGamePauseOn;
            Game.Instance.OnGamePauseOffCallback += OnGamePauseOff;
        }

        protected virtual void OnDisable()
        {
            Game.Instance.OnGameStartCallback -= OnGameStart;
            Game.Instance.OnGameEndCallback -= OnGameEnd;
            Game.Instance.OnGamePauseOnCallback -= OnGamePauseOn;
            Game.Instance.OnGamePauseOffCallback -= OnGamePauseOff;
        }

        private void Start() => OnStart();

        public virtual void OnGameStart() { }
        public virtual void OnGameEnd() { }

        public virtual void OnGamePauseOn() { }
        public virtual void OnGamePauseOff() { }

        public virtual void OnStart() { }
    }
}