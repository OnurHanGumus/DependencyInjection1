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
    #endregion
    #endregion

    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private PoolHolder PoolManager { get; set; }

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameObject enemy = PoolSignals.onGetObject(PoolEnums.Enemy, transform.position);
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
        //PoolSignals.onRemove += OnDespawn;
        //CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        //PoolSignals.onRemove -= OnDespawn;
        //CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public GameObject OnGetObject()
    {
        var enemy = PoolSignals.onGetObject(PoolEnums.Enemy, transform.position);

        return enemy.gameObject;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }
    public void OnDespawn(Enemy enemy)
    {
        //_pool.Remove(enemy);
        PoolSignals.onRemove(PoolEnums.Enemy, enemy);
    }


    private void OnReset()
    {

    }

    private void ResetPool(PoolEnums type)
    {

    }
}
