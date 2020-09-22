﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public string Name;

    public string Type;

    public GameObject Upgradeable;

    private Upgradeable _upgradeable;

    public Image SpriteImage;

    public TextMeshProUGUI CostText;

    public GameObject Player;

    private PlayerWallet _playerWallet;

    private PlayerAttack _playerAttack;


    public bool IsButtonFunc;

    private string _upgradeCost;

    // Start is called before the first frame update
    void Start()
    {
        _upgradeable = Upgradeable.GetComponent<Upgradeable>();
        if (!IsButtonFunc)
        {
            SpriteImage.sprite = _upgradeable.GetSprite();
        }

        _playerWallet = Player.GetComponent<PlayerWallet>();
    }

    // Update is called once per frame
    void Update()
    {
        _upgradeCost = UpgradeCost.GetCosts()[_upgradeable.GetLevel()].ToString();
        if (!IsButtonFunc)
        {
            CostText.text = _upgradeCost;
        }
    }

    public void UpgradeClick()
    {
        _playerWallet.SpendCoins(Int32.Parse(_upgradeCost));
        _upgradeable.SetLevel(_upgradeable.GetLevel() + 1);
        _upgradeable.SetAttack(Int32.Parse(UpgradeCost.GetStats()[_upgradeable.GetLevel()].ToString()));
    }
}