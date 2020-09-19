using System;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
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
        Vector2 currentPosition = transform.position;
        if (Vector2.Distance(currentPosition, _player.position) > EngagementDistance)
        {
            return;
        }

        if (Vector2.Distance(transform.position, _player.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(currentPosition, _player.position, Speed * Time.deltaTime);
        }

        _currentAttackInterval -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            bool isDead = AttackAction.EnemyAttack(FindObjectOfType<PlayerAttack>(), this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }
        if (_currentAttackInterval <= 0)
        {
            bool isDead = AttackAction.EnemyAttack(FindObjectOfType<PlayerAttack>(), this);
            _currentAttackInterval = AttackInterval;
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
}