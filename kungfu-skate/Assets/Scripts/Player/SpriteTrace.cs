using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrace : MonoBehaviour
{
    public SpriteRenderer[] sprites = new SpriteRenderer[3];
    private GameObject playerShadow;
    private int traceSpeed = 50;
    private Vector2 tracePos;
    
    private int stackLayer= 0;
    // Start is called before the first frame update
    void Start()
    {
        tracePos = new Vector2(transform.position.x-2,transform.position.y);
        createTrace();
    }

    void createTrace(){
        sprites[0] = transform.Find("legs").GetComponent<SpriteRenderer>();
        sprites[1] = transform.Find("body").GetComponent<SpriteRenderer>();
        sprites[2] = transform.Find("arms").GetComponent<SpriteRenderer>();
        playerShadow = new GameObject("player-trace");
        for(int i=0;i< sprites.Length;i++){
              GameObject tempObj = new GameObject();
              tempObj.AddComponent<SpriteRenderer>().sprite = sprites[i].sprite;
              tempObj.transform.SetParent(playerShadow.transform);
              tempObj.GetComponent<SpriteRenderer>().color = new Color32(30,0,255,255);
              tempObj.GetComponent<SpriteRenderer>().sortingOrder = sprites[i].sortingOrder-5-stackLayer;
              stackLayer++;
        }
        if(stackLayer==10)stackLayer=0;
        playerShadow.transform.position = tracePos;
        playerShadow.AddComponent<SpriteTraceMovement>();
        
    }
}
