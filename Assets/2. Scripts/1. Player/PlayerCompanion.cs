using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompanion : MonoBehaviour
{
    public Transform Player;

    public Vector2 Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Player.position.x - Offset.x, Player.position.y - Offset.y);
    }
}