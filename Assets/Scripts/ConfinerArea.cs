using UnityEngine;
using Cinemachine;

public class ConfinerArea : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _currentCam;
    [SerializeField] private CinemachineVirtualCamera _exitCam;

    //bug: exit cam in a different confiner (example: if player decides to go back)
    //try: make the main confiner trigger too

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2.0f;

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

        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2.0f;
        
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
