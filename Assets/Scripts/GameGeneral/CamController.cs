using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera1;
    [SerializeField]
    private CinemachineVirtualCamera _camera2;
    [SerializeField]
    private CinemachineVirtualCamera _camera3;
    
    private bool _switchArea = true;

    private void Start()
    {
        GameEvents.instance.onCameraSwitchEnter += SwitchPriority;
    }

    private void SwitchPriority()
    {
        if (_switchArea)
        {
            _camera1.Priority = 1;
            _camera2.Priority = 2;
            GameManager.instance.virtualCamera = _camera2;
            Debug.Log("cam2");
        }
        else
        {
            _camera3.Priority = 2;
            _camera2.Priority = 0;
            GameManager.instance.virtualCamera = _camera3;
            Debug.Log("cam3");
        }

        _switchArea = !_switchArea;

    }
}
