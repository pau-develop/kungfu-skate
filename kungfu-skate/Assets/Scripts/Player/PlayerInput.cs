using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
private PlayerMovement player;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
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
        player.isShooting = true;
        return;
    }
    if(Input.GetKey(KeyCode.N)){
        player.isSwinging = true;
        return;
    }
    player.isSwinging = false;
    player.isShooting = false;
   }

    void getVerticalMovement(){
        if(Input.GetKey(KeyCode.W)){
            player.movingDown = false;
            player.movingUp=true;
            return;
        }
        if(Input.GetKey(KeyCode.S)){
            player.movingUp = false;
            player.movingDown = true;
            return;
        }
        player.movingUp = false;
        player.movingDown = false;
    }  

    void getHorizontalMovement(){
        if(Input.GetKey(KeyCode.A)){
            player.movingRight = false;
            player.movingLeft=true;
            return;
        }
        if(Input.GetKey(KeyCode.D)){
            player.movingLeft = false;
            player.movingRight = true;
            return;
        }
        player.movingLeft = false;
        player.movingRight = false;
    }  
}
