using System;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;

namespace Components.Players
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;

        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerCameraControllerSettings;
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
            PlayerEvents.onPlayerMove += OnPlayerMove;
            //MainSceneEvents.GameLoaded += OnGameLoaded;
        }

        //private void OnGameLoaded(){
        //loadPlayer
        //}
        
        private void OnPlayerMove(Vector3 playerPos)
        {
            _myTransform.position = playerPos + _mySettings.CameraOffset;
        }

        private void UnRegisterEvents()
        {
            PlayerEvents.onPlayerMove -= OnPlayerMove;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 CameraOffset;
        }
    }
}
