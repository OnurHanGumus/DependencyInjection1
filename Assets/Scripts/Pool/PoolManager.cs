using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;

public class PoolManager : MonoBehaviour
{
    [Inject] private PoolHolder _poolHolder { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetObject += Spawn;
        PoolSignals.onRemove += Remove;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetObject -= Spawn;
        PoolSignals.onRemove -= Remove;
    }

    public GameObject Spawn(PoolEnums poolEnum, Vector2 spawnPos)
    {
        return _poolHolder.PoolDictionary[poolEnum].Spawn(spawnPos);
    }

    public void Remove(PoolEnums poolEnum, IPoolType type)
    {
        _poolHolder.PoolDictionary[poolEnum].Despawn(type);
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

}
