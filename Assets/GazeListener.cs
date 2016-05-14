using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface GazeListener
{
    void GameObjectFocused(GameObject gameObject);
    void GameObjectLostFocus(GameObject gameObject);
    void GameObjectGazed(GameObject gameObject);
}
