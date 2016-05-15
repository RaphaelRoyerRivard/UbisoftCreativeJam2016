using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This class is designed to be used on the GazePointer prefab.
/// </summary>
public class GazePointer : MonoBehaviour, GazeListener, DirectPathOrderListener
{
    GazeDetector gazeDetector;
    GameObject selectedKey;
    GameObject selectedLock;
    int locks = 0;
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

            gameObject.SetActive(false);
            SoundPlayer.playSoundNamed("VO_FirstKey", GameObject.Find("Helper(Clone)").GetComponent<AudioSource>());

            GameLoop gameLoop = Camera.main.GetComponent<GameLoop>();
            gameLoop.moveSentinel(new Vector3(-13.96f, -4.07f, 14.42f), 3, true);
        }
        else if (gameObject.CompareTag("lock") && selectedKey != null)
        {
            GameObject helper = GameObject.Find("Helper(Clone)");
            DirectPathOrder dpo = (DirectPathOrder)helper.GetComponent(typeof(DirectPathOrder));
            dpo.setDestination(gameObject.transform.position, false, this);

            selectedLock = gameObject;
        }
        else if (gameObject.CompareTag("dresser"))
        {
            if (!dressermoved && (gameObject.GetComponent("Halo") as Behaviour).enabled)
            {
                (gameObject.GetComponent("Halo") as Behaviour).enabled = false;
                GameObject helper = GameObject.Find("Helper(Clone)");
                DirectPathOrder dpo = (DirectPathOrder)helper.GetComponent(typeof(DirectPathOrder));
                dpo.setDestination(gameObject.transform.position, false, this);
            }
        }
    }

    public void destinationReached(GameObject subject, bool rotatedAfter)
    {
        if (!dressermoved)
        {
            GameObject tiroir = GameObject.Find("table_0002_tiroir_table");
            tiroir.transform.position += new Vector3(0, 0, -1f);
            SoundPlayer.playSoundNamed("SFX_DrawerOpen", tiroir.GetComponent<AudioSource>());
            GameObject.Find("dresserKey").transform.position += new Vector3(0, 0.4f, 0f);
            dressermoved = true;
        } else if(selectedLock != null)
        {
            SoundPlayer.playSoundNamed("SFX_KeyInsert", selectedLock.GetComponent<AudioSource>());
            Destroy(selectedLock);
            //Destroy(selectedKey);
            //selectedKey = null;
            selectedLock = null;
            locks++;
            if(locks == 2)
            {
                //TODO close lights and play
                SoundPlayer.playSoundNamed("VO_Run", GameObject.Find("Helper(Clone)").GetComponent<AudioSource>());
            }
        }
    }
}
