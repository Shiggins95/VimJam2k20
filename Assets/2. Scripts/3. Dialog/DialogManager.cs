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

    public static DialogManager Instance;

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

    public void StartDialog(Dialog dialog)
    {
        Animator.SetBool("IsOpen", true);
        Debug.Log($"Starting convo with {dialog.Name}");
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
        if (Sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        if (_stillTyping)
        {
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
            Debug.Log($"TEXT SPEED {_textSpeed}");
            DialogText.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }

        _stillTyping = false;
    }

    private void EndDialog()
    {
        Debug.Log($"dialog ended");
        Animator.SetBool("IsOpen", false);
        // if event exists, invoke
        StartBattle?.Invoke();
    }
    
    public delegate void BossCallback();

    public BossCallback StartBattle;
}