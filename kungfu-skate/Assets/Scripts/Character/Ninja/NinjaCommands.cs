using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public Vector2[] exitPos;
    private bool shouldLeave;
    public int moveType = 0;
    public float timeOnScreen = 5;
    private float actualTime = 0;
    private int direction = -1;
    public int ninjaSpeed = 100;
    
    public string exitType;
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
            !GetComponent<NinjaEnterExit>().shouldLeave) {
                moveNinja();
                countTimeOnScreen();
            }
        }    
    }

    void countTimeOnScreen(){
        actualTime += 1 * Time.deltaTime;
        if(actualTime > timeOnScreen) GetComponent<NinjaEnterExit>().shouldLeave = true;
    }

    void moveNinja(){
        switch(moveType){
            case 0:
                break;
            case 1:
                straightMovement();
                break;
            case 2:
                break;
        }
    }

    void straightMovement(){
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }
}
