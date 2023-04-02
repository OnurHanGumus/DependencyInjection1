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

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
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
    }

    private void UnsubscribeEvents()
    {
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
}
