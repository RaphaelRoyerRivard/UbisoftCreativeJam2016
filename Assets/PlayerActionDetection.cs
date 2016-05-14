using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This class detects when the player performs an action.
/// The Update method must be called on each frame update.
/// </summary>
public class PlayerActionDetection : MonoBehaviour, PlayerActionListener
{
    PlayerActionDetector playerActionDetector;

    // Use this for initialization
    void Start()
    {
        playerActionDetector = new PlayerActionDetector(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerActionDetector.Update();
    }

    public void ActionDetected()
    {
        Debug.Log("ActionDetected");
        UnityEngine.VR.InputTracking.Recenter();
    }
}
