using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BossController : MonoBehaviour, EnemyClass
{
    public float Speed;

    public GameStateManager GameStateManager;

    public Transform Player;

    private Vector2 startingPosition;

    public float Health;

    public float Armour;

    public float Attack;

    public float Defence;

    public float AttackInterval;

    private float _currentAttackInterval;

    public Projectile Projectile;

    public Transform ProjectileSpawnPoint;

    public Dialog Dialog;

    public List<int> CurrencyDropTable;

    private void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.IsBossBattle || GameStateManager.DisableMovement)
        {
            return;
        }


        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        if (_currentAttackInterval <= 0)
        {
            Instantiate(Projectile.gameObject, ProjectileSpawnPoint, false);
            // AttackAction.BossAttack();
            _currentAttackInterval = AttackInterval;
        }
        else
        {
            _currentAttackInterval -= Time.deltaTime;
        }

        Vector2 currentPosition = transform.position;
        // float distance = Vector2.Distance(currentPosition, Player.position);
        if (Math.Abs(Player.position.x - transform.position.x) < 0.1)
        {
            if (!GameStateManager.IsBossBattle && Math.Abs(transform.position.x - startingPosition.x) > 0.01)
            {
                transform.position = startingPosition;
            }

            return;
        }

        if (currentPosition.x > Player.position.x)
        {
            transform.position = new Vector2(currentPosition.x - Speed * Time.deltaTime, startingPosition.y);
        }
        else if (currentPosition.x < Player.position.x)
        {
            transform.position = new Vector2(currentPosition.x + Speed * Time.deltaTime, startingPosition.y);
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
        return Armour;
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