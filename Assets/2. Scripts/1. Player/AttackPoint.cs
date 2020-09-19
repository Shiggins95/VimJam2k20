using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private bool _isTriggered;
    private bool _isEnemy;

    private void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
    }

    private void Update()
    {
        if (_isEnemy && _isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.X) && !_gameStateManager.DisableMovement)
            {
                AttackAction.PlayerAttack(gameObject.GetComponentInParent<PlayerAttack>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isTriggered = true;
        if (other.CompareTag("Enemy"))
        {
            _isEnemy = true;
        }
        else
        {
            _isEnemy = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isTriggered = false;
        _isEnemy = false;
    }
}