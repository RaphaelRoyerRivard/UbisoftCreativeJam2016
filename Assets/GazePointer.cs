using UnityEngine;
using System.Collections;

public class GazePointer : MonoBehaviour, GazeListener
{
    GazeDetector gazeDetector;

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("VRPointer").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
        gazeDetector = new GazeDetector(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gazeDetector.Update();
    }

    public void GameObjectFocused(GameObject gameObject)
    {
        Debug.Log("GameObjectFocused: " + gameObject);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void GameObjectLostFocus(GameObject gameObject)
    {
        Debug.Log("GameObjectLostFocus: " + gameObject);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void GameObjectGazed(GameObject gameObject)
    {
        Debug.Log("GameObjectGazed: " + gameObject);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
