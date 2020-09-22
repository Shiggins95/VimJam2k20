using UnityEngine;

public interface Upgradeable
{
    string GetName();
    int GetLevel();
    Sprite GetSprite();

    void SetAttack(float attack);
    void SetLevel(int level);
}