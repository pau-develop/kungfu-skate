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
        if(collider.gameObject.tag =="PlayerBullet" && !isPlayer) {
            dealWithCollision(1);
            addPoints(false);
        }
        if(collider.gameObject.tag =="PlayerMelee" && !isPlayer){
            dealWithCollision(10);
            addPoints(true);
        } 
        if(collider.gameObject.tag =="PlayerWave" && !isPlayer) dealWithCollision(5);
        if(collider.gameObject.tag =="EnemyBullet" && isPlayer) dealWithCollision(1);
    }

    void addPoints(bool isMeleeAttack){
        if(isMeleeAttack) GlobalData.playerOneScore += 50;
        else GlobalData.playerOneScore += 25;
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") dealWithLayerTrigger(collider);
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") GetComponent<CharacterLayer>().leftLayer = true;
    }

    void dealWithLayerTrigger(Collider2D collider){
        int obstacleXPos = (int)collider.gameObject.transform.position.x;
        if(transform.position.x <= obstacleXPos) GetComponent<CharacterLayer>().leftLayer = true;
        else GetComponent<CharacterLayer>().leftLayer = false;
    }

    void dealWithCollision(int damage){
        audioFx.playSound(charData.hitProjectile);
        GetComponent<CharacterData>().hitPoints-= damage;
        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<SwapSprites>().isBlinking = true;
    }
}
