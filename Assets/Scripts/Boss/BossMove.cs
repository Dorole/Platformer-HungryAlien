using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FEEDBACK: zasto koristis StateMachineBehaviour?
// obicno ako trebas state design patter onda ga bolje implementirat sam
// https://www.dofactory.com/net/state-design-pattern
public class BossMove : StateMachineBehaviour
{
    private float _speed;
    public float startingSpeed = 4.0f;
    public float damagedSpeed = 6.0f;

    private float _attackRange;
    public float startingRange = 5.0f;
    public float damagedRange = 7.5f;

    private Transform _player;
    private Rigidbody2D _rb;
    private Boss _boss;
    private BossHealth _bossHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _player = _boss.player;
        _rb = animator.GetComponent<Rigidbody2D>();
        _bossHealth = animator.GetComponent<BossHealth>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_bossHealth.health <= 0)
            return;

        if (_player == null)
        {
            _boss.FindPlayer();
            _player = _boss.player;
            return;
        }

        _boss.LookAtPlayer();

        //ne radi ako mijenjam transform animacije u Unityju, zasto?
        //FEEDBACK: moguce da animacija ima postavljene pozicije pa ih overridea
        Vector2 target = new Vector2(_player.position.x, _rb.position.y);

        if (_bossHealth.health >= 30)
        {
            _speed = startingSpeed;
            _attackRange = startingRange;
        }
        else
        {
            _speed = damagedSpeed;
            _attackRange = damagedRange;
        }

        Vector2 newPos = Vector2.MoveTowards(_rb.position, target, _speed * Time.fixedDeltaTime);

        float distanceFromPlayer = Vector2.Distance(_player.position, _rb.position);

        if (distanceFromPlayer > _attackRange)
            _rb.MovePosition(newPos);

        if (distanceFromPlayer <= _attackRange)
            _boss.AttackPlayer();

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
