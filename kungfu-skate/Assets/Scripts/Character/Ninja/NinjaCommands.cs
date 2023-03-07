using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    public int moveType = 0;
    private int direction = -1;
    public int ninjaSpeed = 100;
    private Vector2 arcOriginPos;
    private Vector2 arcDestPos;
    int arcHeight = 40;
    int arcLenght = 40;
    int arcDir = 1;
    int zigZagSpeed = 2;
    private string zigZagDirection;
    private bool resetArc = true;
    private Vector2 actualArcHeight;
    private float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = GetComponent<NinjaEnterExit>().initialDestPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            if(GetComponent<NinjaEnterExit>().reachedInitialDestPos &&
            !GetComponent<NinjaEnterExit>().shouldLeave) moveNinja();
        }    
    }

    void moveNinja(){
        switch(moveType){
            case 0:
                break;
            case 1:
                straightMovement();
                break;
            case 2:
                zigZagMovement();
                break;
        }
    }

    void straightMovement(){
        ninja.ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }

    void zigZagMovement(){
        if(resetArc) getArcInfo();
        else {
            if(ninja.ninjaPos.x != arcDestPos.x) moveInArc();
            else resetVars();
        }
    }

    void getArcInfo(){
        arcDir *= -1;
        arcOriginPos = ninja.ninjaPos;
        arcDestPos = new Vector2(arcOriginPos.x+arcLenght,arcOriginPos.y);
        actualArcHeight = arcOriginPos +(arcDestPos -arcOriginPos)/2 +Vector2.up *(arcHeight*arcDir);
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