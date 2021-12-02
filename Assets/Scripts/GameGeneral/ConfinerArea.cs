using UnityEngine;
using Cinemachine;

public class ConfinerArea : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _currentCam;
    [SerializeField] private CinemachineVirtualCamera _exitCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        CinemachineVirtualCamera cam = GameManager.instance.virtualCamera;
        cam.Priority = 1;

        if (_currentCam.transform.CompareTag("SecondaryCamera"))
            _currentCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;

        _currentCam.Priority = 2;
        GameManager.instance.virtualCamera = _currentCam;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        if (_exitCam == null)
            return;
        else
        {
            _exitCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;

            _currentCam.Priority = 1;
            _exitCam.Priority = 2;
            GameManager.instance.virtualCamera = _exitCam;
        }
    }
}
