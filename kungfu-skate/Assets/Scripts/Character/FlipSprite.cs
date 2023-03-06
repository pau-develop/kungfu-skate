using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public bool isFliped;
    private CharacterMovement ninja;

    // Update is called once per frame
    void Update()
    {
        ninja = GetComponent<CharacterMovement>();
        if(ninja.isAlive){
            if(isFliped) flipSprite(true);
            else flipSprite(false);
        }
    }

    void flipSprite(bool fliped){
        for(int i=0; i< transform.childCount;i++){
            transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = fliped;
        }
    }
}
