using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    private Animator bodyAnimator;
    private bool trackingState = true;
    
    void Start(){
        bodyAnimator = transform.Find("body").GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(trackingState) keepTrackHitPoints();    
    }

    void keepTrackHitPoints(){
        if(GetComponent<CharacterData>().hitPoints <= 0) 
            GetComponent<CharacterMovement>().isAlive = false;
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
