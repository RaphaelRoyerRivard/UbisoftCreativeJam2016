using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will look for angle change of the main camera.
/// The Update method must be call for it to properly detect the movements.
/// </summary>
public class HeadMovementDetector
{
    private const float ANGLE_THRESHOLD = 0.0002f;
    private const float MOVEMENT_TIME_THRESHOLD = 0.01f;

    private HeadMovementListener listener;
    private Vector3 lastEulerAngles;
    private float movementStartTime = 0;
    private bool moving = false;

    public HeadMovementDetector(HeadMovementListener listener)
    {
        this.listener = listener;
        lastEulerAngles = Camera.main.gameObject.transform.eulerAngles;
    }

    public void Update()
    {
        Vector3 currentEulerAngles = Camera.main.gameObject.transform.position;
        float deltaAngleX = Mathf.Abs(Mathf.DeltaAngle(currentEulerAngles.x, lastEulerAngles.x));
        float deltaAngleY = Mathf.Abs(Mathf.DeltaAngle(currentEulerAngles.y, lastEulerAngles.y));
        float deltaAngleZ = Mathf.Abs(Mathf.DeltaAngle(currentEulerAngles.z, lastEulerAngles.z));
        //Debug.Log(deltaAngleX > deltaAngleY ? (deltaAngleX > deltaAngleZ ? "x" : "z") : deltaAngleY > deltaAngleZ ? "y" : "z");
        if (deltaAngleX > ANGLE_THRESHOLD || deltaAngleY > ANGLE_THRESHOLD || deltaAngleZ > ANGLE_THRESHOLD)
        {
            if (movementStartTime == 0)
                movementStartTime = Time.time;
            else if (Time.time - movementStartTime > MOVEMENT_TIME_THRESHOLD)
            {
                moving = true;
                listener.HeadMoved();
            }
        }
        else if(movementStartTime > 0)
        {
            if (moving)
            {
                moving = false;
                listener.HeadStopped(Time.time - movementStartTime);
            }
            movementStartTime = 0;
        }
        lastEulerAngles = currentEulerAngles;
    }
}