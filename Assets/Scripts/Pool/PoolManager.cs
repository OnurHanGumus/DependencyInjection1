using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;

public class PoolManager : MonoBehaviour
{
    [Inject] private PoolHolder PoolHolder { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetObject += Spawn;
        PoolSignals.onRemove += Remove;

        CoreGameSignals.onRestartLevel += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetObject -= Spawn;
        PoolSignals.onRemove -= Remove;

        CoreGameSignals.onRestartLevel -= OnRestartLevel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void Start()
    {
        StartCoroutine(ResetPool());

    }

    private GameObject Spawn(PoolEnums poolEnum, Vector2 spawnPos)
    {
        return PoolHolder.PoolDictionary[poolEnum].Spawn(spawnPos);

    }

    private void Remove(PoolEnums poolEnum, IPoolType type)
    {
        PoolHolder.PoolDictionary[poolEnum].Despawn(type);
    }

    private void OnRestartLevel()
    {
        PoolHolder.Reset();
    }

    private IEnumerator ResetPool()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Resetted.");

        CoreGameSignals.onRestartLevel?.Invoke();
    }
}
