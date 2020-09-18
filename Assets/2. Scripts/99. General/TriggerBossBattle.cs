using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossBattle : MonoBehaviour
{
    public Animator GateAnimator;
    public Animator BossAnimator;
    public PlayerController Player;
    public GameStateManager GameStateManager;
    public BossController Boss;

    private bool _triggered;

    private void Update()
    {
        if (_triggered && Player._isGrounded)
        {
            Boss.gameObject.SetActive(true);
            GateAnimator.SetBool("IsBossBattle", true);
            GameStateManager.IsBossBattle = true;
            // BossAnimator.SetBool("Start", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _triggered = !_triggered;
        }
    }
}