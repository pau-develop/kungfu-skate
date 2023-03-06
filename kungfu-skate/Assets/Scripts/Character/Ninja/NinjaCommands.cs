using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public Vector2 initialDestPos;
    public Vector2[] exitPos;
    private bool shouldLeave;
    private Vector2 initialMoveDir;
    public bool reachedInitialDestPos = false;
    public int moveType = 0;
    public float timeOnScreen = 5;
    private float actualTime = 0;
    public Vector2 exitScreenPosition = new Vector2(140,110);
    private int exitIndex = 0;
    private int direction = -1;
    public int ninjaSpeed = 100;
    private float latestXPos;
    public string exitType;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = GetComponent<NinjaEnterExit>().initialDestPos;
        initialMoveDir = (initialDestPos - ninjaPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            if(GetComponent<NinjaEnterExit>().reachedInitialDestPos &&
            !GetComponent<NinjaEnterExit>().shouldLeave) {
                moveNinja();
                countTimeOnScreen();
            }
            setNinjaDirections();
        }    
    }

    void countTimeOnScreen(){
        actualTime += 1 * Time.deltaTime;
        if(actualTime > timeOnScreen) {
            GetComponent<NinjaEnterExit>().shouldLeave = true;
        }
    }

    void setNinjaDirections(){
        if(transform.position.x > latestXPos){
            ninja.movingLeft = false;
            ninja.movingRight = true;
        }
        else if(transform.position.x < latestXPos){
            ninja.movingRight = false;
            ninja.movingLeft = true;
        } 
        else{
            ninja.movingLeft = false;
            ninja.movingRight = false;
        }
        latestXPos = transform.position.x;
    }

    void moveNinja(){
        switch(moveType){
            case 0:
                break;
            case 1:
                straightMovement();
                break;
            case 2:
                oscillationMovement();
                break;
        }
    }

    void oscillationMovement(){
        
    }

    void straightMovement(){
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }
}
