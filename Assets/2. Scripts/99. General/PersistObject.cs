using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistObject : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "EntryCutscene")
        {
            // DontDestroyOnLoad(gameObject);
        }
    }
}