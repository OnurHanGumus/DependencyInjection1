using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;

public class PoolManager : MonoBehaviour
{
    [Inject] private PoolHolder _poolHolder { get; set; }
    [Inject] private PoolSignals poolSignals { get; set; }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        poolSignals.onGetObject += Spawn;
        poolSignals.onRemove += Remove;
    }

    private void UnsubscribeEvents()
    {
        poolSignals.onGetObject -= Spawn;
        poolSignals.onRemove -= Remove;
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
