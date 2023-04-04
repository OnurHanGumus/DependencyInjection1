using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
using Enums;
using Zenject;
using Signals;

public class EnemySpawnManager : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion

    #region Serialized Variables
    [SerializeField] private List<Vector3> spawnPoints;

    #endregion
    #region Private Variables
    #endregion
    #endregion

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            GameObject enemy = PoolSignals.onGetObject(PoolEnums.Enemy, spawnPoints[Random.Range(0, spawnPoints.Count)]);
            enemy.SetActive(true);
        }

    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onPlay += OnPlay;
        CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.onLevelFailed += OnLevelFailed;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= OnPlay;
        CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.onLevelFailed -= OnLevelFailed;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnPlay()
    {
        StartCoroutine(Spawn());
    }
    private void OnLevelSuccessful()
    {
        StopAllCoroutines();
    }
    private void OnLevelFailed()
    {
        StopAllCoroutines();
    }
}
