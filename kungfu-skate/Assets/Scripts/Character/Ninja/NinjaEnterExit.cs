﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnterExit : MonoBehaviour
{
    private float actualTime = 0;
    public float timeOnScreen = 5;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public Vector2 initialDestPos;
    float ninjaSpeed = 100;
    public string exitType;
    private Vector2[] exitPos;
    private int exitIndex = 0;
    private bool gotExitPosition = false;
    private CharacterMovement ninja;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<CharacterMovement>().isAlive){
            if(!GetComponent<CharacterData>().reachedInitialDestPos) moveToInitialDestPos();
            else countTimeOnScreen();
            if(GetComponent<CharacterData>().shouldLeave) {
                if(!gotExitPosition) getExitPosition();
                moveToExitPos();   
            }
        }
    }

    void countTimeOnScreen(){
        actualTime += 1 * Time.deltaTime;
        if(actualTime > timeOnScreen) GetComponent<CharacterData>().shouldLeave = true;
    }

     void getExitPosition(){
        ninjaPos = transform.position;
        switch(exitType){
            case "top":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(transform.position.x, 110);
                break;
            case "left":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(-181, transform.position.y);
                break;
            case "right":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(+181, transform.position.y);
                break;
            case "top-right":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, 45);
                exitPos[1] = new Vector2(+181, 45);
                break;
            case "top-left":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, 45);
                exitPos[1] = new Vector2(-181, 45);
                break;
            case "bot-left":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, -90);
                exitPos[1] = new Vector2(-181, -90);
                break;
            case "bot-right":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, -90);
                exitPos[1] = new Vector2(181, -90);
                break;
        }
        gotExitPosition =  true;
    }

    void moveToInitialDestPos(){
        float step = initialMoveSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, initialDestPos, step);
        if(ninjaPos == initialDestPos) GetComponent<CharacterData>().reachedInitialDestPos = true;
        ninja.ninjaPos = ninjaPos;
    }

    void moveToExitPos(){
        float step = ninjaSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, exitPos[exitIndex], step);
        if(ninjaPos == exitPos[exitIndex]) exitIndex++;
        ninja.ninjaPos = ninjaPos;
    }
}
