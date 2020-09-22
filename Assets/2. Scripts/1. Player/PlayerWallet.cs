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

    public int SpendCoins(int amount)
    {
        Coins -= amount;
        return Coins;
    }

    public int ReceiveCoinsGG(int amount)
    {
        // if (_updating)
        // {
        //     StopCoroutine(CountCoins(amount, Coins));
        // }
        // _updating = true;
        int startingCoins = Coins;

        StartCoroutine(CountCoins(amount, startingCoins));

        return Coins;
    }

    private IEnumerator CountCoins(int amount, int startingCoins)
    {
        float startingFontSize = NumberOfGemsDisplay.fontSize;
        float currentFontSize = startingFontSize;
        for (int i = startingCoins; i < startingCoins + amount; i++)
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
            Coins += 1;
        }
    }

    private void Start()
    {
        NumberOfGemsDisplay.text = Coins.ToString();
    }

    private void Update()
    {
        NumberOfGemsDisplay.text = Coins.ToString();
    }
}