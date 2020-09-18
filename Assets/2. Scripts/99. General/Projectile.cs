using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;

    private Transform _player;
    private Vector2 _target;
    private GameStateManager _gameStateManager;

    private void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _target = new Vector2(_player.position.x, _player.position.y);
    }

    private void Update()
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }

        if (Math.Abs(transform.position.x - _target.x) <= 0.05 && Math.Abs(transform.position.y - _target.y) <= 0.05)
        {
            Destroy(gameObject);
            return;
        }

        transform.position
            = Vector2.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}