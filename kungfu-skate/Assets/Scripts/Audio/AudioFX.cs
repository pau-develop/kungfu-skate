using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip char1Shoot;
    public AudioClip char1Swing;
    public AudioClip char1Die;
    public AudioClip char1Hit;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playSound(AudioClip currentAudio)
    {
        audioSource.PlayOneShot(currentAudio);
    }
}
