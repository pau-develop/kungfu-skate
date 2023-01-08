using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
private GameObject player;
    public bool movingUp =false;
    public bool movingDown =false;
    public bool movingLeft =false;
    public bool movingRight =false;

    public bool isShooting = false;
    public bool isSwinging =  false;
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        getVerticalMovement();
        getHorizontalMovement();
        getButtonInput();
       
    }

   void  getButtonInput(){
    if(Input.GetKey(KeyCode.M)) {
        isShooting = true;
        return;
    }
    if(Input.GetKey(KeyCode.N)){
        isSwinging = true;
        return;
    }
    isSwinging=false;
    isShooting=false;
   }

    void getVerticalMovement(){
        if(Input.GetKey(KeyCode.W)){
            movingDown = false;
            movingUp=true;
            return;
        }
        if(Input.GetKey(KeyCode.S)){
            movingUp = false;
            movingDown = true;
            return;
        }
        movingUp = false;
        movingDown = false;
    }  

    void getHorizontalMovement(){
        if(Input.GetKey(KeyCode.A)){
            movingRight = false;
            movingLeft=true;
            return;
        }
        if(Input.GetKey(KeyCode.D)){
            movingLeft = false;
            movingRight = true;
            return;
        }
        movingLeft = false;
        movingRight = false;
    }  
}
