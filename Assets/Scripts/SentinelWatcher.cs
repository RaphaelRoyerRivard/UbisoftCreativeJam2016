using UnityEngine;
using UnityEngine.SceneManagement;

class SentinelWatcher : MonoBehaviour
{
    bool isMoving = false;
    Vector3 lastPosition;
    AudioSource audioSource;
    AudioClip bonesSound;

    void Start()
    {
        lastPosition = transform.position;
        bonesSound = (AudioClip)Resources.Load("SFX_BoneRub");
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.PlayOneShot(bonesSound);
        audioSource.volume = 0;
    }

    void Update()
    {
        if(!isMoving)
        {
            if(lastPosition != transform.position)
            {
                //Debug.Log("startMoving");
                isMoving = true;
                startSoundLoop();
            }
        } else
        {
            if (lastPosition == transform.position)
            {
                //Debug.Log("stopMoving");
                isMoving = false;
                stopSoundLoop();
            }
        }
        lastPosition = transform.position;
    }

    void startSoundLoop()
    {
        audioSource.volume = 1;
    }

    void stopSoundLoop()
    {
        audioSource.volume = 0;
    }
}
