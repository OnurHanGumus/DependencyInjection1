using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;

namespace Components.Players
{
    public class PlayerShootManager : MonoBehaviour
    {
        [Inject] private MainSceneInputEvents MainSceneInputEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [Inject] private PoolSignals PoolSignals { get; set; }

        [SerializeField] Vector3 playerCurrentPos;
        [SerializeField] private Transform bulletHolder;
        private List<IAttackable> _attackedTargets = new();

        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;
        private BulletPool _pool;
        [Inject]
        private PoolManager poolManager;
        [Inject]
        public void Constructor(BulletPool pool)
        {
            _pool = pool;
        }
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
            MainSceneInputEvents.onAttackedToEnemy += OnAttackedToEnemy;
            PlayerEvents.onPlayerMove += OnPlayerMove;
            PlayerEvents.onEnemyShooted += OnEnemyShooted;
            PoolSignals.onRemoveBullet += OnDespawnBullet;
        }

        private void UnRegisterEvents()
        {
            MainSceneInputEvents.onAttackedToEnemy -= OnAttackedToEnemy;
            PlayerEvents.onPlayerMove -= OnPlayerMove;
            PlayerEvents.onEnemyShooted -= OnEnemyShooted;
            PoolSignals.onRemoveBullet -= OnDespawnBullet;
        }

        private void OnAttackedToEnemy(Vector3 targetPos)
        {
            Debug.Log("Attacked to enemy");
            GameObject bullet = poolManager.Spawn(transform.position, PoolEnums.Bullet); /*_pool.SpawnBullet(transform.position).gameObject;*/

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
                attackable.OnDeath += OnAttackedDeath;
            }
        }
        private void OnAttackedDeath(IAttackable diedAttackable)
        {
            diedAttackable.OnDeath -= OnAttackedDeath;
            _attackedTargets.Remove(diedAttackable);
            Debug.LogWarning("Target Died");
        }

        private void OnDespawnBullet(BulletCollisionDetector bulletCollisionDetector)
        {
            //_pool.RemoveBullet(bulletCollisionDetector);
            poolManager.Remove(bulletCollisionDetector, PoolEnums.Bullet);
        }
        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}