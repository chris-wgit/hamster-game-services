using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    private float enabledTime = 0f;
    public float dutation = 1f;

    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();

    }

    private void Start()
    {
        dutation = particle.main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        enabledTime += Time.deltaTime;
        if(enabledTime >= dutation)
        {
            enabledTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
