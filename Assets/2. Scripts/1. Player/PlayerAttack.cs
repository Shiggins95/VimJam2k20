using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public float Attack;

    public float Defence;

    public float Armour;

    public float Health;

    public float StartingHealth;

    public Weapon Weapon;

    public float AttackRange;

    public float KnockbackDistance;

    public LayerMask AttackLayerMask;

    public Transform AttackPoint;

    public PlayerProjectile Projectile;

    public CapsuleCollider2D CapsuleCollider;

    public List<Image> Lives;
    public int RemainingLives;
    public Sprite DeadLifeSprite;
    public Sprite LifeSprite;

    private GameStateManager _gameStateManager;
    public float KnockbackSpeed;
    public Transform SpawnPoint;
    public float ReloadTime;
    public float _currentReloadTime;

    public Slider HealthBar;
    private Transform _currentCheckpoint;

    public float RespawnSpeed;

    private int _loads;

    private bool _respawning;

    public GameObject GunSprite;

    public Animator Animator;

    private Coroutine _death;

    public Animator FadeToBlack;

    public Image GameOverScreen;

    private bool _death1;

    public float InvincibleTimer;
    private float _currentInvincible;

    public bool _isInvincible;

    public Transform ParentCanvas;

    public GameObject Enemies;
    public int HealthLevel;

    private void Start()
    {
        HealthLevel = 1;
        _currentInvincible = InvincibleTimer;
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _currentReloadTime = ReloadTime;
        Health = StartingHealth;
        RemainingLives = 3;
        if (HealthBar)
        {
            HealthBar.maxValue = StartingHealth;
            HealthBar.value = StartingHealth;
        }
    }

    private int GetRemainingLives()
    {
        int total = Lives.Count;
        for (int i = 0; i < Lives.Count; i++)
        {
            if (!Lives[i])
            {
                continue;
            }

            if (Lives[i].sprite == DeadLifeSprite || !Lives[i].gameObject.activeSelf)
            {
                total -= 1;
            }
        }

        return total;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthBar)
        {
            HealthBar.value = Health;
        }

        if (Health <= 0 && _death == null)
        {
            if (_gameStateManager.IsBossBattle && RemainingLives > 1)
            {
                _gameStateManager.DisableMovement = true;
                Animator.SetBool("Dead", true);
                _death = StartCoroutine(BossBattleDeath());
                return;
            }

            _gameStateManager.DisableMovement = true;

            _gameStateManager.DisableMovement = true;
            _death = StartCoroutine(TriggerDeath());

            // gameObject.GetComponent<Rigidbody2D>().position = _gameStateManager.LastCheckpoint;
        }

        if (_isInvincible)
        {
            if (_currentInvincible <= 0)
            {
                _isInvincible = false;
                _currentInvincible = InvincibleTimer;
            }
            else
            {
                _currentInvincible -= Time.deltaTime;
            }
        }

        if (_respawning)
        {
            GunSprite.SetActive(false);
            Animator.SetBool("Dead", true);
            CapsuleCollider.enabled = false;
            transform.position =
                Vector2.MoveTowards(transform.position, _gameStateManager.LastCheckpoint,
                    RespawnSpeed * Time.deltaTime);
            if (Math.Abs(transform.position.x - _gameStateManager.LastCheckpoint.x) <= 0.5f &&
                Math.Abs(transform.position.y - _gameStateManager.LastCheckpoint.y) <= 0.5f)
            {
                CapsuleCollider.enabled = true;
                StartCoroutine(ReEnablePlayer());
            }

            if (Math.Abs(transform.position.x - _gameStateManager.LastCheckpoint.x) <= 0.4f &&
                Math.Abs(transform.position.y - _gameStateManager.LastCheckpoint.y) <= 0.4f)
            {
                CapsuleCollider.enabled = true;
                _respawning = false;
                Animator.SetBool("Dead", false);
                GunSprite.SetActive(true);
                Health = StartingHealth;
                StartCoroutine(ReEnablePlayer());
            }
        }

        if (_gameStateManager.DisableMovement)
        {
            return;
        }

        if (_currentReloadTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickPosition = Input.mousePosition;
                GameObject projectile = Instantiate(Projectile.gameObject, null, false);
                projectile.transform.position = SpawnPoint.position;

                _currentReloadTime = ReloadTime;
            }
        }
        else
        {
            _currentReloadTime -= Time.deltaTime;
        }
    }

    private IEnumerator BossBattleDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Health = StartingHealth;
        Animator.SetBool("Dead", false);
        _gameStateManager.DisableMovement = false;
        Image currentLife = Lives[RemainingLives - 1];
        currentLife.sprite = DeadLifeSprite;
        RemainingLives = GetRemainingLives();
        _death = null;
        _isInvincible = true;
    }

    private IEnumerator ReEnablePlayer()
    {
        yield return new WaitForSeconds(0.3f);
        _gameStateManager.DisableMovement = false;
        _isInvincible = true;
    }

    private IEnumerator ReloadLevel()
    {
        FadeToBlack.Play("FadeToBlack");
        yield return new WaitForSeconds(0.5f);
        Instantiate(GameOverScreen, ParentCanvas, false);
        yield return new WaitForSeconds(1f);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator TriggerDeath()
    {
        if (!_respawning)
        {
            Image currentLife = Lives[RemainingLives - 1];
            currentLife.sprite = DeadLifeSprite;
            // Destroy(currentLife.gameObject);
        }

        yield return new WaitForSeconds(0.5f);

        _death = null;

        _respawning = true;
        RemainingLives = GetRemainingLives();
        if (RemainingLives == 0)
        {
            Enemies.SetActive(false);
            StartCoroutine(ReloadLevel());
            _death1 = true;
            _gameStateManager.DisableMovement = true;
        }
    }

    public void EnableNewLife()
    {
        bool found = false;
        foreach (Image image in Lives)
        {
            if (image.sprite == DeadLifeSprite && !found)
            {
                image.sprite = LifeSprite;
                found = true;
            }
        }

        if (!found)
        {
            if (Lives.Count > RemainingLives)
            {
                Lives[RemainingLives].gameObject.SetActive(true);
            }
        }

        RemainingLives = GetRemainingLives();
    }
}