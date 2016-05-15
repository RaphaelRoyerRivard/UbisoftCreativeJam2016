using UnityEngine;
using System.Collections;

public class NeonScript : MonoBehaviour {

    Light pointLight;
    Light spotlight;
    Light[] lights;
    // Use this for initialization
    void Start () {
        lights = GetComponentsInChildren<Light>();
        Close();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Open()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }

    public void Close()
    {
        foreach (Light light in lights)
            light.enabled = false;
    }
}
