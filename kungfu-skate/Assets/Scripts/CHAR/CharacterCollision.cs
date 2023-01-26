using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private BoxCollider2D charCollider;
    void Start(){
        charCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="PlayerBullet") substractHitPoints();
    }

    void substractHitPoints(){
        GetComponent<CharacterData>().hitPoints--;
    }
}
