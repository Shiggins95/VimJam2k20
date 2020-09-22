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

    public float Health;
    public float Defence;
    public float Armor;
    private GameStateManager _gameStateManager;
    public float AttackInterval;
    private float _currentAttackInterval;
    public List<int> CurrencyDropTable;

    public float RetreatTimer;
    private float _currentRetreatTimer;
    private bool _isRetreating;

    public Transform Canvas;

    public ClampToParent LootDisplay;

    // Start is called before the first frame update
    void Start()
    {
        _currentRetreatTimer = Random.Range(0.1f, RetreatTimer);
        _player = FindObjectOfType<PlayerController>().transform;
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
            _currentRetreatTimer -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, _player.position) < StoppingDistance)
        {
            _isRetreating = true;
            transform.position = Vector2.MoveTowards(currentPosition, _player.position, (-Speed / 2) * Time.deltaTime);
        }

        _currentAttackInterval -= Time.deltaTime;
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