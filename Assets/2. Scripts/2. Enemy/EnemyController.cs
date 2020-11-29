using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, EnemyClass
{
    private Transform _player;

    public float EngagementDistance;

    public float StoppingDistance;

    public float Speed;

    public float Attack;

    public Projectile Projectile;

    public float Health;
    public float Defence;
    public float Armor;
    private GameStateManager _gameStateManager;
    public float AttackInterval;
    private float _currentAttackInterval;
    public List<int> CurrencyDropTable;
    public float AttackSpeed;

    public float RetreatTimer;
    private float _currentRetreatTimer;
    private bool _isRetreating;

    public Transform Canvas;

    public ClampToParent LootDisplay;

    public bool Shooter;

    public bool Meelee;

    public bool PainInTheArse;
    private CameraShake _mainCamera;

    public float CameraShakeMagnitude;
    public float CameraShakeDuration;

    public Transform AttackPosition;

    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindObjectOfType<CameraShake>();
        _currentRetreatTimer = Random.Range(0.1f, RetreatTimer);
        _playerController = FindObjectOfType<PlayerController>();
        _player = _playerController.transform;
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _currentAttackInterval = AttackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
            // TODO - MAKE ANIMATION FOR DEATH AND TRIGGER
            return;
        }

        Vector2 currentPosition = transform.position;
        if (Vector2.Distance(currentPosition, _player.position) > EngagementDistance)
        {
            return;
        }

        if (Vector2.Distance(transform.position, _player.position) > StoppingDistance)
        {
            float _speed = Speed;
            if (_isRetreating && _currentRetreatTimer >= 0)
            {
                _speed = (Speed / 2) * -1;
            }
            else
            {
                _isRetreating = false;
                _currentRetreatTimer = Random.Range(0.1f, RetreatTimer);
                _speed = Speed;
            }

            transform.position = Vector2.MoveTowards(currentPosition, _player.position, _speed * Time.deltaTime);
            if (_player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }

            _currentRetreatTimer -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, _player.position) < StoppingDistance)
        {
            _isRetreating = true;
            transform.position = Vector2.MoveTowards(currentPosition, _player.position, (-Speed / 2) * Time.deltaTime);
        }

        if (_currentAttackInterval <= 0)
        {
            if (Shooter || PainInTheArse)
            {
                Shoot();
            }
        }

        _currentAttackInterval -= Time.deltaTime;
    }

    private void Shoot()
    {
        Projectile instantiatedProjectile = Instantiate(Projectile, null, false);
        instantiatedProjectile.Damage = Attack;
        instantiatedProjectile.Speed = AttackSpeed;
        instantiatedProjectile.transform.position = AttackPosition.position;
        _currentAttackInterval = AttackInterval;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Meelee || PainInTheArse)
        {
            if (other.CompareTag("Player"))
            {
                PlayerAttack pa = _player.gameObject.GetComponent<PlayerAttack>();
                if (!pa._isInvincible)
                {
                    _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
                    AttackAction.EnemyAttack(pa, this);
                    _playerController.GetHit();
                }
            }
        }
    }

    public float GetAttack()
    {
        return Attack;
    }

    public float GetDefence()
    {
        return Defence;
    }

    public float GetArmor()
    {
        return Armor;
    }

    public float GetHealth()
    {
        return Health;
    }

    public bool DecreaseHealth(float attack)
    {
        Health -= attack;
        return Health > 0;
    }

    public List<int> GetCurrencySpawnTable()
    {
        return CurrencyDropTable;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public ClampToParent GetClampToParent()
    {
        return LootDisplay;
    }

    public Transform GetCanvas()
    {
        return Canvas;
    }
}