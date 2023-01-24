using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrace : MonoBehaviour
{
    
    private GameObject playerShadow;
    private int traceSpeed = 50;
    private Vector2 tracePos;
    
    private int stackLayer= 0;

    private float traceTimer = 0;
    private float traceInterval = 0.08f;

    private Color32 dogColor = new Color32(255,75,50,125);
    private Color32 boyColor = new Color32(50,75,255,125);
    private Color32 girlColor = new Color32(50,255,50,125);
    private Color32 currentColor;
    // Start is called before the first frame update
    void Start()
    {
        tracePos = new Vector2(transform.position.x-2,transform.position.y);  
    }

    Color32 getCurrentColor(){
        string spriteName = transform.Find("body").GetComponent<SwapSprites>().spriteSheetName;
        if(spriteName=="CHAR1") return boyColor;
        else if(spriteName=="CHAR2") return girlColor;
        else return dogColor;
    }

    void Update(){
        if(!GetComponent<CharacterMovement>().isGrounded) instantiateTraces();
    }

    void instantiateTraces(){
        traceTimer+= Time.deltaTime;
        if(traceTimer>= traceInterval){
            createTrace();
            traceTimer = 0;
        }
    }

    void createTrace(){
        playerShadow = new GameObject("player-trace");
        SpriteRenderer[] sprites = new SpriteRenderer[transform.childCount];
        for(int i=0;i< sprites.Length;i++){
              GameObject tempObj = new GameObject();
              tempObj.AddComponent<SpriteRenderer>().sprite = transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
              tempObj.transform.SetParent(playerShadow.transform);
              currentColor = getCurrentColor();
              tempObj.GetComponent<SpriteRenderer>().color = new Color32(currentColor.r,currentColor.g,currentColor.b,100);
              tempObj.GetComponent<SpriteRenderer>().sortingOrder = transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder-5-stackLayer;
              stackLayer++;
        }
        if(stackLayer==10) stackLayer=0;
        playerShadow.transform.position = transform.position;
        playerShadow.AddComponent<SpriteTraceMovement>();
        
    }
}
