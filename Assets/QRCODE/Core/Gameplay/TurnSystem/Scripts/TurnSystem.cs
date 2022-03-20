using System.Collections;
using System.Collections.Generic;
using QRCode.Debugging;
using QRCode.Extensions;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace QRCode.Gameplay
{
    public static class TurnSystem
    {
        public static TurnInfo turnInfo;

        private static int turnIndex;
        private static bool isPlaying;

        public static TunrInfoUnityEvent OnTurnStart, OnTurnEnd;
        
        public static void Initialize()
        {
            isPlaying = true;

            turnInfo.actors = new List<ITurnActor>();
            turnIndex = 0;
        }

        public static void Play()
        {
            if (turnInfo.actors.IsNullOrEmpty())
            {
                QRDebug.Log("TurnSystem", FrenchPallet.GREEN_SEA, "There is no Actor register.");
                return;
            }
            
            NextTurn();
        }

        public static void NextTurn()
        {
            if(!isPlaying || turnIndex == turnInfo.turnCount)
                return;

            if (turnInfo.currentActor != null)
                if (turnInfo.currentActor.CanPlayTurn(turnInfo))
                {
                    turnInfo.currentActor.OnEndTurn(turnInfo);
                }

            OnTurnStart?.Invoke(turnInfo);
            
            turnInfo.currentActor = turnInfo.actors[turnIndex];
            
            if(turnInfo.currentActor != null)
                if (turnInfo.currentActor.CanPlayTurn(turnInfo))
                {
                    turnInfo.currentActor.OnBeginTurn(turnInfo);
                }

            OnTurnEnd?.Invoke(turnInfo);

            turnIndex++;

            if (turnIndex == turnInfo.turnCount)
            {
                QRDebug.Log("TurnSystem", FrenchPallet.GREEN_SEA, "Round Finished.");
                NextRound();
            }
        }

        public static void NextRound()
        {
            turnIndex = 0;
            NextTurn();
        }

        public static void Stop()
        {
            isPlaying = false;
            turnInfo.actors.Clear();
            turnInfo.currentActor = null;
        }

        public static void RegisterActors(ITurnActor[] actors)
        {
            for (int i = 0; i < actors.Length; i++)
                RegisterActor(actors[i]);
        }
        
        public static void RegisterActor(ITurnActor actor)
        {
            turnInfo.actors.Add(actor);
        }

        public static void UnregisterActor(ITurnActor actor)
        {
            if (turnInfo.actors.Contains(actor))
            {
                turnInfo.actors.Remove(actor);
            }
        }
    }

    public struct TurnInfo
    {
        public List<ITurnActor> actors;
        public ITurnActor currentActor;
        public int turnCount => actors.Count;
    }
    
    public class TunrInfoUnityEvent : UnityEvent<TurnInfo>{ }

    public interface ITurnActor
    {
        public void OnBeginTurn(TurnInfo info);

        public void OnEndTurn(TurnInfo info);

        public bool CanPlayTurn(TurnInfo info);
    }
}
