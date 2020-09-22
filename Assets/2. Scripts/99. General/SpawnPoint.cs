using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform Transform;
    public Vector2 Position;

    private void Start()
    {
        Position = transform.position;
    }
}
