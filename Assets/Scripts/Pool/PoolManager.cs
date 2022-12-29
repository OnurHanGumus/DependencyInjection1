using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
using Enums;
using Zenject;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject particlePrefab;

    [SerializeField] private Dictionary<PoolEnums, List<GameObject>> poolDictionary;


    [SerializeField] private int amountBullets = 50;
    [SerializeField] private int amountParticle = 5;



    #endregion
    #region Private Variables
    #endregion
    #endregion

    [Inject] private PoolSignals PoolSignals { get; set; }
    [InjectAttribute]
    public void Constructor()
    {
        Init();
    }
    private void Awake()
    {
        //Init();
    }
    private void Init()
    {
        poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();
        InitializePool(PoolEnums.Bullet, bulletPrefab, amountBullets);
        //InitializePool(PoolEnums.Particle, particlePrefab, amountParticle);
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

    private void InitializePool(PoolEnums type, GameObject prefab, int size)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = Instantiate(prefab, transform);
            tmp.SetActive(false);
            tempList.Add(tmp);
        }
        poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                return poolDictionary[type][i];
            }
        }
        return null;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }


    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Bullet);
        //ResetPool(PoolEnums.Particle);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
