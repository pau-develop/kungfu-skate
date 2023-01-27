﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private AudioFX audioFx;
    private BoxCollider2D charCollider;
    void Start(){
        audioFx = GameObject.Find("audio-fx").GetComponent<AudioFX>();
        charCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="PlayerBullet") dealWithCollision();
    }

    void dealWithCollision(){
        audioFx.playSound(audioFx.char1Hit);
        GetComponent<CharacterData>().hitPoints--;
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<SwapSprites>().isBlinking = true;
        }
        
    }
}
