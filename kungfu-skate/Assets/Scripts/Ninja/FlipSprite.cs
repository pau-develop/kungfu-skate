using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public bool isFliped;
    

    // Update is called once per frame
    void Update()
    {
        if(isFliped) flipSprite(true);
        else flipSprite(false);
    }

    void flipSprite(bool fliped){
        for(int i=0; i< transform.childCount;i++){
            transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = fliped;
        }
    }
}
