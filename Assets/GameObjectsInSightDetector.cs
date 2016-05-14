using UnityEngine;
using System.Collections;

/// <summary>
/// This script must be attached to a GameObject that has a render.
/// </summary>
public class GameObjectsInSightDetector : MonoBehaviour {
    private bool toggle = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnBecameInvisible()
    {
        GetComponent<Renderer>().material.color = toggle ? Color.black : Color.white;   //TODO do something else
        toggle = !toggle;
        Debug.Log(this.name + " is invisible and " + (toggle ? "" : "not") + " toggled");
    }

    void OnBecameVisible()
    {
        Debug.Log(this.name + " is visible");
    }
}
