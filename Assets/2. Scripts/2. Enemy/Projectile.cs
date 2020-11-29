using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;

    public Transform _player;
    public Vector2 _target;
    private GameStateManager _gameStateManager;
    public GameObject Particles;
    private CameraShake _mainCamera;
    public float CameraShakeMagnitude;
    public float CameraShakeDuration;
    public AudioSource InstantiateSource;
    private PlayerController _playerController;

    public float Damage;

    private void Start()
    {
        _mainCamera = FindObjectOfType<CameraShake>();
        _gameStateManager = FindObjectOfType<GameStateManager>();
        _player = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
        _target = new Vector2(_player.position.x, _player.position.y);
        _playerController = FindObjectOfType<PlayerController>();
        InstantiateSource.Play();
    }

    private void Update()
    {
        if (_gameStateManager.DisableMovement)
        {
            return;
        }

        if (Math.Abs(transform.position.x - _target.x) <= 0.05 && Math.Abs(transform.position.y - _target.y) <= 0.05)
        {
            GameObject particles = Instantiate(Particles, null, false);
            particles.transform.position = transform.position;
            _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
            _playerController.GetHit();
            StartCoroutine(DestroySelf());
            return;
        }

        transform.position
            = Vector2.MoveTowards(transform.position, _target, Speed * 2 * Time.deltaTime);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack player = other.gameObject.GetComponent<PlayerAttack>();
            if (player._isInvincible)
            {
                return;
            }
            float attack = Damage - ((player.Armour + player.Defence) / 3);
            if (attack <= 0)
            {
                attack = 10;
            }

            player.Health -= attack;

            GameObject particles = Instantiate(Particles, null, false);
            particles.transform.position = transform.position;
            _mainCamera.Shake(CameraShakeMagnitude, CameraShakeDuration);
            StartCoroutine(DestroySelf());
        }
    }
}