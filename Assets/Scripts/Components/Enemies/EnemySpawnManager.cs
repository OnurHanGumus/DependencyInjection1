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
    [Inject] private PoolManager PoolManager { get; set; }

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
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameObject enemy = OnGetObject();
            enemy.SetActive(false);
            enemy.transform.position = transform.position;
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

    public GameObject OnGetObject()
    {
        var enemy = PoolManager.Spawn(transform.position, PoolEnums.Enemy);

        return enemy.gameObject;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }
    public void OnDespawn(Enemy enemy)
    {
        //_pool.Remove(enemy);
        PoolManager.Remove(enemy, PoolEnums.Enemy);
    }


    private void OnReset()
    {

    }

    private void ResetPool(PoolEnums type)
    {

    }
}
