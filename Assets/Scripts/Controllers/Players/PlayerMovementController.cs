using Data.MetaData;
using Events.External;
using Extensions.Unity;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private RoutineHelper _onPosUpdate;
        private bool _isStarted = false;
        private Settings _mySettings;
        private void OnEnable()
        {
            RegisterEvents();

            _onPosUpdate = new RoutineHelper
            (this, null, OnPosUpdate, () => true);
        }
        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerMovementSettings;
        }
        private void OnPosUpdate()
        {
            PlayerSignals.onPlayerMove?.Invoke(_myTransform.position);

            if (_navMeshAgent.isStopped)
            {
                _onPosUpdate.StopCoroutine();
            }
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            InputSignals.onInputUpdate += OnInputUpdate;
            PlayerSignals.onAttackedToEnemy += OnAttackToEnemy;
        }

        private void OnInputUpdate(InputSignals.InputUpdate inputUpdate)
        {
            if (!_isStarted)
            {
                return;
            }
            if (_onPosUpdate.IsInvoking == false)
            {
                _navMeshAgent.isStopped = false;

                _onPosUpdate.StartCoroutine();
            }

            //Debug.LogWarning(MethodBase.GetCurrentMethod().Name);

            _navMeshAgent.destination = inputUpdate.TerrainPos;
            _navMeshAgent.speed = _mySettings.Speed;
        }

        private void UnRegisterEvents()
        {
            InputSignals.onInputUpdate -= OnInputUpdate;
            PlayerSignals.onAttackedToEnemy -= OnAttackToEnemy;
        }

        public void OnPlay()
        {
            _isStarted = true;
        }
        public void OnRestartLevel()
        {
            _isStarted = false;
        }
        private void OnAttackToEnemy(Vector3 targetPos)
        {
            _navMeshAgent.isStopped = true;
            transform.LookAt(targetPos);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}