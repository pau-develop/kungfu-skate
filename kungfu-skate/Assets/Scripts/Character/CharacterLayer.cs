using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLayer : MonoBehaviour
{
    public bool leftLayer = true;
    private SpriteRenderer[] childSprites;
    // Start is called before the first frame update

    void Start(){
        getChildSprites();
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
