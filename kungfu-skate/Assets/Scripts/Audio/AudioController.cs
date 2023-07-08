using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource fxSource;
    private AudioSource musicSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        fxSource = transform.Find("audio-fx").GetComponent<AudioSource>();
        musicSource = transform.Find("audio-music").GetComponent<AudioSource>();
        Debug.Log(fxSource);
        Debug.Log(musicSource);
    }

    // Update is called once per frame
    public void playSound(AudioClip currentAudio){
        fxSource.PlayOneShot(currentAudio);
    }

    public void playRandomSound(AudioClip[] audios){
        int randomSound = Random.Range(0,audios.Length);
        fxSource.PlayOneShot(audios[randomSound]);
    }

    public void playMusic(AudioClip currentClip){
        Debug.Log(musicSource);
        musicSource.clip = currentClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
