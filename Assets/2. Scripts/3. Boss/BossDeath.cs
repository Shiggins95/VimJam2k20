using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

public class BossDeath : StateMachineBehaviour
{
    private DialogManager _dialogManager;

    private NextLevelTrigger _sceneLoader;

    private bool _movePlayer;

    public float Speed;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // trigger success dialog
        // at end of dialog open gate / shine light for next area
        Dialog dialog = FindObjectOfType<BossController>().SuccessDialog;
        FindObjectOfType<GameStateManager>().DisableMovement = true;
        _dialogManager = DialogManager.Instance;
        _dialogManager.EndBattle += StartLoadingNewScene;
        _sceneLoader = FindObjectOfType<NextLevelTrigger>();
        _sceneLoader.Active = true;
        FindObjectOfType<DialogManager>().StartDialog(dialog, "END");
    }

    public void StartLoadingNewScene()
    {
        FindObjectOfType<PlayerAttack>().EnableNewLife();
        _movePlayer = true;
        GameObject.FindGameObjectWithTag("FadeToBlack").GetComponent<Animator>().Play("FadeToBlack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_movePlayer)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            Transform player = pc.gameObject.transform;
            if (Math.Abs(player.position.x - _sceneLoader.transform.position.x) <= 0.1)
            {
                _movePlayer = false;
                pc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                return;
            }
            
            player.position =
                Vector2.MoveTowards(player.position, _sceneLoader.transform.position, Speed * Time.deltaTime);
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