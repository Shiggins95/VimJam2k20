using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public Animator FadeToBlack;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().gameObject.transform.position = transform.position;
        FadeToBlack.Play("FadeFromBlack");
    }
}
