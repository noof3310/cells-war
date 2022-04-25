using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip pickUpWBCSound,  placeTowerSound;

    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        pickUpWBCSound = Resources.Load<AudioClip> ("pickup");
        placeTowerSound = Resources.Load<AudioClip> ("placetower");
        
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSound(string soundType)
    {
        switch (soundType) {
            case "pick":
                audioSrc.PlayOneShot(pickUpWBCSound);
                break;
            case "place":
                audioSrc.PlayOneShot(placeTowerSound);
                break;
        }
    }
}
