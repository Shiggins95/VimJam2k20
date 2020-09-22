using System.Collections.Generic;
using UnityEngine;

public interface EnemyClass
{
    float GetAttack();
    float GetDefence();
    float GetArmor();
    float GetHealth();
    bool DecreaseHealth(float attack);

    List<int> GetCurrencySpawnTable();

    GameObject GetGameObject();

    ClampToParent GetClampToParent();

    Transform GetCanvas();

}