using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
private CharacterMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GlobalData.gamePaused){
            getVerticalMovement();
            getHorizontalMovement();
            getButtonInput();
        }
       
    }

   void  getButtonInput(){
    if(Input.GetKey(KeyCode.M)) {
        playerMovement.isShooting = true;
        playerMovement.isSwinging = false;
        return;
    }
    if(Input.GetKey(KeyCode.N)){
        playerMovement.isSwinging = true;
        playerMovement.isShooting = false;
        return;
    }
    playerMovement.isSwinging = false;
    playerMovement.isShooting = false;
   }

    void getVerticalMovement(){
        if(Input.GetKey(KeyCode.W)){
            playerMovement.movingDown = false;
            playerMovement.movingUp=true;
            return;
        }
        if(Input.GetKey(KeyCode.S)){
            playerMovement.movingUp = false;
            playerMovement.movingDown = true;
            return;
        }
        playerMovement.movingUp = false;
        playerMovement.movingDown = false;
    }  

    void getHorizontalMovement(){
        if(Input.GetKey(KeyCode.A)){
            playerMovement.movingRight = false;
            playerMovement.movingLeft=true;
            return;
        }
        if(Input.GetKey(KeyCode.D)){
            playerMovement.movingLeft = false;
            playerMovement.movingRight = true;
            return;
        }
        playerMovement.movingLeft = false;
        playerMovement.movingRight = false;
    }  
}
