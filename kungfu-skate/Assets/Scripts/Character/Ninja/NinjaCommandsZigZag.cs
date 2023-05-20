using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommandsZigZag : MonoBehaviour
{
    private CharacterMovement ninja;
    private Vector2 arcOriginPos;
    private Vector2 arcDestPos;
    public int arcHeight;
    public int arcLength;
    int arcDir = 1;
    private bool resetArc = true;
    private Vector2 actualArcHeight;
    private float count = 0;
    public bool horizontalArc;
    public float zigZagSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            if(GetComponent<CharacterData>().reachedInitialDestPos &&
            !GetComponent<CharacterData>().shouldLeave) zigZagMovement();
        }    
    }

    void zigZagMovement(){
        if(!GetComponent<CharacterMovement>().rampedUp && !GetComponent<CharacterMovement>().rampedDown){
            if(resetArc) getArcInfo();
            else {
                if(ninja.ninjaPos != arcDestPos) moveInArc();
                else resetVars();
            }
        }
    }

    void getArcInfo(){
        arcDir *= -1;
        arcOriginPos = ninja.ninjaPos;
        if(horizontalArc) {
            arcDestPos = new Vector2(arcOriginPos.x+arcLength,arcOriginPos.y);
            actualArcHeight = arcOriginPos +(arcDestPos - arcOriginPos)/2 +Vector2.up *(arcHeight*arcDir);
        } else {
            arcDestPos = new Vector2(arcOriginPos.x,arcOriginPos.y+arcLength);
			actualArcHeight = arcOriginPos +(arcDestPos - arcOriginPos)/2 +Vector2.left *(arcHeight*arcDir); 
        }   
        resetArc = false;
    }

    void resetVars(){
        resetArc = true;
        count = 0;
    }

    void moveInArc(){
        count += zigZagSpeed *Time.deltaTime;
        Vector2 m1 = Vector2.Lerp( arcOriginPos, actualArcHeight, count );
        Vector2 m2 = Vector2.Lerp( actualArcHeight, arcDestPos, count );
        ninja.ninjaPos = Vector2.Lerp(m1, m2, count);
    }
}    