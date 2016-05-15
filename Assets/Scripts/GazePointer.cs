using UnityEngine;
using System.Collections;

/// <summary>
/// This class is designed to be used on the GazePointer prefab.
/// </summary>
public class GazePointer : MonoBehaviour, GazeListener
{
    GazeDetector gazeDetector;
    GameObject selectedKey;
    bool dressermoved = false;

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
        if((selectedKey == null && gameObject.CompareTag("key")) || (selectedKey != null && gameObject.CompareTag("lock")))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void GameObjectLostFocus(GameObject gameObject)
    {
        Debug.Log("GameObjectLostFocus: " + gameObject);
        if (gameObject.CompareTag("key") && selectedKey == null || gameObject.CompareTag("lock"))
            gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void GameObjectGazed(GameObject gameObject)
    {
        Debug.Log("GameObjectGazed: " + gameObject);
        
        if (gameObject.CompareTag("key"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            selectedKey = gameObject;
        }
        else if (gameObject.CompareTag("lock") && selectedKey != null)
        {
            Destroy(gameObject);
            Destroy(selectedKey);
            selectedKey = null;
        }
        else if (gameObject.CompareTag("dresser"))
        {
            if (!dressermoved) {

                (gameObject.GetComponent("Halo") as Behaviour).enabled = false;
                var key = GameObject.Find("dresserKey");

                gameObject.transform.position += new Vector3(0,0,-1f);
                key.transform.position += new Vector3(0, 0.4f, 0f);

                dressermoved = true;
            }
        }

    }
}
