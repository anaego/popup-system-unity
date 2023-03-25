using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PopupPool 
{
    private ObjectPool<PopupController> objectPool;

    public PopupPool(
        Func<PopupController> createFunc, 
        Action<PopupController> actionOnGet, 
        Action<PopupController> actionOnRelease, 
        int defaultCapacity)
    {
        objectPool = new ObjectPool<PopupController>(
            createFunc, actionOnGet, actionOnRelease, defaultCapacity: defaultCapacity);
    }

    public PopupController GetObjectFromPool()
    {
        return objectPool.Get();
    }

    public void ReleaseObjectToPool(PopupController popupToRelease)
    {
        objectPool.Release(popupToRelease);
    }
}
