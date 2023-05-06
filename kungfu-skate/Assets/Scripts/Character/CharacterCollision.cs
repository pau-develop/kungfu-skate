using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private AudioController audioFx;
    private BoxCollider2D charCollider;
    private CharacterData charData;
    private bool isPlayer;
    void Start(){
        charData = GetComponent<CharacterData>();
        audioFx = GameObject.Find("audio").GetComponent<AudioController>();
        charCollider = GetComponent<BoxCollider2D>();
        isPlayer = GetComponent<CharacterData>().isPlayer;
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="PlayerBullet" && !isPlayer) dealWithCollision(1);
        if(collider.gameObject.tag =="PlayerMelee" && !isPlayer) dealWithCollision(10);
        if(collider.gameObject.tag =="PlayerWave" && !isPlayer) dealWithCollision(5);
        if(collider.gameObject.tag =="EnemyBullet" && isPlayer) dealWithCollision(1);
    }

    void dealWithCollision(int damage){
        audioFx.playSound(charData.hitProjectile);
        GetComponent<CharacterData>().hitPoints-= damage;
        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<SwapSprites>().isBlinking = true;
    }
}
