using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinalScene : MonoBehaviour
{
    public DialogHolder _player;

    private DialogManager _dm;
    // Start is called before the first frame update
    void Start()
    {
        _dm = DialogManager.Instance;
        _dm.FinalScene += LoadCredits;
        StartCoroutine(RunCredits());
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    private IEnumerator RunCredits()
    {
        yield return new WaitForSeconds(1f);
        _dm.StartDialog(_player.StartingDialog, "ENDING");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
