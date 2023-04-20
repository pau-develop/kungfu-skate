using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource fxSource;

    
    // Start is called before the first frame update
    void Start()
    {
        fxSource = transform.Find("audio-fx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playSound(AudioClip currentAudio){
        fxSource.PlayOneShot(currentAudio);
    }

    public void playRandomSound(AudioClip[] audios){
        int randomSound = Random.Range(0,audios.Length);
        fxSource.PlayOneShot(audios[randomSound]);
    }
}
