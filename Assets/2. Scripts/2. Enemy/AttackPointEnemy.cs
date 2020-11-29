using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AttackPointEnemy : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float AttackRadius;
    public float Damage;
    public float DamageInterval;
    public float _currentDamageInterval;
    public CameraShake _mainCamera;
    public float CameraShakeMagnitude;
    public float CameraShakeDuration;

    private void Start()
    {
        _currentDamageInterval = DamageInterval;
        _mainCamera = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, AttackRadius, PlayerLayer);
        if (player && _currentDamageInterval <= 0)
        {
            PlayerAttack playerProps = player.gameObject.GetComponent<PlayerAttack>();
            if (!playerProps)
            {
                return;
            }

            playerProps.Health -= Damage;
            _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
            _currentDamageInterval = DamageInterval;
            player.gameObject.GetComponent<PlayerController>().GetHit();
        }

        if (_currentDamageInterval > 0)
        {
            _currentDamageInterval -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}