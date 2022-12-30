using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
using Enums;
using Zenject;

public class BulletSpawner : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables


    #endregion
    #region Private Variables
    private BulletPool _pool;
    #endregion
    #endregion

    [Inject] private PoolSignals PoolSignals { get; set; }

    [Inject]
    public void Constructor(BulletPool pool)
    {
        _pool = pool;
    }
    private void Awake()
    {

    }



    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetPoolManagerObj += OnGetPoolManagerObj;
        PoolSignals.onGetObject += OnGetObject;
        //CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetPoolManagerObj -= OnGetPoolManagerObj;
        PoolSignals.onGetObject -= OnGetObject;
        //CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public GameObject OnGetObject(PoolEnums type)
    {
        var bullet = _pool.SpawnEnemy(transform.position);
        return bullet.gameObject;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }


    private void OnReset()
    {

    }

    private void ResetPool(PoolEnums type)
    {

    }
}
