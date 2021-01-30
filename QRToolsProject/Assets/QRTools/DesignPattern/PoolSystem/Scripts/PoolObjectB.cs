using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools;

public class PoolObjectB : MonoBehaviour, IPoolable
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PoolingManager.Instance.Push(this);
        }
    }

    public void OnPool()
    {
        Debug.Log(GetType().ToString() + ": I'm Pool");
    }

    public void OnPush()
    {
        Debug.Log(GetType().ToString() + ": I'm Push");
    }
}
