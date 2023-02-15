using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 50;
    private Vector2 initialDestPos = new Vector2(140,40);
    private Vector2 initialMoveDir;
    public bool reachedInitialDestPos = false;
    public int moveType = 0;
    private int direction = -1;
    private int ninjaSpeed = 80;
    // Start is called before the first frame update
    void Start()
    {
        ninjaPos = transform.position;
        initialMoveDir = (initialDestPos - ninjaPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) moveToInitialDestPos();
        else moveNinja();    
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
