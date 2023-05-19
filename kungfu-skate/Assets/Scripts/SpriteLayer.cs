using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour
{
    public bool leftLayer = true;
    private SpriteRenderer[] childSprites;
    private SpriteRenderer sprite;
    // Start is called before the first frame update

    void Start(){
        if(transform.GetComponent<SpriteRenderer>() == null) getChildSprites();
        else getSprite();
    }

    private void getSprite(){
        childSprites = new SpriteRenderer[1];
        childSprites[0] = GetComponent<SpriteRenderer>();
    }

    private void getChildSprites(){
        childSprites = new SpriteRenderer[transform.childCount];
        for(int i = 0; i < childSprites.Length; i++) childSprites[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        changeSpriteLayer(leftLayer);
    }

    private void changeSpriteLayer(bool leftLayer){
        string layerName;
        if(leftLayer) layerName = "LeftLayer";
        else layerName = "RightLayer";
        for(int i = 0; i < childSprites.Length; i++) childSprites[i].sortingLayerName = layerName; 
    }
}
