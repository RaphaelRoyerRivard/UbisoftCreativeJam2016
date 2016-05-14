using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class checks if a GameObject is in the center of the main camera's sight.
/// The Update method must be call on each frame update.
/// </summary>
public class GazeDetector
{
    private const float GAZE_DURATION_THRESHOLD = 1f;

    private GazeListener listener;
    private GameObject currentlyFocused;
    private float gazeStartTime = 0;
    private bool gazed = false;
    Vector3 screenPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    Ray ray;
    RaycastHit hit;

    public GazeDetector(GazeListener listener)
    {
        this.listener = listener;
    }

    public void Update()
    {
        ray = Camera.main.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if(objectHit.gameObject != currentlyFocused)
            {
                if (currentlyFocused != null)
                {
                    listener.GameObjectLostFocus(currentlyFocused);
                    gazed = false;
                    gazeStartTime = 0;
                }
                currentlyFocused = objectHit.gameObject;
                listener.GameObjectFocused(currentlyFocused);
                gazeStartTime = Time.time;
            }
            if (!gazed && Time.time - gazeStartTime > GAZE_DURATION_THRESHOLD)
            {
                listener.GameObjectGazed(currentlyFocused);
                gazed = true;
            }
        }
        else if (currentlyFocused != null)
        {
            listener.GameObjectLostFocus(currentlyFocused);
            currentlyFocused = null;
            gazed = false;
            gazeStartTime = 0;
        }
    }
}