using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playSound(AudioClip currentAudio){
        audioSource.PlayOneShot(currentAudio);
    }

    public void playRandomSound(AudioClip[] audios){
        int randomSound = Random.Range(0,audios.Length);
        audioSource.PlayOneShot(audios[randomSound]);
    }
}
