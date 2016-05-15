using UnityEngine;
using System;
using System.Collections;

public class NeonScript : MonoBehaviour
{

    Light pointLight;
    Light spotlight;
    Light[] lights;
    int TimeBeforeSound = 0;
    // Use this for initialization
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
        var neonSound = GameObject.Find("NeonSound").GetComponent<AudioSource>();
        if (TimeBeforeSound <= 0)
        {
            neonSound.volume = 1;
            neonSound.Play();
        }
       

    }

    public void Close()
    {
        foreach (Light light in lights)
            light.enabled = false;
    }

   
}
