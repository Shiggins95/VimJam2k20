using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashingLight : MonoBehaviour
{
    public float StartTImer;
    private float _currentTimer;
    private float _startingIntensity;

    private Light2D _light;

    private bool goingDown;

    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = StartTImer;
        _light = GetComponent<Light2D>();
        _startingIntensity = _light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (_light.intensity <= 0)
        {
            goingDown = false;
        }
        else if (_light.intensity >= _startingIntensity)
        {
            goingDown = true;
        }

        if (goingDown)
        {
            _light.intensity -= Time.deltaTime * StartTImer;
        }
        else
        {
            _light.intensity += Time.deltaTime * StartTImer;
        }
    }
}