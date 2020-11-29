using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCutscene : MonoBehaviour
{
    public BossController BossController;
    private PlayerController _player;
    private GameStateManager _gsm;

    public float MoveSpeed;

    private bool _moveTowardsPlayer;
    private bool _moveAwayFromPlayer;

    public float RetreatDistance;
    private float _currentRetreatDistance;

    public float FinalRetreatDistance;
    private float _currentFinalRetreatDistance;

    private DialogManager _dialogManager;

    private Animator _playerAnimator;

    private bool _finalRetreat;
    public Animator FadeToBlack;

    private void Start()
    {
        _dialogManager = DialogManager.Instance;
        _dialogManager.EndStartingCutscene += HandleEndCutscene;
        _dialogManager.PlayerEndStartingCutscene += HandlePlayerEndCutscene;
        _currentRetreatDistance = RetreatDistance;
        _gsm = FindObjectOfType<GameStateManager>();
        _player = FindObjectOfType<PlayerController>();
        _playerAnimator = _player.gameObject.GetComponent<Animator>();
        _currentFinalRetreatDistance = FinalRetreatDistance;
    }


    private void Update()
    {
        Vector2 bossPosition = BossController.transform.position;
        Vector2 playerPosition = _player.transform.position;
        if (_moveTowardsPlayer)
        {
            BossController.transform.position = Vector2.MoveTowards(bossPosition,
                playerPosition, MoveSpeed * Time.deltaTime);

            if (Math.Abs(bossPosition.x - playerPosition.x) <= 0.5 &&
                Math.Abs(bossPosition.y - playerPosition.y) <= 0.5)
            {
                _moveTowardsPlayer = false;
                _playerAnimator.SetBool("Dead", true);
                StartCoroutine(TriggerMoveAway());
            }
        }

        if (_moveAwayFromPlayer)
        {
            if (_currentRetreatDistance <= 0)
            {
                _moveAwayFromPlayer = false;
                _playerAnimator.SetBool("Dead", false);
                Dialog dialog = BossController.gameObject.GetComponent<DialogHolder>().StartingDialog;
                _dialogManager.StartDialog(dialog, "STARTCUTSCENE");
            }
            else
            {
                _currentRetreatDistance -= Time.deltaTime;
                BossController.transform.position = BossController.transform.position = Vector2.MoveTowards(
                    bossPosition,
                    playerPosition, -MoveSpeed * Time.deltaTime);
            }
        }

        if (_finalRetreat)
        {
            if (_currentFinalRetreatDistance <= 0)
            {
                _finalRetreat = false;
                Dialog playerDialog = _player.gameObject.GetComponent<DialogHolder>().StartingDialog;
                _dialogManager.StartDialog(playerDialog, "PLAYERSTARTCUTSCENE");
            }
            else
            {
                BossController.transform.position = BossController.transform.position = Vector2.MoveTowards(
                    bossPosition,
                    playerPosition, -MoveSpeed * Time.deltaTime);
                _currentFinalRetreatDistance -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gsm.DisableMovement = true;
            // trigger cutscene
            BossController.gameObject.SetActive(true);
            _moveTowardsPlayer = true;
            _player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private IEnumerator TriggerMoveAway()
    {
        yield return new WaitForSeconds(0.5f);

        _moveAwayFromPlayer = true;
    }

    private void HandleEndCutscene()
    {
        _finalRetreat = true;
    }

    private void HandlePlayerEndCutscene()
    {
        FadeToBlack.Play("FadeToBlack");
        StartCoroutine(LoadTheScene());
    }

    private IEnumerator LoadTheScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
}