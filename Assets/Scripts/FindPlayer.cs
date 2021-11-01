using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private float _nextTimeToSearch = 0.5f;

    private void Start()
    {
        _cam = GameManager.instance.virtualCamera;
    }

    private void LateUpdate()
    {
        if (_cam != GameManager.instance.virtualCamera)
            _cam = GameManager.instance.virtualCamera;

        if (_cam.Follow == null)
        {
            if (_cam.gameObject.CompareTag("StaticCam"))
                return;

            FindTarget();
            return;
        }
    }

    private void FindTarget()
    {
        if (_nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");

            GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

            if (searchResult != null)
                _cam.Follow = searchResult.transform;

            _nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
