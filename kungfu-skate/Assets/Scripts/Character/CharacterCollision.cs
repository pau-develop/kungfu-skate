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
        if(collider.gameObject.tag =="PlayerBullet") dealWithCollision();
    }

    void dealWithCollision(){
        GetComponent<CharacterData>().hitPoints--;
        Debug.Log(transform.childCount);
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<SwapSprites>().isBlinking = true;
        }
        
    }
}
