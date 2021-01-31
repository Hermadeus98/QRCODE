using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools;
using QRTools.Observer;

public class PoolObjectB : MonoBehaviour, IPoolable, IObservableMessage
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

    private void OnEnable()
    {
        ObserverTest.Instance?.Register(this);
    }

    private void OnDisable()
    {
        ObserverTest.Instance?.Unregister(this);
    }

    public void OnNotify()
    {
        Debug.Log("OBSEVER");
    }
}
