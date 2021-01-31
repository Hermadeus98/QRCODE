using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Singletons;

namespace GameplayFramework
{
    public class Game : MonoBehaviourSingleton<Game>
    {
        public delegate void GameEventDelegate();
        public GameEventDelegate
            OnGameStartCallback,
            OnGameEndCallback,
            OnGamePauseOnCallback,
            OnGamePauseOffCallback;

        public virtual void StartGame()
        {
            OnGameStartCallback?.Invoke();
        }

        public virtual void EndGame()
        {
            OnGameEndCallback?.Invoke();
        }

        public virtual void PauseOn()
        {
            OnGamePauseOnCallback?.Invoke();
        }

        public virtual void PauseOff()
        {
            OnGamePauseOffCallback?.Invoke();
        }
    }
}