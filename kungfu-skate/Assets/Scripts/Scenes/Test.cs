using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject player;
    private Spawner spawner;
    void Start()
    {
        player = GameObject.Find("player");
        spawner = GameObject.Find("SPAWNER").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1)) spawnPlayer(0);
        if(Input.GetKeyUp(KeyCode.Alpha2)) spawnPlayer(1);
        if(Input.GetKeyUp(KeyCode.Alpha3)) spawnPlayer(2);
        if(Input.GetKeyUp(KeyCode.B)) blinkPlayer();
        if(Input.GetKeyUp(KeyCode.F)) killPlayer();
        if(Input.GetKeyUp(KeyCode.Keypad0)) StartCoroutine(spawner.spawnZigZagNinjasRoutine(-180,-80,1,0,100));
        if(Input.GetKeyUp(KeyCode.Keypad1)) StartCoroutine(spawner.spawnZigZagNinjasRoutine(-180,-20,1,0,100));
        if(Input.GetKeyUp(KeyCode.Keypad2)) StartCoroutine(spawner.spawnZigZagNinjasRoutine(140,100,1,0,100));
        //DONT SPAWN ENEMIES FURTHER THAN +Y100, -Y100, -X180 & +X180!
        //Or they will be outOfBounds & Destroyed
    }

    void spawnPlayer(int playerNum){
        player = GameObject.FindWithTag("Player");
        Vector2 spawnPos = new Vector2(-180,0);
        if(player != null) player.GetComponent<CharacterData>().hitPoints = -500;
        spawner.spawnPlayer(playerNum, spawnPos);
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
            GameObject.FindWithTag("Player").GetComponent<CharacterData>().hitPoints = -50;
            GameObject.FindWithTag("Enemy").GetComponent<CharacterData>().hitPoints = -50;
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
