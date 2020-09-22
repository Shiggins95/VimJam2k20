using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Color = UnityEngine.Color;

public class Checkpoint : MonoBehaviour
{
    private GameStateManager _gm;
    public Light2D RedLight;
    public Light2D GreenLight;
    public GameObject NextPoint;
    public int CheckpointId;

    private void Start()
    {
        _gm = FindObjectOfType<GameStateManager>();
        RedLight.gameObject.SetActive(true);
        GreenLight.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RedLight.gameObject.SetActive(false);
            GreenLight.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            _gm.LastCheckpoint = other.gameObject.transform.position;
            // foreach (CheckpointID checkpoint in FindObjectsOfType<CheckpointID>())
            // {
            //     if (checkpoint.ID < CheckpointId)
            //     {
            //         checkpoint.gameObject.SetActive(false);
            //     }
            // }
        }
    }
}