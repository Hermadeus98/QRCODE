using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameplayFramework
{
    // http://wiki.unity3d.com/index.php/CSharpMessenger_Extended
    public static class GF_EventInternal
    {
        readonly public static Dictionary<GF_EVENT_ENUM, Delegate> eventsTable = new Dictionary<GF_EVENT_ENUM, Delegate>();
        static public GF_MESSENGER_MODE DEFAULT_MODE = GF_MESSENGER_MODE.REQUIRE_LISTENER;

        public static void AddListener(GF_EVENT_ENUM adress, Delegate callback)
        {
            OnListenerAdding(adress, callback);
            eventsTable[adress] = Delegate.Combine(eventsTable[adress], callback);
        }

        public static void RemoveListener(GF_EVENT_ENUM adress, Delegate handler)
        {
            OnListenerRemoving(adress, handler);
            eventsTable[adress] = Delegate.Remove(eventsTable[adress], handler);
            OnListenerRemoved(adress);   
        }

        public static T[] GetInvocationList<T>(GF_EVENT_ENUM adress)
        {
            if (eventsTable.TryGetValue(adress, out var d))
            {
                try
                {
                    return d.GetInvocationList().Cast<T>().ToArray();
                }
                catch
                {
                    throw CreateBroadcastSignatureException(adress);
                }
            }

            return new T[0];
        }

        static void OnListenerAdding(GF_EVENT_ENUM adress, Delegate listenerBeingAdded)
        {
            if (!eventsTable.ContainsKey(adress))
                eventsTable.Add(adress, null);

            var d = eventsTable[adress];
            if(d != null && d.GetType() != listenerBeingAdded.GetType())
                throw new ListenerException(
                    string.Format(
                        "Attempting to add listener with inconsistent signature for event type {0}." +
                        " Current listeners have type {1} and listener being added has type {2}",
                        adress,
                        d.GetType().Name,
                        listenerBeingAdded.GetType().Name));
        }

        static void OnListenerRemoving(GF_EVENT_ENUM adress, Delegate listenerBeingRemoved)
        {
            if (eventsTable.ContainsKey(adress))
            {
                var d = eventsTable[adress];
                if(d == null)
                {
                    throw new ListenerException(
                        string.Format(
                            "Attempting to remove listener with for event type {0} but current listener is null.",
                            adress));
                }
                else if(d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(
                        string.Format(
                            "Attempting to remove listener with inconsistent signature for event type {0}." +
                            " Current listeners have type {1} and listener being removed has type {2}",
                            adress,
                            d.GetType().Name,
                            listenerBeingRemoved.GetType().Name));
                }
            }
            else
            {
                throw new ListenerException(
                    string.Format(
                        "Attempting to remove listener for type {0} but Messenger doesn't know about this event type."
                        , adress));
            }
        }

        static void OnListenerRemoved(GF_EVENT_ENUM adress)
        {
            if(eventsTable[adress] == null)
            {
                eventsTable.Remove(adress);
            }
        }

        static public void OnBroadcasting(GF_EVENT_ENUM adress, GF_MESSENGER_MODE mode)
        {
            if (mode == GF_MESSENGER_MODE.REQUIRE_LISTENER && !eventsTable.ContainsKey(adress))
            {
                throw new BroadcastException(
                    string.Format(
                        "Broadcasting message {0} but no listener found.",
                        adress));
            }
        }

        static public BroadcastException CreateBroadcastSignatureException(GF_EVENT_ENUM adress)
        {
            return new BroadcastException(
                string.Format(
                    "Broadcasting message {0} but listeners have a different signature than the broadcaster."
                    , adress));
        }

        public class BroadcastException : Exception
        {
            public BroadcastException(string msg)
                : base(msg)
            {
            }
        }

        public class ListenerException : Exception
        {
            public ListenerException(string msg)
                : base(msg)
            {
            }
        }
    }

    static public class GF_Event
    {
        static public void AddListener(GF_EVENT_ENUM adress, Action handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void AddListener<TReturn>(GF_EVENT_ENUM adress, Func<TReturn> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void RemoveListener(GF_EVENT_ENUM adress, Action handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void RemoveListener<TReturn>(GF_EVENT_ENUM adress, Func<TReturn> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void Broadcast(GF_EVENT_ENUM adress)
        {
            Broadcast(adress, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, Action<TReturn> returnCall)
        {
            Broadcast(adress, returnCall, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Action>(adress);

            foreach (var callback in invocationList)
                callback.Invoke();
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, Action<TReturn> returnCall, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Func<TReturn>>(adress);

            foreach (var result in invocationList.Select(del => del.Invoke()).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    // One parameter
    static public class GF_Event<T>
    {
        static public void AddListener(GF_EVENT_ENUM adress, Action<T> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void AddListener<TReturn>(GF_EVENT_ENUM adress, Func<T, TReturn> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void RemoveListener(GF_EVENT_ENUM adress, Action<T> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void RemoveListener<TReturn>(GF_EVENT_ENUM adress, Func<T, TReturn> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1)
        {
            Broadcast(adress, arg1, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(adress, arg1, returnCall, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Action<T>>(adress);

            foreach (var callback in invocationList)
                callback.Invoke(arg1);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, Action<TReturn> returnCall, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Func<T, TReturn>>(adress);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    // Two parameters
    static public class GF_Event<T, U>
    {
        static public void AddListener(GF_EVENT_ENUM adress, Action<T, U> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void AddListener<TReturn>(GF_EVENT_ENUM adress, Func<T, U, TReturn> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void RemoveListener(GF_EVENT_ENUM adress, Action<T, U> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void RemoveListener<TReturn>(GF_EVENT_ENUM adress, Func<T, U, TReturn> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1, U arg2)
        {
            Broadcast(adress, arg1, arg2, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(adress, arg1, arg2, returnCall, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1, U arg2, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Action<T, U>>(adress);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, U arg2, Action<TReturn> returnCall, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Func<T, U, TReturn>>(adress);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }


    // Three parameters
    static public class GF_Event<T, U, V>
    {
        static public void AddListener(GF_EVENT_ENUM adress, Action<T, U, V> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void AddListener<TReturn>(GF_EVENT_ENUM adress, Func<T, U, V, TReturn> handler)
        {
            GF_EventInternal.AddListener(adress, handler);
        }

        static public void RemoveListener(GF_EVENT_ENUM adress, Action<T, U, V> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void RemoveListener<TReturn>(GF_EVENT_ENUM adress, Func<T, U, V, TReturn> handler)
        {
            GF_EventInternal.RemoveListener(adress, handler);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1, U arg2, V arg3)
        {
            Broadcast(adress, arg1, arg2, arg3, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(adress, arg1, arg2, arg3, returnCall, GF_EventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(GF_EVENT_ENUM adress, T arg1, U arg2, V arg3, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Action<T, U, V>>(adress);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2, arg3);
        }

        static public void Broadcast<TReturn>(GF_EVENT_ENUM adress, T arg1, U arg2, V arg3, Action<TReturn> returnCall, GF_MESSENGER_MODE mode)
        {
            GF_EventInternal.OnBroadcasting(adress, mode);
            var invocationList = GF_EventInternal.GetInvocationList<Func<T, U, V, TReturn>>(adress);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2, arg3)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    public enum GF_MESSENGER_MODE
    {
        DONT_REQUIRE_LISTENER,
        REQUIRE_LISTENER
    }

    public enum GF_EVENT_ENUM
    {
        GF_EVENT_TEST_01,
        GF_EVENT_TEST_02
    }
}