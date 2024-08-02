using System;
using Cinemachine;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.Services
{
    public class CameraService
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        [Inject]
        public CameraService(CinemachineVirtualCamera cinemachineVirtualCamera)
        {
            _cinemachineVirtualCamera = cinemachineVirtualCamera;
        }

        public void UpdateCamera(Transform transform)
        {
            _cinemachineVirtualCamera.m_Follow = transform;
            _cinemachineVirtualCamera.m_LookAt = transform;
        }
        
    }
}