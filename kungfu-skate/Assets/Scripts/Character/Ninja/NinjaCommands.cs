using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public Vector2 initialDestPos;
    private Vector2 initialMoveDir;
    public bool reachedInitialDestPos = false;
    public int moveType = 0;
    private int direction = -1;
    private int ninjaSpeed = 80;
    private float latestXPos;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = transform.position;
        initialMoveDir = (initialDestPos - ninjaPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) moveToInitialDestPos();
        else moveNinja();
        setNinjaDirections();    
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

    void moveToInitialDestPos(){
        float step = initialMoveSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, initialDestPos, step);
        if(ninjaPos == initialDestPos) reachedInitialDestPos = true;
    }

    void moveNinja(){
        switch(moveType){
            case 0:
                straightMovement();
                break;
        }
    }

    void straightMovement(){
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }
}
