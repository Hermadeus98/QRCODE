using UnityEngine.Events;
using UnityEngine;
namespace GameplayFramework
{
    [System.Serializable]
    public class FloatUnityEvent : UnityEvent<float> { }

    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int> { }

    [System.Serializable]
    public class BoolUnityEvent : UnityEvent<bool> { }

    [System.Serializable]
    public class Vector2UnityEvent : UnityEvent<Vector2> { }

    [System.Serializable]
    public class Vector2IntUnityEvent : UnityEvent<Vector2Int> { }

    [System.Serializable]
    public class Vector3UnityEvent : UnityEvent<Vector3> { }

    [System.Serializable]
    public class Vector3IntUnityEvent : UnityEvent<Vector3Int> { }

    [System.Serializable]
    public class QuaternionUnityEvent : UnityEvent<Quaternion> { }
}
