using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerProjectile : MonoBehaviour
{
    public float Speed;

    public PlayerAttack _player;
    public Vector2 _target;
    private GameStateManager _gameStateManager;
    public GameObject Particles;
    private CameraShake _mainCamera;

    public float CameraShakeMagnitude;
    public float CameraShakeDuration;

    public float Damage;

    private void Start()
    {
        _mainCamera = FindObjectOfType<CameraShake>();
        _player = FindObjectOfType<PlayerAttack>();
        _gameStateManager = FindObjectOfType<GameStateManager>();
        if (!(Camera.main is null)) _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }

        if (Math.Abs(transform.position.x - _target.x) <= 0.05 && Math.Abs(transform.position.y - _target.y) <= 0.05)
        {
            Debug.Log($"destroying");
            GameObject particles = Instantiate(Particles, null, false);
            particles.transform.position = transform.position;
            _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
            StartCoroutine(DestroySelf());
            return;
        }

        transform.position
            = Vector2.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyClass enemy = other.gameObject.GetComponent<EnemyClass>();
            float attack = Damage + _player.Weapon.Damage -
                           (enemy.GetArmor() + enemy.GetDefence());
            Debug.Log($"ATTACK: {attack}");
            if (attack <= 0)
            {
                attack = 10;
            }

            if (!enemy.DecreaseHealth(attack))
            {
                List<int> spawnTable = enemy.GetCurrencySpawnTable();
                int spawnedCurrency = 0;
                int randomInt = Random.Range(0, 100);
                if (randomInt < 50)
                {
                    spawnedCurrency = spawnTable[0];
                }
                else if (randomInt < 75)
                {
                    spawnedCurrency = spawnTable[1];
                }
                else if (randomInt < 90)
                {
                    spawnedCurrency = spawnTable[2];
                }
                else if (randomInt < 96)
                {
                    spawnedCurrency = spawnTable[3];
                }
                else
                {
                    spawnedCurrency = spawnTable[4];
                }

                ClampToParent go = Instantiate(enemy.GetClampToParent(), enemy.GetCanvas(), false);
                go.StartPoint = enemy.GetGameObject().transform.position;
                TextMeshProUGUI tmp = go.gameObject.GetComponent<TextMeshProUGUI>();
                tmp.text = $"+{spawnedCurrency}";
                _player.gameObject.GetComponent<PlayerWallet>().ReceiveCoinsGG(spawnedCurrency);
                // Destroy(enemy.GetGameObject());
            }

            GameObject particles = Instantiate(Particles, null, false);
            particles.transform.position = transform.position;
            _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
            StartCoroutine(DestroySelf());
        }
    }
}