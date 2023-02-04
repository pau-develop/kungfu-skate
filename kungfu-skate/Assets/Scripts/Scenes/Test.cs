using System.Collections;
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
        if(GameObject.FindWithTag("Player").GetComponent<CharacterData>().hitPoints > 0){ 
            GameObject.FindWithTag("Player").GetComponent<CharacterData>().hitPoints = 0;
            GameObject.FindWithTag("Enemy").GetComponent<CharacterData>().hitPoints = 0;
        }
        else { 
            GameObject.Find("Player").GetComponent<CharacterData>().hitPoints = -50;
            GameObject.Find("Enemy").GetComponent<CharacterData>().hitPoints = -50;
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
