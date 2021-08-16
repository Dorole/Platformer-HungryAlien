using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Object References")]
    public LayerMask GroundLayerMask;
    public Transform GroundCheckTransform;
    public Slider appleSlider;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [Header("Movement Variables")]
    public float movementSpeed = 5.0f;
    public float JumpForce;

    private float _climbVelocity;
    private float _gravityStore;

    private float _movementHorizontal;
    private float _movementVertical;

    private float _currentMovementSpeed;

    private bool _isGrounded = true;
    public bool _isFacingRight = true;

    public bool isDoubleJumpEnabled = false;
    private bool _canDoubleJump;

    [Header("Climbing Variables")]
    public bool IsOnLadder;
    public float ClimbSpeed;

    [Header("Wall Jump")]
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    
    private bool _isWallSliding = false;
    private RaycastHit2D _wallCheckHit;
    private float _jumpTime;
    public bool isWallJumpEnabled = false;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _gravityStore = _rigidbody2D.gravityScale;

        _currentMovementSpeed = movementSpeed;
    }

    private void Start()
    {
        if (appleSlider == null)
            appleSlider = GameObject.FindGameObjectWithTag("AppleBar").GetComponent<Slider>();
    }


    private void Update()
    {
        _movementHorizontal = Input.GetAxisRaw("Horizontal");
        _movementVertical = _rigidbody2D.velocity.y;

        if (_movementHorizontal != 0)
            _animator.SetBool("IsWalking", true);
        else
            _animator.SetBool("IsWalking", false);
        

        _isGrounded = Physics2D.Linecast(transform.position, GroundCheckTransform.position, GroundLayerMask);

        if (_isGrounded)
        {
            _canDoubleJump = true;
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded || _isWallSliding)
            {
                _movementVertical = 0.0f;
                _rigidbody2D.AddForce(Vector2.up * JumpForce);
            } else
            {
                if (_canDoubleJump)
                {
                    if (!isDoubleJumpEnabled)
                        return;

                    _movementVertical = 0.0f;
                    _rigidbody2D.AddForce(Vector2.up * JumpForce);
                    _canDoubleJump = false;
                }
            }
            
        }

        if ((_movementVertical > 0) && Input.GetButtonUp("Jump"))
            _movementVertical = 0.0f;

        _animator.SetBool("IsGrounded", _isGrounded);

        //TOO DIFFICULT?????
        /*if (appleSlider.value < (appleSlider.maxValue / 2))
            _currentMovementSpeed = movementSpeed / 1.5f;
        else
            _currentMovementSpeed = movementSpeed; */


        _rigidbody2D.velocity = new Vector2 (_movementHorizontal * _currentMovementSpeed, _movementVertical);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Platform"), LayerMask.NameToLayer("Player"), _movementVertical > 0.0f);

        if (IsOnLadder)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                _animator.SetBool("IsGrounded", true);
                _animator.SetBool("IsClimbing", true);
                _animator.speed = Mathf.Abs(1);
                _rigidbody2D.gravityScale = 0.0f;
                _climbVelocity = ClimbSpeed * Input.GetAxisRaw("Vertical");

                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _climbVelocity);
            }
            if (Input.GetButtonUp("Vertical"))
            {
                _animator.SetBool("IsGrounded", true);
                _animator.SetBool("IsClimbing", true);
                _animator.speed = 0;
                _rigidbody2D.gravityScale = 0.0f;
                _climbVelocity = ClimbSpeed * Input.GetAxisRaw("Vertical");

                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _climbVelocity);
            }
        }

        if(!IsOnLadder)
        {
            _rigidbody2D.gravityScale = _gravityStore;
            _animator.SetBool("IsClimbing", false);
            
        }

    }

    private void FixedUpdate()
    {
        if (_isFacingRight)
            WallJump(wallDistance);
        else
            WallJump(-wallDistance);

        if (_wallCheckHit && !_isGrounded)
        {
            _isWallSliding = true;
            _jumpTime = Time.time + wallJumpTime;
        } else if (_jumpTime < Time.time)
            _isWallSliding = false;

        if (_isWallSliding)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Clamp(_rigidbody2D.velocity.y, wallSlideSpeed, float.MaxValue));





    }

    private void LateUpdate()
    {
        Vector3 localScale = transform.localScale;

        if (_movementHorizontal > 0.0f)
            _isFacingRight = true;
        else if (_movementHorizontal < 0.0f)
            _isFacingRight = false;

        if ( (_isFacingRight && (localScale.x < 0.0f)) || (!_isFacingRight && (localScale.x > 0.0f)) )
            localScale.x *= -1.0f;

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "MovingPlatform") || (collision.gameObject.tag == "SpawningPlatform") || (collision.gameObject.tag == "TriggeredPlatform"))
            transform.parent = collision.transform;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "MovingPlatform") || (collision.gameObject.tag == "SpawningPlatform") || (collision.gameObject.tag == "TriggeredPlatform"))
            transform.parent = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, GroundCheckTransform.position);
    }

    private void WallJump (float wallDistanceVar)
    {
       if (!isWallJumpEnabled)
            return;

        _wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistanceVar, 0), wallDistance, GroundLayerMask);
    }

}

