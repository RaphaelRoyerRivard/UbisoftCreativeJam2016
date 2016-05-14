using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will check for Controller's base button press, spacebar key press and left click.
/// </summary>
public class PlayerActionDetector
{
    private PlayerActionListener listener;
    private bool pressing = false;

    public PlayerActionDetector(PlayerActionListener listener)
    {
        this.listener = listener;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
            if(!pressing)
            {
                listener.ActionDetected();
                pressing = true;
            }
        }
        else
        {
            pressing = false;
        }
    }
}