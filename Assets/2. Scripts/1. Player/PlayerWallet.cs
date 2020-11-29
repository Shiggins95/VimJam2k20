using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour
{
    public TextMeshProUGUI NumberOfGemsDisplay;
    public Image GemsImage;
    public int Coins;

    private GameStateManager _gsm;

    public int SpendCoins(int amount)
    {

        Coins -= amount;
        // StartCoroutine(DecreaseCoins(amount, Coins - amount));
        return Coins;
    }

    public int ReceiveCoinsGG(int amount)
    {
        Coins += amount;
        // StartCoroutine(IncreaseCoins(Coins, Coins + amount));

        return Coins;
    }

    private IEnumerator IncreaseCoins(int startingAmount, int newAmount)
    {
        float startingFontSize = NumberOfGemsDisplay.fontSize;
        float currentFontSize = startingFontSize;
        int counter = 0;
        if (newAmount <= 25)
        {
            Coins += newAmount;
        }

        for (int i = startingAmount; i < newAmount; i++)
        {
            counter++;
            if (counter == 25 || i == newAmount)
            {
                yield return null;
                if (currentFontSize >= startingFontSize + 20)
                {
                    currentFontSize = startingFontSize;
                }
                else
                {
                    currentFontSize += 5;
                }

                NumberOfGemsDisplay.fontSize = currentFontSize;
                Coins += 25;
                if (Coins > newAmount)
                {
                    Coins = newAmount;
                }

                counter = 0;
            }
        }

        NumberOfGemsDisplay.fontSize = startingFontSize;
    }

    private IEnumerator DecreaseCoins(int startingAmount, int newAmount)
    {
        float startingFontSize = NumberOfGemsDisplay.fontSize;
        float currentFontSize = startingFontSize;
        int counter = 0;
        for (int i = startingAmount; i > newAmount; i--)
        {
            counter++;
            if (counter == 25 || i == newAmount)
            {
                yield return null;
                if (currentFontSize <= 5)
                {
                    currentFontSize = startingFontSize;
                }
                else
                {
                    currentFontSize -= 5;
                }

                NumberOfGemsDisplay.fontSize = currentFontSize;
                Coins -= 25;
                if (Coins <= 0)
                {
                    Coins = 0;
                }

                counter = 0;
            }
        }
    }

    private void Start()
    {
        if (!NumberOfGemsDisplay)
        {
            return;
        }

        if (MoneyManager.CurrentCoins > 0)
        {
            Coins = MoneyManager.CurrentCoins;
        }

        NumberOfGemsDisplay.text = Coins.ToString();
    }

    private void Update()
    {
        if (!NumberOfGemsDisplay)
        {
            return;
        }

        MoneyManager.CurrentCoins = Coins;

        NumberOfGemsDisplay.text = Coins.ToString();
    }
}