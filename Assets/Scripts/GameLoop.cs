using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour, HeadMovementListener, DirectPathOrderListener
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
    private float timeBeforeSentinelExit = 0;

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
                //TODO add sounds
                moveSentinel(new Vector3(-13.96f, -4.07f, 14.42f), 3, true);
            }
        }
    }

    void moveSentinel(Vector3 destination, float speed, bool rotateAfter)
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
            if (timeBeforeSentinelExit > 0 && elapsedTime > timeBeforeSentinelExit) {
                sentinelChallenge = false;
                moveSentinel(new Vector3(-19.85f, -4.07f, 14.42f), 3, false);
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
    }

    public void SentinelExited()
    {
        Debug.Log("SentinelExited!!");
        //TODO make helper apprear
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
            Debug.Log("Why does it pass here...");
        }
    }
}
