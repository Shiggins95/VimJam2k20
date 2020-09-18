using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float Attack;

    public float Defence;

    public float Armour;

    public float Health;

    public GameObject Weapon;

    public float AttackRange;

    public LayerMask AttackLayerMask;

    public Transform AttackPoint;

    private GameStateManager _gameStateManager;

    private void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !_gameStateManager.DisableMovement)
        {
            AttackAction.PlayerAttack(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}