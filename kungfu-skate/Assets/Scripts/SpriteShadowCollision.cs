using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadowCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") dealWithLayerTrigger(collider);
    }

    void dealWithLayerTrigger(Collider2D collider){
        int obstacleXPos = (int)collider.gameObject.transform.position.x;
        if(transform.position.x <= obstacleXPos) GetComponent<SpriteLayer>().leftLayer = true;
        else GetComponent<SpriteLayer>().leftLayer = false;
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") GetComponent<SpriteLayer>().leftLayer = true;
    }
}
