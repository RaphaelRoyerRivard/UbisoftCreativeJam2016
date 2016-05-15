using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    public float fadeInTimeFirstSounds = 5;

    private AudioSource ambiantSound;
    private float elapsedTime = 0;
    private bool firstSoundsManaged = false;

    // Use this for initialization
    void Start () {
        ambiantSound = GameObject.Find("Audio Ambiance 1").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime;
        fadeInAmbiantSound();
        ManageFirstScreems();
	}

    void fadeInAmbiantSound()
    {
        if(elapsedTime < fadeInTimeFirstSounds)
        {
            ambiantSound.volume = (elapsedTime / fadeInTimeFirstSounds) * 0.5f;
        }
    }

    void ManageFirstScreems()
    {
        if(!firstSoundsManaged && elapsedTime > fadeInTimeFirstSounds)
        {
            SoundPlayer soundPlayer = (SoundPlayer)GetComponent(typeof(SoundPlayer));
            soundPlayer.addSound(2);
            firstSoundsManaged = true;
        }
    }
}
