using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform Platform;
    public List<Transform> Waypoints;

    public float MovementSpeed = 5.0f;
    public float WaitAtWaypointTime = 0.5f;

    public bool shouldLoop = true;

    private int _currentWaypointIndex = 0;

    private float _timer = 0.0f;

    public bool shouldMove; 

    private void Awake()
    {
        Platform.position = Waypoints[0].position;
        if (Platform.tag == "TriggeredPlatform")
            shouldMove = false;
        else
            shouldMove = true;
    }

    private void Update()
    {
        if((shouldMove) && (Time.time >= _timer))
            MovePlatformTowardsCurrentWaypoint();
        
    }

    private void MovePlatformTowardsCurrentWaypoint()
    {
        Platform.position = Vector3.MoveTowards(Platform.position, Waypoints[_currentWaypointIndex].position, MovementSpeed * Time.deltaTime);

        float distance = Vector3.Distance(Platform.position, Waypoints[_currentWaypointIndex].position);

        if (distance <= 0.0f)
        {
            _currentWaypointIndex++;
            _timer = Time.time + WaitAtWaypointTime;

            if (_currentWaypointIndex >= Waypoints.Count)  //index od 0 do N-1, count N
            {
                if (Platform.gameObject.tag == "SpawningPlatform")
                    Destroy(gameObject);

                if (shouldLoop)
                    _currentWaypointIndex = 0;
                else
                    shouldMove = false;
            }
        }
    }

}
