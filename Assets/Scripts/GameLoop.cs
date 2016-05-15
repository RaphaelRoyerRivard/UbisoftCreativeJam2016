using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    public float firstTickTime = 5;

    private AudioSource ambiantSound;
    private float elapsedTime = 0;
    private bool firstTickPassed = false;

    // Use this for initialization
    void Start () {
        ambiantSound = GameObject.Find("Audio Ambiance 1").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime;
        manageFirstTick();
	}

    void manageFirstTick()
    {
        if(!firstTickPassed)
        {
            fadeInAmbiantSound();
            if(elapsedTime > firstTickTime)
            {
                firstTickPassed = true;
                startFirstScreems();
                openNeon();
            }
        }
    }

    void fadeInAmbiantSound()
    {
        ambiantSound.volume = (elapsedTime / firstTickTime) * 0.5f;
    }

    void startFirstScreems()
    {
        SoundPlayer soundPlayer = (SoundPlayer)GetComponent(typeof(SoundPlayer));
        soundPlayer.addSound(2);
        firstTickPassed = true;
    }

    void openNeon()
    {
        NeonScript neon = (NeonScript) GameObject.Find("Neon").GetComponent(typeof(NeonScript));
        Debug.Log(neon);
        neon.Open();
    }
}
