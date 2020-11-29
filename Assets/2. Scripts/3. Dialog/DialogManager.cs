using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> Sentences;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogText;
    public Animator Animator;

    public float TextSpeed;
    private float _textSpeed;

    private bool _stillTyping;
    private bool _speedUp;

    private bool _isStarted;
    public static DialogManager Instance;

    private string Type;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _textSpeed = TextSpeed;
        Sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_stillTyping && _isStarted)
        {
            DisplayNextSentence();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_textSpeed <= 0.01)
            {
                _textSpeed = 0;
                return;
            }

            _textSpeed /= 2;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _textSpeed = TextSpeed;
        }
    }

    public void StartDialog(Dialog dialog, string type)
    {
        AudioSource source = FindObjectOfType<GameStateManager>().gameObject.GetComponent<AudioSource>();

        if (source)
        {
            source.mute = true;
        }

        _isStarted = true;
        Type = type;
        Animator.SetBool("IsOpen", true);
        NameText.text = dialog.Name;
        Sentences.Clear();
        foreach (string sentence in dialog.Sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_stillTyping)
        {
            return;
        }

        if (Sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = Sentences.Dequeue();
        StartCoroutine(TypeText(sentence));
    }

    private IEnumerator TypeText(string sentence)
    {
        _stillTyping = true;
        DialogText.text = "";
        foreach (char letter in sentence)
        {
            DialogText.text += letter;
            yield return null;
        }

        _stillTyping = false;
    }

    private void EndDialog()
    {
        Animator.SetBool("IsOpen", false);
        _isStarted = false;
        // if event exists, invoke
        if (Type == "START")
        {
            StartBattle?.Invoke();
        }
        else if (Type == "END")
        {
            EndBattle?.Invoke();
        }
        else if (Type == "STARTCUTSCENE")
        {
            EndStartingCutscene?.Invoke();
        }
        else if (Type == "PLAYERSTARTCUTSCENE")
        {
            PlayerEndStartingCutscene?.Invoke();
        }
        else if (Type == "HEALTHPICKUP")
        {
            HealthPickup?.Invoke();
        }
        else if (Type == "ENDING")
        {
            FinalScene?.Invoke();
        }

        AudioSource source = FindObjectOfType<GameStateManager>().gameObject.GetComponent<AudioSource>();

        if (source)
        {
            source.mute = false;
        }
    }

    public delegate void BossCallback();

    public BossCallback StartBattle;
    public BossCallback EndBattle;
    public BossCallback EndStartingCutscene;
    public BossCallback PlayerEndStartingCutscene;
    public BossCallback HealthPickup;
    public BossCallback FinalScene;
}