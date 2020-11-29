using System.Collections;
using Cinemachine;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public string NextScene;
    public bool Active;
    private GameStateManager _gameStateManager;
    public CameraBox NextCam;
    public CameraBox PrevCam;
    public CinemachineConfiner Camera;
    public GameObject NextFrame;
    public GameObject PrevFrame;
    public Animator FadeToBlack;
    public GameObject Enemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
        if (!Active)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerAttack>().Enemies = Enemies;
            NextCam.gameObject.SetActive(true);
            PrevCam.gameObject.SetActive(false);
            Camera.m_BoundingShape2D = NextCam.gameObject.GetComponent<PolygonCollider2D>();
            StartCoroutine(StartNextFrame());
            NextFrame.SetActive(true);
        }
    }

    private IEnumerator StartNextFrame()
    {
        yield return new WaitForSeconds(1f);
        PrevFrame.SetActive(false);
        FadeToBlack.Play("FadeFromBlack");
        _gameStateManager.DisableMovement = false;
        _gameStateManager.IsBossBattle = false;
    }
}