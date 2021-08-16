using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform Enemy;
    public List<Transform> Waypoints;
    public float MovementSpeed;
    public float WaitAtWaypointTime = 2.0f;

    private int _currentWaypointIndex = 0;
    private Animator _animator;

    private bool _isStunned = false;
    private float _timer = 0.0f;

    private void Awake()
    {
        _animator = Enemy.GetComponent<Animator>();

        if (_animator != null)
            _animator.SetBool("IsMoving", true);

        Enemy.position = Waypoints[0].position;

    }
    private void Update()
    {
        if (Enemy == null)
            return;

        if (!_isStunned && (Time.time >= _timer))
            MoveEnemyTowardsCurrentWaypoint(); 
    }

    private void FlipEnemy()
    {
        Vector3 localScale = Enemy.localScale;
        localScale.x *= -1.0f;
        Enemy.localScale = localScale;
    }

    public void Stun(bool value = true)
    {
        _isStunned = value;
        _animator.SetBool("IsStunned", value);
    }

    public void Die()
    {
        _animator.SetBool("IsDead", true);
    }

    private void MoveEnemyTowardsCurrentWaypoint()
    {
        Enemy.position = Vector3.MoveTowards(Enemy.position, Waypoints[_currentWaypointIndex].position, MovementSpeed * Time.deltaTime);

        float distance = Vector3.Distance(Enemy.position, Waypoints[_currentWaypointIndex].position);

        if (distance <= 0.0f)
        {
            _currentWaypointIndex++;

            if((Enemy.gameObject.tag == "SnakeEnemy") || (Enemy.gameObject.tag == "InstantDeath"))
                _timer = Time.time + WaitAtWaypointTime;

            if (Enemy.gameObject.tag != "FlyingEnemy")
                FlipEnemy();

            if (_currentWaypointIndex >= Waypoints.Count)  //index od 0 do N-1, count N
                _currentWaypointIndex = 0;

        }
    }

}
