using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    public float Timer;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
        {
            _timer = Timer;
            gameObject.SetActive(false);
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}