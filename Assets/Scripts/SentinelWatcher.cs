using UnityEngine;
using UnityEngine.SceneManagement;

class SentinelWatcher : MonoBehaviour
{
    bool isMoving = false;
    bool isplaying = false;
    Vector3 lastPosition;
    AudioSource audioSource;
    AudioClip bonesSound;
    Rigidbody rigidbody;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!rigidbody.IsSleeping())
        {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
        else {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
