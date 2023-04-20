using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    private Animator bodyAnimator;
    private bool trackingState = true;
    private AudioController audioFX;
    
    void Start(){
        audioFX = GameObject.Find("audio").GetComponent<AudioController>();
        bodyAnimator = transform.Find("body").GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(trackingState) keepTrackHitPoints();    
    }

    void keepTrackHitPoints(){
        if(GetComponent<CharacterMovement>().isAlive) 
            if(GetComponent<CharacterData>().hitPoints <= 0) killPlayer();
        if(GetComponent<CharacterData>().hitPoints <= GetComponent<CharacterData>().explodeThreshold)
            explodeAndRemoveScript();
    }

    void explodeAndRemoveScript(){
        audioFX.playSound(GetComponent<CharacterData>().explode);
        GetComponent<CharacterMovement>().isExploded = true;
        Destroy(GetComponent<CharacterCollider>());
        Destroy(GetComponent<BoxCollider2D>());
        trackingState = false;
    }

    void killPlayer(){
        audioFX.playRandomSound(GetComponent<CharacterData>().die);
        GetComponent<CharacterMovement>().isAlive = false;
    }
}
