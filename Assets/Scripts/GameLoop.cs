using UnityEngine;
using System.Collections;
using System;

public class GameLoop : MonoBehaviour, HeadMovementListener
{
    public float firstTickTime = 5;
    public float secondTickTime = 10;

    private HeadMovementDetector headMovementDetector;
    private AudioSource ambiantSound;
    private float elapsedTime = 0;
    private bool firstTickPassed = false;
    private bool secondTickPassed = false;
    private bool sentinelChallenge = false;
    private bool movedTooMuch = false;

    // Use this for initialization
    void Start () {
        ambiantSound = GameObject.Find("Audio Ambiance 1").GetComponent<AudioSource>();
        headMovementDetector = new HeadMovementDetector(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime;
        manageFirstTick();
        manageSecondTick();
        manageSentinelChallenge();
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

    void manageSecondTick()
    {
        if(!secondTickPassed)
        {
            if (elapsedTime > secondTickTime)
            {
                secondTickPassed = true;
                //TODO open lights
                moveSentinel();
            }
        }
    }

    void moveSentinel()
    {
        //TODO start sentinel pattern
        sentinelChallenge = true;
    }

    void manageSentinelChallenge()
    {
        if (sentinelChallenge)
            headMovementDetector.Update();
    }

    public void HeadMoved()
    {
        //TODO sentinel should rotate and come toward us
    }

    public void HeadStopped(float elapsedTime)
    {
        if (!movedTooMuch)
        {
            if (elapsedTime < 0.5f)
            {
                //TODO sentinel should stop moving toward us
            }
            else
            {
                //sentinel should keep coming toward us
                movedTooMuch = true;
            }
        }
    }

    public void SentinelExited()
    {
        //TODO trigger next
    }
}
