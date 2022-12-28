using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;

namespace Components.Players
{
    public class PlayerShootManager : MonoBehaviour
    {
        [Inject] private MainSceneInputEvents MainSceneInputEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [Inject] private PoolSignals poolSignals { get; set; }
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
        }

        private void UnRegisterEvents()
        {
            MainSceneInputEvents.onAttackedToEnemy -= OnAttackedToEnemy;
        }

        private void OnAttackedToEnemy(Vector3 targetPos)
        {
            Debug.Log("Attacked to enemy");
            GameObject bullet = poolSignals.onGetObject(PoolEnums.Bullet);
            bullet.SetActive(true);
        }

    }
}