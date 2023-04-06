using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;
using Signals;

namespace Controllers
{
    public class PlayerShootController : MonoBehaviour
    {
        [Inject] private InputSignalsPoyrazHoca InputSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private PoolSignals PoolSignals { get; set; }

        [SerializeField] Vector3 playerCurrentPos;
        [SerializeField] private Transform bulletHolder;
        private List<IAttackable> _attackedTargets = new();

        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;

        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerShootManagerSettings;
        }
        private void OnEnable()
        {
            RegisterEvents();
        }
        private void OnDisable()
        {
            UnRegisterEvents();
        }
        private void RegisterEvents()
        {
            PlayerSignals.onAttackedToEnemy += OnAttackedToEnemy;
            PlayerSignals.onPlayerMove += OnPlayerMove;
            PlayerSignals.onEnemyShooted += OnEnemyShooted;
        }

        private void UnRegisterEvents()
        {
            PlayerSignals.onAttackedToEnemy -= OnAttackedToEnemy;
            PlayerSignals.onPlayerMove -= OnPlayerMove;
            PlayerSignals.onEnemyShooted -= OnEnemyShooted;
        }

        private void OnAttackedToEnemy(Vector3 targetPos)
        {
            Debug.Log("Attacked to enemy");
            GameObject bullet = PoolSignals.onGetObject(PoolEnums.Bullet,transform.position); /*_pool.SpawnBullet(transform.position).gameObject;*/

            bullet.SetActive(false);
            bullet.transform.position = bulletHolder.transform.position;
            bullet.transform.LookAt(targetPos);
            bullet.transform.eulerAngles = new Vector3(0, bullet.transform.eulerAngles.y, bullet.transform.eulerAngles.z);
            bullet.SetActive(true);
        }
        private void OnPlayerMove(Vector3 currentPos)
        {
            playerCurrentPos = currentPos;
        }

        private void OnEnemyShooted(IAttackable attackable)
        {
            if (!_attackedTargets.Contains(attackable))
            {
                _attackedTargets.Add(attackable);
                attackable.GetInternalEvents().OnDeath += OnAttackedDeath;
            }
        }
        private void OnAttackedDeath(IAttackable diedAttackable)
        {
            diedAttackable.GetInternalEvents().OnDeath -= OnAttackedDeath;
            _attackedTargets.Remove(diedAttackable);
            Debug.LogWarning("Target Died");
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}