using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider : MonoBehaviour
{
    private BoxCollider2D charCollider;
    private float xOffset;
    private float currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        charCollider = GetComponent<BoxCollider2D>();
        xOffset = charCollider.offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        changeColliderPosition();
    }

    void changeColliderPosition(){
        if(GetComponent<FlipSprite>()){
            if(GetComponent<FlipSprite>().isFliped) currentOffset = xOffset * -1;
            else currentOffset = xOffset * 1;
        }
        charCollider.offset = new Vector2(currentOffset, charCollider.offset.y);
    }
}
