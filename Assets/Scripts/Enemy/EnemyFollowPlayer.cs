using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 10.0f;
    public float minimalDistance = 2.0f;

    private Transform _player;
    private float _nextTimeToSearch = 2.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void FixedUpdate()
    {
        if (_player == null)
        {
            FindPlayer();
            return;
        }

        if (Vector2.Distance(transform.position, _player.position) > minimalDistance)
            transform.position= Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);

    }

    private void FindPlayer()
    {
        if (_nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                _player = searchResult.transform;
            _nextTimeToSearch = Time.time + 0.5f;
        }

    }
}
