using UnityEngine;
using System.Collections;
using System;

public class CameraLogger : MonoBehaviour, HeadMovementListener {
    GameObject cube;
    HeadMovementDetector detector;

    // Use this for initialization
    void Start ()
    {
        cube = GameObject.Find("Cube");
        detector = new HeadMovementDetector(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        detector.Update();
    }

    public void HeadMoved()
    {
        cube.GetComponent<Renderer>().material.color = Color.red;
    }

    public void HeadStopped(float elapsedTime)
    {
        cube.GetComponent<Renderer>().material.color = Color.green;
        Debug.Log("Head stopped after " + elapsedTime + " seconds of movement");
    }
}
