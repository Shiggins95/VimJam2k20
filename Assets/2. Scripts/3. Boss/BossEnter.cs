using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : StateMachineBehaviour
{
    private DialogManager _dialogManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"StateEnter");
        FindObjectOfType<GameStateManager>().DisableMovement = true;
        Dialog dialog = FindObjectOfType<BossController>().Dialog;
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        _dialogManager = DialogManager.Instance;
        _dialogManager.StartBattle += StartBattle;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<DialogManager>().DisplayNextSentence();
        }
    }

    private void StartBattle()
    {
        FindObjectOfType<GameStateManager>().DisableMovement = false;
    }

    //
    // // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }
}