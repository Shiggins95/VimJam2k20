using System.Collections.Generic;

public interface EnemyClass
{
    float GetAttack();
    float GetDefence();
    float GetArmor();
    float GetHealth();
    bool DecreaseHealth(float attack);

    List<int> GetCurrencySpawnTable();
}