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
            MainSceneInputEvents.onAttackedToEnemy += OnAttackedToEnemy;
            PlayerEvents.onPlayerMove += OnPlayerMove;
            PlayerEvents.onEnemyShooted += OnEnemyShooted;
        }

        private void UnRegisterEvents()
        {
            MainSceneInputEvents.onAttackedToEnemy -= OnAttackedToEnemy;
            PlayerEvents.onPlayerMove -= OnPlayerMove;
            PlayerEvents.onEnemyShooted -= OnEnemyShooted;
        }

        private void OnAttackedToEnemy(Vector3 targetPos)
        {
            Debug.Log("Attacked to enemy");
            GameObject bullet = PoolSignals.onGetObject(PoolEnums.Bullet);
            bullet.transform.position = playerCurrentPos + _mySettings.ShootOffset;
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
        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}