using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform _target;

    [SerializeField]
    Vector3 _offset;

    [SerializeField]
    float _timeOffset;

    [SerializeField]
    float _rightLimit;
    [SerializeField]
    float _leftLimit;
    [SerializeField]
    float _bottomLimit;
    [SerializeField]
    float _topLimit;

    float _nextTimeToSearch = 2.0f;

    bool _cameraAdjusted;

    [SerializeField]
    float _newOffsetX;
    [SerializeField]
    float _newOffsetY;
    [SerializeField]
    float _newSize;


    private void LateUpdate()
    {
        if (_target == null)
        {
            FindPlayer();

            if (!_cameraAdjusted && GameManager.instance.bossLevel)
               AdjustCamera(_newOffsetX, _newOffsetY, _newSize);

            return;
        }

        Vector3 cameraPos = transform.position;

        Vector3 playerPos = _target.transform.position;
        playerPos.x += _offset.x;
        playerPos.y += _offset.y;
        playerPos.z += _offset.z;

        transform.position = Vector3.Lerp(cameraPos, playerPos, _timeOffset * Time.deltaTime);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, _rightLimit, _leftLimit),
            Mathf.Clamp(transform.position.y, _bottomLimit, _topLimit),
            transform.position.z
        );

    }
    private void FindPlayer()
    {
        if (_nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                _target = searchResult.transform;
            _nextTimeToSearch = Time.time + 0.5f;
        }

    }

    private void AdjustCamera (float x, float y, float size)
    {
        _offset.x = x;
        _offset.y = y;
        GetComponent<Camera>().orthographicSize = size;

        _cameraAdjusted = true;       
    }

}
