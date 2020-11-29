using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeMenu : MonoBehaviour
{
    public GameStateManager GameStateManager;
    public Animator MenuAnimator;
    public Image Selector;
    public List<UpgradeSlot> UpgradeSlots;
    public int CurrentHighlighted;
    public bool GameOverScreen;
    public bool TitleScreen;

    public GameObject UpgradeablesMenu;

    private void Start()
    {
        CurrentHighlighted = 0;
        if (GameOverScreen)
        {
            UpgradeablesMenu = GameObject.FindGameObjectWithTag("UpgradeMenu");
            UpgradeablesMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !GameOverScreen && !TitleScreen)
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

        if (!GameOverScreen && !TitleScreen && !MenuAnimator.GetBool("IsOpen"))
        {
            return;
        }

        if (!TitleScreen)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UpgradeSlots[CurrentHighlighted].Highlighted = false;
                DecreaseIndex();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpgradeSlots[CurrentHighlighted].Highlighted = false;
                IncreaseIndex();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpgradeSlots[CurrentHighlighted].Highlighted = false;
                DecreaseIndex();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpgradeSlots[CurrentHighlighted].Highlighted = false;
                IncreaseIndex();
            }
        }

        UpgradeSlots[CurrentHighlighted].Highlighted = true;
    }

    private void DecreaseIndex()
    {
        if (CurrentHighlighted == 0)
        {
            CurrentHighlighted = UpgradeSlots.Count - 1;
        }
        else
        {
            CurrentHighlighted -= 1;
        }
    }

    private void IncreaseIndex()
    {
        if (CurrentHighlighted == UpgradeSlots.Count - 1)
        {
            CurrentHighlighted = 0;
        }
        else
        {
            CurrentHighlighted += 1;
        }
    }
}