using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private AudioFX audioFx;
    private BoxCollider2D charCollider;
    private CharacterData charData;
    void Start(){
        charData = GetComponent<CharacterData>();
        audioFx = GameObject.Find("audio-fx").GetComponent<AudioFX>();
        charCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="PlayerBullet") dealWithCollision(1);
        if(collider.gameObject.tag =="PlayerMelee") dealWithCollision(10);
    }

    void dealWithCollision(int damage){
        audioFx.playSound(charData.hit);
        GetComponent<CharacterData>().hitPoints-= damage;
        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<SwapSprites>().isBlinking = true;
    }
}
