using System;
using UnityEditor;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameStateManager GameStateManager;
    public Animator MenuAnimator;

    private void Update()
    {
        if (GameStateManager.DisableMovement)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool newState = true;

            if (MenuAnimator.GetBool("IsOpen"))
            {
                newState = false;
            }
            else
            {
                if (GameStateManager.DisableMovement)
                {
                    return;
                }
            }


            GameStateManager.DisableMovement = newState;

            MenuAnimator.SetBool("IsOpen", newState);
        }
    }
}