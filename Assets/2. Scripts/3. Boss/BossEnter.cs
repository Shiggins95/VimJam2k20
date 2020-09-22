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
    }
}