using Components.Enemies;
using Events.External;
using Extensions.Unity;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;

namespace Components
{
    public class InputManagerPoyrazHoca : MonoBehaviour
    {
        private RoutineHelper _inputRoutine;
        private bool _isMoveable;
        private Vector3 _lastInputPos;
        private Vector3 _mousePosDelta;
        private Camera _mainCam;

        [UsedImplicitly]
        [Inject] private InputSignalsPoyrazHoca InputSignalsPoyrazHoca { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        private Ray _ray;

        private void Awake()
        {
            _mainCam = Camera.main;
        }
        private void Update()
        {
            InputUpdate();
        }
        private void OnEnable()
        {
            //_inputRoutine.StartCoroutine();
        }

        private void OnDisable()
        {
            //_inputRoutine.StartCoroutine();
        }

        private void InputUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);

                _ray = Camera.main.ScreenPointToRay(mousePos);

                RaycastHit hit;
                if (Physics.Raycast(_ray, out hit, 100f))
                {
                    if (hit.collider.TryGetComponent(out IAttackable attackable))
                    {
                        Vector3 targetPos = hit.transform.position;
                        PlayerSignals.onAttackedToEnemy?.Invoke(targetPos);
                    }
                    else
                    {
                        _isMoveable = true;
                        InputSignalsPoyrazHoca.onInputBegin?.Invoke();
                    }
                }
            }
            else if (_isMoveable)
            {
                if (TryGetTerrainInputPos(Input.mousePosition, out Vector3 terrainInputPos))
                {
                    InputSignalsPoyrazHoca.InputUpdate inputUpdate = new(terrainInputPos);

                    InputSignalsPoyrazHoca.onInputUpdate?.Invoke(inputUpdate);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isMoveable = false;
                InputSignalsPoyrazHoca.onInputEnd?.Invoke();

            }
        }

        private bool TryGetTerrainInputPos(Vector3 currMousePos, out Vector3 terrainInputPos)
        {
            Ray inputRay = _mainCam.ScreenPointToRay(currMousePos);

            Debug.DrawRay(inputRay.origin, inputRay.direction);

            bool didInputRayHit = Physics.Raycast(inputRay, out RaycastHit inputRayHit);

            if (didInputRayHit)
            {
                if (inputRayHit.collider.gameObject.TryGetComponent(out Terrain _))
                {
                    terrainInputPos = inputRayHit.point;
                    return true;
                }
            }

            terrainInputPos = new Vector3();
            return false;
        }
    }
}