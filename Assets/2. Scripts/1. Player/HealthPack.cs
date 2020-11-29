using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float MaxHealthIncrease;

    public Dialog PickupDialog;

    private DialogManager _dm;
    private GameStateManager _gsm;

    private bool _triggered;

    // Start is called before the first frame update
    void Start()
    {
        _dm = DialogManager.Instance;
        _dm.HealthPickup += HandlePickup;
        _gsm = FindObjectOfType<GameStateManager>();
    }

    private void HandlePickup()
    {
        _gsm.DisableMovement = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>()._isGrounded && !_triggered)
        {
            _triggered = true;
            _gsm.Wahhhh.Play();
            PickupDialog.Sentences[0] += $" {MaxHealthIncrease}";
            _gsm.LastCheckpoint = other.gameObject.transform.position;
            _gsm.DisableMovement = true;
            PlayerAttack player = other.gameObject.GetComponent<PlayerAttack>();
            player.HealthBar.maxValue += MaxHealthIncrease;
            player.Health = player.HealthBar.maxValue;
            player.StartingHealth = player.HealthBar.maxValue;
            _dm.StartDialog(PickupDialog, "HEALTHPICKUP");
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>()._isGrounded && !_triggered)
        {
            _triggered = true;
            _gsm.Wahhhh.Play();
            PickupDialog.Sentences[0] += $" {MaxHealthIncrease}";
            _gsm.LastCheckpoint = other.gameObject.transform.position;
            _gsm.DisableMovement = true;
            PlayerAttack player = other.gameObject.GetComponent<PlayerAttack>();
            player.HealthBar.maxValue += MaxHealthIncrease;
            player.Health = player.HealthBar.maxValue;
            player.StartingHealth = player.HealthBar.maxValue;
            _dm.StartDialog(PickupDialog, "HEALTHPICKUP");
            Destroy(gameObject);
        }
    }
}