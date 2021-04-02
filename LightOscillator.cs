using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOscillator : MonoBehaviour
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float Frequency;
    [SerializeField] private float PhaseShift;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;

        light.intensity = (Amplitude * Mathf.Sin(t * Frequency))+PhaseShift;
    }
}
