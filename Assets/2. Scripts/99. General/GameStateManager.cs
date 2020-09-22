using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsBossBattle;
    public bool DisableMovement;

    public int CurrentCheckpoint;
    public Vector2 LastCheckpoint;

    private static GameStateManager Instance;
    private void Start()
    {
        CurrentCheckpoint = 0;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        int newCheckpoint = PlayerPrefs.GetInt("CurrentCheckPoint");
        if (CurrentCheckpoint != newCheckpoint)
        {
            Debug.Log($"NEW CHECKPOINT {newCheckpoint}");
            CurrentCheckpoint = newCheckpoint;
        }
    }
}