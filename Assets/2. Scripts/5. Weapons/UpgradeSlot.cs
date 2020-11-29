using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool Highlighted;

    public TextMeshProUGUI NotEnoughCashText;

    public Image HighlightedImage;

    public bool GameOverScreen;

    private string _upgradeCost;

    public string ButtonValue;

    private Animator _fadeToBlack;

    public bool TitleScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (GameOverScreen)
        {
            return;
        }

        _playerAttack = FindObjectOfType<PlayerAttack>();
        _upgradeable = Upgradeable.GetComponent<Upgradeable>();
        // SpriteImage.sprite = _upgradeable.GetSprite();
        _playerWallet = Player.GetComponent<PlayerWallet>();
        _fadeToBlack = GameObject.FindGameObjectWithTag("FadeToBlack").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOverScreen)
        {
            if (ButtonValue == "WEAPON")
            {
                _upgradeCost = UpgradeCost.GetCosts()[_upgradeable.GetLevel()].ToString();
            }
            else if (ButtonValue == "DEFENCE")
            {
                _upgradeCost = UpgradeCost.GetDefenceUpgrades()[_playerAttack.HealthLevel].ToString();
            }

            CostText.text = _upgradeCost;
        }


        if (Highlighted)
        {
            if (!HighlightedImage.gameObject.activeSelf)
            {
                HighlightedImage.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!GameOverScreen && !TitleScreen)
                {
                    UpgradeClick();
                }
                else
                {
                    if (ButtonValue == "YES")
                    {
                        StartCoroutine(ReloadScene());
                    }
                    else if (ButtonValue == "NO")
                    {
                        SceneManager.LoadScene("TitleScreen");
                    }
                    else if (ButtonValue == "TitleScreen")
                    {
                        SceneManager.LoadScene("CreditsScene");
                    }
                    else if (ButtonValue == "Play")
                    {
                        SceneManager.LoadScene("EntryCutscene");
                    }
                    else if (ButtonValue == "Controls")
                    {
                        // load control screen
                        SceneManager.LoadScene("Controls");
                    }
                }
            }
        }
        else
        {
            if (HighlightedImage.gameObject.activeSelf)
            {
                HighlightedImage.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(transform.parent.gameObject);
        yield return null;
    }

    public void UpgradeClick()
    {
        if (_playerWallet.Coins < Int32.Parse(_upgradeCost))
        {
            HighlightedImage.color = Color.red;
            NotEnoughCashText.gameObject.SetActive(true);
            StartCoroutine(SetBack());
            return;
        }

        HighlightedImage.color = Color.cyan;
        _playerWallet.SpendCoins(Int32.Parse(_upgradeCost));

        if (ButtonValue == "WEAPON")
        {
            _upgradeable.SetLevel(_upgradeable.GetLevel() + 1);
            _upgradeable.SetAttack(Int32.Parse(UpgradeCost.GetStats()[_upgradeable.GetLevel()].ToString()));
        }
        else if (ButtonValue == "DEFENCE")
        {
            _playerAttack.HealthLevel += 1;
            _playerAttack.Defence = Int32.Parse(UpgradeCost.GetDefenceStats()[_playerAttack.HealthLevel].ToString());
        }

        StartCoroutine(SetBack());
    }

    private IEnumerator SetBack()
    {
        yield return new WaitForSeconds(0.2f);
        HighlightedImage.color = Color.white;
    }
}