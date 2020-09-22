using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public float Offset;

    public CapsuleCollider2D CapsuleCollider;

    public List<Image> Lives;
    public Sprite DeadLifeSprite;

    private GameStateManager _gameStateManager;
    public float KnockbackSpeed;
    public Transform SpawnPoint;
    public Transform GlobalSpawnPoint;
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

    private void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _currentReloadTime = ReloadTime;
        Health = StartingHealth;
        HealthBar.maxValue = StartingHealth;
        HealthBar.value = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;

        if (Health <= 0 && _death == null)
        {
            _gameStateManager.DisableMovement = true;
            _death = StartCoroutine(TriggerDeath());
            // gameObject.GetComponent<Rigidbody2D>().position = _gameStateManager.LastCheckpoint;
        }

        if (_respawning)
        {
            GunSprite.SetActive(false);
            Animator.SetBool("Dead", true);
            CapsuleCollider.enabled = false;
            transform.position =
                Vector2.MoveTowards(transform.position, _gameStateManager.LastCheckpoint,
                    RespawnSpeed * Time.deltaTime);
            if (Math.Abs(transform.position.x - _gameStateManager.LastCheckpoint.x) <= 0.1f &&
                Math.Abs(transform.position.y - _gameStateManager.LastCheckpoint.y) <= 0.1f)
            {
                _respawning = false;
                _gameStateManager.DisableMovement = false;
                CapsuleCollider.enabled = true;
                Animator.SetBool("Dead", false);
                GunSprite.SetActive(true);
                Health = StartingHealth;
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
                Debug.Log($"clocked");
                Vector2 clickPosition = Input.mousePosition;
                GameObject projectile = Instantiate(Projectile.gameObject, GlobalSpawnPoint, false);
                projectile.transform.position = SpawnPoint.position;

                _currentReloadTime = ReloadTime;
            }
        }
        else
        {
            _currentReloadTime -= Time.deltaTime;
        }
    }

    private IEnumerator TriggerDeath()
    {
        if (!_respawning)
        {
            Image currentLife = Lives[0];
            Lives.RemoveAt(0);
            currentLife.sprite = DeadLifeSprite;
            // Destroy(currentLife.gameObject);
        }

        yield return new WaitForSeconds(0.5f);

        _death = null;

        _respawning = true;
    }
}