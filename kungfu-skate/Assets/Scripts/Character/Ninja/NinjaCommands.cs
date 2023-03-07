using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public int moveType = 0;
    private int direction = -1;
    public int ninjaSpeed = 100;
    private bool reachedZigZagEdge = false;
    private string zigZagDirection;
    // Start is called before the first frame update
    void Start()
    {
        if(moveType == 2) zigZagDirection = getDirection();
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = GetComponent<NinjaEnterExit>().initialDestPos;
    }

    string getDirection(){
        if(transform.position.y >= 100) return "bottom";
        else if(transform.position.y <= -100) return "top";
        else if(transform.position.x > 140) return "left";
        else return "right";
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
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }

    void zigZagMovement(){
        if(zigZagDirection == "left") horizontalZigZag(-1);
        else if(zigZagDirection == "right") horizontalZigZag(1);
        else if(zigZagDirection == "bottom") verticalZigZag(-1);
        else verticalZigZag(1);
    }

    void horizontalZigZag(int direction){
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }
    void verticalZigZag(int direction){
        ninjaPos.y += (ninjaSpeed * direction) * Time.deltaTime;
    }
}
