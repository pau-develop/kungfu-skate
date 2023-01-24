﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1)) swapPlayerSprites("1");
        if(Input.GetKeyUp(KeyCode.Alpha2)) swapPlayerSprites("2");
        if(Input.GetKeyUp(KeyCode.Alpha3)) swapPlayerSprites("3");
        if(Input.GetKeyUp(KeyCode.B)) blinkPlayer();
        if(Input.GetKeyUp(KeyCode.F)) killPlayer();
    }

    void blinkPlayer(){
        player.transform.Find("body").GetComponent<SwapSprites>().isBlinking =true;
        if(player.transform.Find("legs") !=null)
            player.transform.Find("legs").GetComponent<SwapSprites>().isBlinking =true;
        if(player.transform.Find("arms") !=null)
            player.transform.Find("arms").GetComponent<SwapSprites>().isBlinking =true;
    }

    void killPlayer(){
        if(GameObject.Find("player").GetComponent<PlayerMovement>().isAlive){ 
            GameObject.Find("player").GetComponent<PlayerMovement>().isAlive = false;
            GameObject.Find("ninja").GetComponent<PlayerMovement>().isAlive = false;
        }
        else { 
            GameObject.Find("player").GetComponent<PlayerMovement>().isExploded = true;
            GameObject.Find("ninja").GetComponent<PlayerMovement>().isExploded = true;
            }
    }

    void swapPlayerSprites(string spriteSheetNumber){
        player.transform.Find("body").GetComponent<SwapSprites>().spriteSheetName = "CHAR"+spriteSheetNumber; 
        if(player.transform.Find("legs") !=null)
            player.transform.Find("legs").GetComponent<SwapSprites>().spriteSheetName = "CHAR"+spriteSheetNumber;
        if(player.transform.Find("arms") !=null)
            player.transform.Find("arms").GetComponent<SwapSprites>().spriteSheetName = "CHARMELEE"+spriteSheetNumber;
    }
}
