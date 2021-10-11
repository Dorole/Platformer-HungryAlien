using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera1;
    [SerializeField]
    private CinemachineVirtualCamera _camera2;
    [SerializeField]
    private bool _defaultCameraActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwitchPriority();
    }

    private void SwitchPriority()
    {
        if (_defaultCameraActive)
        {
            _camera1.Priority = 1;
            _camera2.Priority = 2;
            GameManager.instance.virtualCamera = _camera2;
        } 
        else
        {
            _camera1.Priority = 2;
            _camera2.Priority = 1;
            GameManager.instance.virtualCamera = _camera1;
        }
    }
}
