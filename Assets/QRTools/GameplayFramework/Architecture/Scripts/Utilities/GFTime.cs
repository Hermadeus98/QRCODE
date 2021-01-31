using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    /// <summary>
    /// Manage delta time of the game.
    /// </summary>
    public static class GFTime
    {
        /// <summary>
        /// Delta time of Unity.
        /// </summary>
        public static float deltaTime => Time.deltaTime;

        /// <summary>
        /// Time scale of gameplay part.
        /// </summary>
        public static float gameplayTimeScale = 1f;
        
        /// <summary>
        /// Delta time of gameplay part.
        /// </summary>
        public static float gameplayDeltaTime
        {
            get => deltaTime * gameplayTimeScale;
        }

        /// <summary>
        /// Time scale of UI part.
        /// </summary>
        public static float uiTimeScale = 1f;

        /// <summary>
        /// Delta Time of UI.
        /// </summary>
        public static float uiDeltaTime
        {
            get => deltaTime * uiTimeScale;
        }
    }
}