﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, HeadMovementListener, DirectPathOrderListener
{
    public float firstTickTime = 5;
    public float secondTickTime = 10;
    public float TimeBeforeSound = 0;
    private HeadMovementDetector headMovementDetector;
    private AudioSource ambiantSound;
    private float elapsedTime = 0;
    private bool firstTickPassed = false;
    private bool secondTickPassed = false;
    private bool sentinelChallenge = false;
    private bool movedTooMuch = false;
    private float timeBeforeSentinelExit = 0;
    private bool sentinelFirst = true;

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

        flickerLight();        
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


    void flickerLight() {
        var neonSound = GameObject.Find("NeonSound").GetComponent<AudioSource>();
        if (TimeBeforeSound <= 0)
        {
            neonSound.volume = 0.5f;
            neonSound.Play();
            System.Random rnd = new System.Random();
            //using fibonacci for randomness in time interval with up to 144 
            TimeBeforeSound = Fibonacci(rnd.Next(0, 1000) % 34);
        }
        else
        {
            
            TimeBeforeSound--;
            //neonSound.volume = 0;
        }
    }
    public static int Fibonacci(int n)
    {
        int a = 0;
        int b = 1;
        // In N steps compute Fibonacci sequence iteratively.
        for (int i = 0; i < n; i++)
        {
            int temp = a;
            a = b;
            b = temp + b;
        }
        return a;
    }

    void openNeon()
    {
        NeonScript neon = (NeonScript) GameObject.Find("Neon").GetComponent(typeof(NeonScript));
        Debug.Log(neon);
        neon.Open();
    }

    void closeNeon() {
        NeonScript neon = (NeonScript)GameObject.Find("Neon").GetComponent(typeof(NeonScript));
        Debug.Log(neon);
        neon.Close();
    }

    void manageSecondTick()
    {
        if(!secondTickPassed)
        {
            if (elapsedTime > secondTickTime)
            {
                secondTickPassed = true;
                //TODO open lights
                //TODO add sounds
                moveSentinel(new Vector3(-13.96f, -4.07f, 14.42f), 3, true);
            }
        }
    }

    public void moveSentinel(Vector3 destination, float speed, bool rotateAfter)
    {
        DirectPathOrder dpo = getSentinelDirectPathOrder();
        dpo.speed = speed;
        dpo.setDestination(destination, rotateAfter, this);
    }

    DirectPathOrder getSentinelDirectPathOrder()
    {
        GameObject sentinel = GameObject.Find("sentinel");
        return (DirectPathOrder)sentinel.GetComponent(typeof(DirectPathOrder));
    }

    void manageSentinelChallenge()
    {
        if (sentinelChallenge)
        {

            if (timeBeforeSentinelExit > 0 && Time.time > timeBeforeSentinelExit) {
                Debug.Log("Challenge over");
                sentinelChallenge = false;
                Vector3 destination = sentinelFirst ? new Vector3(-22.85f, -4.07f, 14.42f) : new Vector3(16.23f, -4.07f, 14.42f);
                moveSentinel(destination, 3, false);
            }
            headMovementDetector.Update();
        }
    }

    public void HeadMoved()
    {
        moveSentinel(Camera.main.transform.position - new Vector3(0, 4, 0), 15, false); //the -4y is because the camera is too high
    }

    public void HeadStopped(float elapsedTime)
    {
        if (!movedTooMuch)
        {
            if (elapsedTime < 0.5f)
            {
                //getSentinelDirectPathOrder().interrupt();
                //TODO make it go back in few seconds
            }
            else
            {
                //sentinel will keep coming toward us
                movedTooMuch = true;
            }
        }
    }

    public void SentinelIsWatching()
    {
        sentinelChallenge = true;
        timeBeforeSentinelExit = Time.time + 3;
        SoundPlayer.playSoundNamed("VO_DontMove", GameObject.Find("first_voice").GetComponent<AudioSource>());
    }

    public void SentinelExited()
    {
        Debug.Log("SentinelExited!!");

        SoundPlayer soundPlayer = (SoundPlayer)GetComponent(typeof(SoundPlayer));
        soundPlayer.addSound(2);

        if (sentinelFirst)
        {
            GameObject helper = (GameObject)Instantiate(helperType, new Vector3(0, 0, -1.2f), Quaternion.identity);
            DirectPathOrder dpo = (DirectPathOrder)helper.GetComponent(typeof(DirectPathOrder));
            dpo.setDestination(new Vector3(0, 0, 4), false, this);
            sentinelFirst = false;
        }
    }

    public void destinationReached(bool toKill)
    {
        Debug.Log("destinationReached: "+ toKill);
        if (!sentinelChallenge)
        {
            if(toKill)
            {
                SentinelIsWatching();
            } else
            {
                SentinelExited();
            }
        }
        else if(toKill)
        {
            SceneManager.LoadScene(2);
        } else
        {
            //helper
            Debug.Log("helper finished moving");
            GameObject tirroir = GameObject.Find("table_0002_tiroir_table");
            (tirroir.GetComponent("Halo") as Behaviour).enabled = true;
            SoundPlayer.playSoundNamed("VO_ThreePhrases", GameObject.Find("Helper(Clone)").GetComponent<AudioSource>());
        }
    }
}
