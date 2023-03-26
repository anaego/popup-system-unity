using System;
using UnityEngine.Pool;

public class PopupPool
{
    private ObjectPool<PopupController> objectPool;

    public int ActivePopups => objectPool.CountActive;

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
