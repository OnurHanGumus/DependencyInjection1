using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
using Enums;
using Zenject;

public class EnemySpawnManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables


    #endregion
    #region Private Variables
    private EnemyPool _pool;
    #endregion
    #endregion

    [Inject] private PoolSignals PoolSignals { get; set; }

    [Inject]
    public void Constructor(EnemyPool pool)
    {
        _pool = pool;
    }
    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5f);
        GameObject enemy = OnGetObject(PoolEnums.Bullet);
        enemy.transform.position = transform.position;
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onRemoveEnemy += OnDespawn;
        //CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onRemoveEnemy -= OnDespawn;
        //CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public GameObject OnGetObject(PoolEnums type)
    {
        var enemy = _pool.SpawnEnemy(transform.position);

        return enemy.gameObject;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }
    public void OnDespawn(Enemy enemy)
    {
        _pool.RemoveEnemy(enemy);
    }


    private void OnReset()
    {

    }

    private void ResetPool(PoolEnums type)
    {

    }
}
