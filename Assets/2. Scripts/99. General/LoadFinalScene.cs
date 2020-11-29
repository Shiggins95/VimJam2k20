using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFinalScene : MonoBehaviour
{
    public Animator FadeToBlack;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameStateManager>().DisableMovement = true;
            FadeToBlack.Play("FadeToBlack");
            StartCoroutine(LoadFinalSceneTrigger());
        }
    }

    private IEnumerator LoadFinalSceneTrigger()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FinalScene");
    }
}