using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    public Transform Player;

    public float StoppingDistance;

    public float Speed;

    public float RetreatDistance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        // if (Vector2.Distance(transform.position, Player.position) > BackOffRange)
        // {
        //     transform.position = Vector2.MoveTowards(currentPosition, Player.position, Speed * Time.deltaTime);
        // }
        // else if (Vector2.Distance(transform.position, Player.position) < BackOffRange &&
        //          Vector2.Distance(transform.position, Player.position) > RetreatDistance)
        // {
        //     transform.position = this.transform.position;
        // }
        // else if (Vector2.Distance(transform.position, Player.position) < RetreatDistance)
        // {
        //     transform.position = Vector2.MoveTowards(currentPosition, Player.position, -Speed * Time.deltaTime);
        // }
        if (Vector2.Distance(currentPosition, Player.position) < StoppingDistance)
        {
            var k = 0;
        }
        // if (Vector2.Distance(currentPosition, Player.position) < RetreatDistance)
        // {
        // }
    }
}