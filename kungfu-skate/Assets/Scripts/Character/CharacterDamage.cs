using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    private Animator bodyAnimator;
    private bool trackingState = true;
    private AudioFX audioFX;
    
    void Start(){
        audioFX = GameObject.Find("audio-fx").GetComponent<AudioFX>();
        bodyAnimator = transform.Find("body").GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(trackingState) keepTrackHitPoints();    
    }

    void keepTrackHitPoints(){
        if(GetComponent<CharacterMovement>().isAlive){
            if(GetComponent<CharacterData>().hitPoints <= 0){
                Debug.Log(this.GetComponent<CharacterData>().die);
                Debug.Log(this.GetComponent<CharacterData>().die.Length);
                audioFX.playRandomSound(GetComponent<CharacterData>().die);
                GetComponent<CharacterMovement>().isAlive = false;
            } 
        }
        if(GetComponent<CharacterData>().hitPoints <= GetComponent<CharacterData>().explodeThreshold)
            explodeAndRemoveScript();
    }

    void explodeAndRemoveScript(){
        bodyAnimator.Play("body-explode");
        Destroy(GetComponent<CharacterCollider>());
        Destroy(GetComponent<BoxCollider2D>());
        trackingState = false;
    }
}
