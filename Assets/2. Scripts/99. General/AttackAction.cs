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
        if (player.Weapon)
        {
        }
        else
        {
            // check for collider based on player attack point position and attacking range
            Collider2D collider =
                Physics2D.OverlapCircle(player.AttackPoint.position, player.AttackRange, player.AttackLayerMask);
            if (collider)
            {
                // if collider exists, do logic to calculate damage
                BossController target = collider.gameObject.GetComponent<BossController>();
                if (target)
                {
                    float attack = player.Attack - (target.Defence + target.Armour);
                    Debug.Log($"attack {attack}");
                    target.Health -= attack;
                    return target.Health <= 0;
                }
            }
        }

        return false;
    }
}