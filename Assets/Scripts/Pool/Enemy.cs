using Enums;
using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
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
        PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, this);

    }
    #endregion

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
    public class Pool : MemoryPool<Vector2, Enemy>, IPool
    {
        public void Despawn(IPoolType enemy)
        {
            base.Despawn((Enemy) enemy);
        }

        public void GetObject()
        {
            for (int i = 0; i < base.NumActive; i++)
            {
                //base.GetInternal().gameObject.SetActive(false);
                
            }
        }

        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
