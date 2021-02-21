using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework {
    public static class GF_InputManager
    {
        private static Dictionary<GF_INPUT_EVENT, GF_Input> inputs = new Dictionary<GF_INPUT_EVENT, GF_Input>();

        internal class GF_InputClassInternal { public GF_INPUT_EVENT evnt; public GF_Input input;}
        private static Dictionary<int, GF_InputClassInternal> playersInputs = new Dictionary<int, GF_InputClassInternal>();

        public static GF_Input AddInput(GF_INPUT_EVENT evnt, GF_Input input)
        {
            if (!inputs.ContainsKey(evnt))
                inputs.Add(evnt, input);
            return input;
        }

        public static GF_Input AddPlayerInput(int index, GF_INPUT_EVENT evnt, GF_Input input)
        {
            if (!playersInputs.ContainsKey(index))
            {
                playersInputs.Add(index, new GF_InputClassInternal
                {
                    evnt = evnt,
                    input = input
                });
            }
            return input;
        }

        public static void RemoveInput(GF_INPUT_EVENT evnt)
        {
            if (inputs.ContainsKey(evnt))
                inputs.Remove(evnt);
        }

        public static void RemovePlayerInput(int index)
        {
            if (playersInputs.ContainsKey(index))
                playersInputs.Remove(index);
        }

        public static T GetInput<T>(GF_INPUT_EVENT evnt) where T : GF_Input
        {
            inputs.TryGetValue(evnt, out var i);
            if (i == null)
                throw new InputException(string.Format(
                    "No input founded at the {0} key.",
                    evnt
                    ));

            return (T)i;
        }

        public static T GetPlayerInput<T>(int index, GF_INPUT_EVENT evnt) where T : GF_Input
        {
            playersInputs.TryGetValue(index, out var i);
            if (i == null)
                throw new InputException(string.Format(
                    "No input founded at the {0} key.",
                    evnt
                    ));

            return (T)i.input;
        }
    }

    public class InputException : System.Exception
    {
        public InputException(string msg)
                : base(msg)
        {
        }
    }

    public enum GF_INPUT_EVENT
    {
        TEST_01,
        TEST_02
    }
}