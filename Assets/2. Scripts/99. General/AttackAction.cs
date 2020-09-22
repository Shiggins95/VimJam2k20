using System.Collections.Generic;
using UnityEngine;

public static class AttackAction
{
    public static bool BossAttack(PlayerAttack target, BossController attacker)
    {
        float attack = attacker.Attack - (target.Defence + target.Armour);
        target.Health -= attack;
        return target.Health <= 0;
    }

    public static bool PlayerAttack(PlayerAttack player)
    {
        // check for collider based on player attack point position and attacking range
        Collider2D collider =
            Physics2D.OverlapCircle(player.AttackPoint.position, player.AttackRange, player.AttackLayerMask);
        if (collider)
        {
            // if collider exists, do logic to calculate damage
            EnemyClass target = collider.gameObject.GetComponent<EnemyClass>();
            if (target != null)
            {
                float attack = player.Attack - (target.GetDefence() + target.GetArmor());
                if (player.Weapon != null)
                {
                    attack += player.Weapon.Damage;
                }

                Debug.Log($"attack {attack}");
                if (!target.DecreaseHealth(attack))
                {
                    // GameObject.Destroy(collider.gameObject);
                    List<int> spawnTable = target.GetCurrencySpawnTable();
                    int spawnedCurrency = 0;
                    int randomInt = Random.Range(0, 100);
                    if (randomInt < 50)
                    {
                        spawnedCurrency = spawnTable[0];
                    }
                    else if (randomInt < 75)
                    {
                        spawnedCurrency = spawnTable[1];
                    }
                    else if (randomInt < 90)
                    {
                        spawnedCurrency = spawnTable[2];
                    }
                    else if (randomInt < 96)
                    {
                        spawnedCurrency = spawnTable[3];
                    }
                    else
                    {
                        spawnedCurrency = spawnTable[4];
                    }

                    GameObject.Instantiate(target.GetClampToParent(), target.GetCanvas(), false);
                    Debug.Log($"SPAWNED CURRENCY: {spawnedCurrency}");
                }
                else
                {
                    Vector2 currentTargetPosition = target.GetGameObject().transform.position;
                    Vector2 currentPlayerPosition = player.gameObject.transform.position;
                    float x = currentTargetPosition.x;
                    float y = currentTargetPosition.y;

                    if (x - currentPlayerPosition.x <= 0.1)
                    {
                        x -= player.KnockbackDistance;
                    }
                    else if (x - currentPlayerPosition.x >= 0.1)
                    {
                        x += player.KnockbackDistance;
                    }

                    // target.GetGameObject().transform.position = new Vector2(x, y + player.KnockbackDistance);
                    target.GetGameObject().transform.position = Vector2.MoveTowards(currentTargetPosition,
                        new Vector2(x, y + player.KnockbackDistance), player.KnockbackSpeed * Time.deltaTime);
                }
            }
        }


        return false;
    }

    public static bool EnemyAttack(PlayerAttack target, EnemyClass attacker)
    {
        float attack = attacker.GetAttack() - (target.Defence + target.Armour);
        target.Health -= attack;
        if (target.Health <= 0)
        {
            target.gameObject.SetActive(false);
        }

        return target.Health <= 0;
    }
}