using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommandsLine : MonoBehaviour
{
    public int ninjaSpeed = 80;
    private CharacterMovement ninja;
    public bool horizontalMovement;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        direction = getDirection();
    }

    int getDirection(){
        if(horizontalMovement){
            if(transform.position.x >= 0) return -1;
            else return 1;
        } else {
            if(transform.position.y >= 0) return -1;
            else return 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            if(GetComponent<CharacterData>().reachedInitialDestPos &&
            !GetComponent<CharacterData>().shouldLeave) lineMovement();
        }
    }

    void lineMovement(){
        if(!GetComponent<CharacterMovement>().rampedUp && !GetComponent<CharacterMovement>().rampedDown){
            if(horizontalMovement) ninja.ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
            else ninja.ninjaPos.y += (ninjaSpeed * direction) * Time.deltaTime;

            if(!horizontalMovement & GetComponent<CharacterMovement>().isGrounded) GetComponent<CharacterData>().shouldLeave = true;
        }
    }
}