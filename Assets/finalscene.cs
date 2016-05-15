using UnityEngine;
using System.Collections;

public class finalscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundPlayer.playSoundNamed("VO_Run", GetComponent<AudioSource>());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
