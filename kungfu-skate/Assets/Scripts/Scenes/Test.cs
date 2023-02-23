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
        if(Input.GetKeyUp(KeyCode.Keypad0)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, 45, 140, 45, 4, -40, 0.5f, 0, 4, "top", true));
        if(Input.GetKeyUp(KeyCode.Keypad1)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, -90, 140, -90, 4, +40, 0.5f, 0, 4, "bot-right", true));
        if(Input.GetKeyUp(KeyCode.Keypad2)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, -90, 140, -90, 4, +40, 0.5f, 0, 4, "bot-left", true));
        if(Input.GetKeyUp(KeyCode.Keypad3)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, 45, 140, 45, 4, -40, 0.5f, 0, 4, "top-right", true));
        if(Input.GetKeyUp(KeyCode.Keypad4)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, 45, 140, 45, 4, -40, 0.5f, 0, 4, "top-left", true));
        if(Input.GetKeyUp(KeyCode.Keypad5)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, 45, 140, 45, 4, -40, 0.5f, 0, 4, "left", true));
        if(Input.GetKeyUp(KeyCode.Keypad6)) StartCoroutine(spawner.spawnNinjaColumnRoutine(190, 45, 140, 45, 4, -40, 0.5f, 0, 4, "right", true));
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
