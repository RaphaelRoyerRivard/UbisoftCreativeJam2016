using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayer : MonoBehaviour {
    public float minSoundDelay = 1;
    public float maxSoundDelay = 10;
    
    AudioClip[][] audioClips = new AudioClip[5][];
    AudioSource[] audioSources = new AudioSource[5];
    float[] timeForNextSound = new float[5];
    int soundCount = 0;

    // Use this for initialization
    void Start ()
    {
        audioClips[0] = new AudioClip[3];
        audioClips[1] = new AudioClip[3];
        audioClips[2] = new AudioClip[5];
        audioClips[3] = new AudioClip[5];
        audioClips[4] = new AudioClip[9];

        for (int i = 0; i < audioClips[0].Length; i++)
            audioClips[0][i] = (AudioClip)Resources.Load("SFX_Torture_Female1_0" + (i + 1));

        for (int i = 0; i < audioClips[1].Length; i++)
            audioClips[1][i] = (AudioClip)Resources.Load("SFX_Torture_Female2_0" + (i + 1));

        for (int i = 0; i < audioClips[2].Length; i++)
            audioClips[2][i] = (AudioClip)Resources.Load("SFX_Torture_Male1_0" + (i + 1));

        /*for (int i = 0; i < audioClips[3].Length; i++)
            audioClips[3][i] = (AudioClip)Resources.Load("SFX_Torture_Male2_0" + (i + 1));*/

        for (int i = 0; i < audioClips[3].Length; i++)
            audioClips[3][i] = (AudioClip)Resources.Load("SFX_Torture_Male2_Intense_0" + (i + 1));

        for (int i = 0; i < audioClips[4].Length; i++)
            audioClips[4][i] = (AudioClip)Resources.Load("SFX_Torture_Male3_0" + (i + 1));

        audioSources[0] = GameObject.Find("Female 1 Torture Audio Source").GetComponent<AudioSource>();
        audioSources[1] = GameObject.Find("Female 2 Torture Audio Source").GetComponent<AudioSource>();
        audioSources[2] = GameObject.Find("Male 1 Torture Audio Source").GetComponent<AudioSource>();
        audioSources[3] = GameObject.Find("Male 2 Torture Audio Source").GetComponent<AudioSource>();
        audioSources[4] = GameObject.Find("Male 3 Torture Audio Source").GetComponent<AudioSource>();

        for (int i = 0; i < timeForNextSound.Length; i++)
                timeForNextSound[i] = Random.Range(minSoundDelay, maxSoundDelay);
    }

    public void addSound(int qty)
    {
        soundCount += qty;
    }
	
	// Update is called once per frame
	void Update () {
        for(int i=0; i<soundCount; i++)
        {
            timeForNextSound[i] -= Time.deltaTime;
            if (timeForNextSound[i] <= 0)
            {
                PlaySound(i);
                timeForNextSound[i] = Random.Range(minSoundDelay, maxSoundDelay);
            }
        }
	}

    void PlaySound(int index)
    {
        int soundIndex = (int) Mathf.Floor(Random.Range(0, audioClips[index].Length));
        audioSources[index].PlayOneShot(audioClips[index][soundIndex]);
    }

    public static void playSoundNamed(string sound, AudioSource audioSource)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load(sound));
    }


}
