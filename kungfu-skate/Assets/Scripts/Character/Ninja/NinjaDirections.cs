using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaDirections : MonoBehaviour
{
    private float latestXPos;
    private CharacterMovement ninja;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
