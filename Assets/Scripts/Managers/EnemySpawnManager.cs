using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
using Enums;
using Zenject;
using Signals;
using System.Threading.Tasks;

public class EnemySpawnManager: ITickable
{
    #region Self Variables
    #region Injected Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private bool isStarted = false;

    #endregion
    #region Private Variables
    private List<Vector3> _spawnPoints;
    private PoolSignals PoolSignals { get; set; }
    private CoreGameSignals CoreGameSignals { get; set; }
    private float _enemySpawnDelay = 3, _timer;

    #endregion
    #endregion

    public EnemySpawnManager(PoolSignals poolSignals, CoreGameSignals coreGameSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
        Awake();
    }

    private void Awake()
    {
        Init();
        SubscribeEvents();
    }

    private void Init()
    {
        _spawnPoints = new List<Vector3>() { new Vector3(0,0,30), new Vector3(5, 0, 32), new Vector3(8, 0, 35) };
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
        isStarted = true;
    }
    private void OnLevelSuccessful()
    {
        isStarted = false;
    }
    private void OnLevelFailed()
    {
        isStarted = false;
    }

    public void Tick()
    {
        if (!isStarted)
        {
            return;
        }
       
        _timer -= (Time.deltaTime);
        if (_timer > 0)
        {
            return;
        }
        
        GameObject enemy = PoolSignals.onGetObject(PoolEnums.Enemy, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
        enemy.SetActive(true);
        _timer = _enemySpawnDelay;
    }
}
