using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateClosed : StateMachineBehaviour
{
    public float _waitTime;
    private float _currentWaitTime;

    private DialogManager _dialogManager;
    private bool _dialogStarted;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentWaitTime = _waitTime;
    }

    private void StartBattle()
    {
        FindObjectOfType<GameStateManager>().DisableMovement = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_currentWaitTime <= 0 && !_dialogStarted)
        {
            Dialog dialog = FindObjectOfType<BossController>().Dialog;
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            _dialogManager = DialogManager.Instance;
            _dialogManager.StartBattle += StartBattle;
            _dialogStarted = true;
        }
        else
        {
            _currentWaitTime -= Time.deltaTime;
        }

        if (_dialogStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<DialogManager>().DisplayNextSentence();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}