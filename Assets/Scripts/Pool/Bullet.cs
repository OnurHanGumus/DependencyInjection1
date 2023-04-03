using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
public class Bullet : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject]
    public void Construct(PoolSignals poolSignals, CoreGameSignals coreGameSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
    }
    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestartLevel += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestartLevel -= OnRestartLevel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
    public class Factory : PlaceholderFactory<Bullet>, IPool
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }


    //public class Pool : MemoryPool<Vector2, Bullet>, IPool
    //{
    //    public void Despawn(IPoolType bullet)
    //    {
    //        base.Despawn((Bullet) bullet);
    //    }

    //    public new GameObject Spawn(Vector2 pos)
    //    {
    //        return base.Spawn(pos).gameObject;
    //    }
    //    public void GetObject()
    //    {
    //        for (int i = 0; i < base.NumActive; i++)
    //        {
    //            base.GetInternal().gameObject.SetActive(false);
    //        }

    //    }
    //}
}
