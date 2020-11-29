using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveInput;

    public float Speed;
    public bool _isGrounded;
    public Transform GroundCheck;
    public Transform GroundCheck2;
    public Transform GroundCheck3;
    public float CheckRadius;
    public LayerMask GroundLayer;
    public LayerMask HazardLayer;
    public float JumpForce;
    public float JumpTime;
    private float _jumpTimeCounter;
    public bool _isJumping;
    public float SlamSpeed;
    private Animator _animator;
    public GameObject BloodSplash;
    public Transform HeadTransform;
    public Transform GlobalTransform;
    public AudioSource Footsteps;
    public LayerMask BossGround;

    public TrailRenderer TrailRenderer;

    private float _hitTimerStart = 0.5f;
    private float _hitTimer;

    private GameStateManager _gameStateManager;
    // public UIManager UIManager;
    // public RipplePostProcessor Ripple;

    private bool _isSlamming;

    private float _heightReached;

    private bool _isRunning;

    public AudioSource HitSound;

    // Start is called before the first frame update
    void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _jumpTimeCounter = JumpTime;
        _isJumping = true;
        _hitTimer = _hitTimerStart;
        _heightReached = transform.position.y;
        // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStateManager.DisableMovement)
        {
            Footsteps.Stop();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetBool("Attacking", true);
            StartCoroutine(SetAttacking());
        }

        if (_isGrounded && !_isJumping && TrailRenderer.enabled)
        {
            TrailRenderer.enabled = false;
        }

        // INITIATE JUMPING
        _isGrounded = (Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer) ||
                       Physics2D.OverlapCircle(GroundCheck2.position, CheckRadius, GroundLayer) ||
                       Physics2D.OverlapCircle(GroundCheck3.position, CheckRadius, GroundLayer)) ||
                      (Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, HazardLayer) ||
                       Physics2D.OverlapCircle(GroundCheck2.position, CheckRadius, HazardLayer) ||
                       Physics2D.OverlapCircle(GroundCheck3.position, CheckRadius, HazardLayer)) ||
                      (Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, BossGround) ||
                       Physics2D.OverlapCircle(GroundCheck2.position, CheckRadius, BossGround) ||
                       Physics2D.OverlapCircle(GroundCheck3.position, CheckRadius, BossGround));

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            TrailRenderer.enabled = true;
            _isJumping = true;
            _rb.velocity = Vector2.up * (JumpForce * Time.fixedDeltaTime);
            _isRunning = false;
        }

        // CONTINUOUS JUMPING
        if (Input.GetKey(KeyCode.Space) && _jumpTimeCounter > 0 && _isJumping)
        {
            _isRunning = false;
            _animator.SetBool("Jumping", true);
            _rb.velocity = Vector2.up * (JumpForce * Time.fixedDeltaTime);
            _jumpTimeCounter -= Time.deltaTime;
        }

        // SLAM
        if (Input.GetKeyDown(KeyCode.S) && !_isGrounded)
        {
            _heightReached = transform.position.y;
            _rb.velocity = Vector2.down * (SlamSpeed * Time.fixedDeltaTime);
            _jumpTimeCounter = JumpTime;
            _isJumping = false;
            _isSlamming = true;
        }

        // STOP JUMPING
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _heightReached = transform.position.y;
            _isJumping = false;
            _jumpTimeCounter = JumpTime;
        }

        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        // REVERSE SPRITE IF NECESSARY
        if (mouseX > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (mouseX < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // STOP JUMPING ANIMATION
        if (_isGrounded && _animator.GetBool("Jumping"))
        {
            _animator.SetBool("Jumping", false);
        }

        // STOP HIT ANIMATION IF NECESSARY
        if (_hitTimer <= 0)
        {
            _hitTimer = _hitTimerStart;
            if (_animator.GetBool("Hit"))
            {
                _animator.SetBool("Hit", false);
            }
        }
        else
        {
            _hitTimer -= Time.deltaTime;
        }

        // RIPPLE 
        if (_isGrounded && (_isSlamming || _heightReached - transform.position.y > 3))
        {
            // Ripple.MaxAmount = (10 + Mathf.Abs(transform.position.y - _heightReached)) * (_isSlamming ? 1.5f : 0.25f);
            // Ripple.RippleEffect();
            _isSlamming = false;
            _heightReached = transform.position.y;
        }

        if (!_isRunning)
        {
            Footsteps.Stop();
        }

        _animator.SetFloat("Speed", Mathf.Abs(_moveInput));

        if (!(Camera.main is null))
        {
            Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector2 currentPosition = transform.position;
            float xMovement = Mathf.Clamp(currentPosition.x, minScreenBounds.x + 1, maxScreenBounds.x - 1);
            float yMovement = Mathf.Clamp(currentPosition.y, minScreenBounds.y + 1, maxScreenBounds.y - 1);
            transform.position = new Vector2(xMovement, yMovement);
        }
    }

    private IEnumerator SetAttacking()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("Attacking", false);
    }

    public void PlayParticles(string particleName, Transform _transform)
    {
        GameObject particles;
        // switch (particleName)
        // {
        //     case "BLOOD":
        //         bool isDead = UIManager.RemoveLife();
        //         if (isDead)
        //         {
        //         }
        //
        //         Ripple.MaxAmount = 20;
        //         Ripple.RippleEffect();
        //         _animator.SetBool("Hit", true);
        //         particles = Instantiate(BloodSplash, _transform, false);
        //         particles.transform.position = new Vector3(HeadTransform.position.x, HeadTransform.position.y, -10);
        //         break;
        // }
    }

    private void FixedUpdate()
    {
        if (_gameStateManager.DisableMovement)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        _moveInput = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        if (!_isRunning && _moveInput != 0 && _isGrounded)
        {
            Footsteps.Play();
            _isRunning = true;
        }

        if (_moveInput == 0)
        {
            Footsteps.Stop();
            _isRunning = false;
        }

        _rb.velocity = new Vector2(_moveInput * Speed, _rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(GroundCheck2.position, CheckRadius);
        Gizmos.DrawWireSphere(GroundCheck3.position, CheckRadius / 2);
    }

    public void GetHit()
    {
        if (!HitSound.isPlaying)
        {
            HitSound.Play();
        }
    }
}