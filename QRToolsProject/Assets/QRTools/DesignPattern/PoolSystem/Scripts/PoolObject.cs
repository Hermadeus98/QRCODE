using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools;

public class PoolObject : MonoBehaviour, IPoolable
{
    public void OnPool()
    {
        Debug.Log(GetType().ToString() + ": I'm Pool");
    }

    public void OnPush()
    {
        Debug.Log(GetType().ToString() + ": I'm Push");
    }
}
