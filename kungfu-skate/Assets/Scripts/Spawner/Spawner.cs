using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] enemies;
    // Start is called before the first frame update
    public void spawnPlayer(int player, Vector2 pos){
        Instantiate(characters[player], pos, Quaternion.identity);
    }

    public IEnumerator spawnNinjaLineRoutine(int ninjaXSpawn, int ninjaYSpawn, int ninjaQuantity, int differenceX, int differenceY, bool horizontalMovement, float spawnDelay, float timeOnScreen = 1000, string exitType = "bot-right", bool targetedBullet = true, float attackDelay = 1, int ammunition = 3){
        int actualDifferenceY = 0;
        int actualDifferenceX = 0;
        for(int i=0; i< ninjaQuantity; i++){
            Vector2 spawnLocation = new Vector2(ninjaXSpawn + actualDifferenceX, ninjaYSpawn + actualDifferenceY);
            Vector2 destLocation = new Vector2(ninjaXSpawn + actualDifferenceX, ninjaYSpawn + actualDifferenceY);
            GameObject tempNinja = Instantiate(enemies[0], spawnLocation, Quaternion.identity);
            tempNinja.GetComponent<NinjaEnterExit>().initialDestPos = destLocation;
            tempNinja.GetComponent<NinjaEnterExit>().exitType = exitType;
            tempNinja.GetComponent<NinjaEnterExit>().timeOnScreen = timeOnScreen;
            NinjaCommandsLine tempNinjaCommands = tempNinja.AddComponent<NinjaCommandsLine>();
            tempNinjaCommands.horizontalMovement = horizontalMovement;
            tempNinja.transform.Find("arms").GetComponent<CharacterShoot>().isTargetedBullet = targetedBullet;
            tempNinja.GetComponent<NinjaAttack>().attackCooldown = attackDelay;
            tempNinja.GetComponent<NinjaAttack>().ammunition = ammunition;
            actualDifferenceY += differenceY;
            actualDifferenceX += differenceX;
            yield return new WaitForSeconds(spawnDelay);
        }
        StopCoroutine("spawnNinjaColumnRoutine");
    }

    public IEnumerator spawnZigZagNinjasRoutine(int ninjaXSpawn,int ninjaYSpawn, int arcHeight, int arcLenght, bool horizontalArc, float zigZagSpeed, int ninjaQuantity, float spawnDelay, float timeOnScreen = 1000, string exitType ="bot-right", bool targetedBullet = true, float attackDelay = 2, int ammunition = 4){
        for(int i = 0; i < ninjaQuantity; i++){
            Vector2 spawnLocation = new Vector2(ninjaXSpawn, ninjaYSpawn);
            GameObject tempNinja = Instantiate(enemies[0], spawnLocation, Quaternion.identity);
            NinjaZigZagCommands tempNinjaCommands = tempNinja.AddComponent<NinjaZigZagCommands>();
            tempNinjaCommands.arcLength = arcLenght;
            tempNinjaCommands.arcHeight = arcHeight;
            tempNinjaCommands.horizontalArc = horizontalArc;
            tempNinjaCommands.zigZagSpeed = zigZagSpeed;
            tempNinja.GetComponent<NinjaEnterExit>().initialDestPos = spawnLocation;
            tempNinja.GetComponent<NinjaEnterExit>().timeOnScreen = timeOnScreen;
            tempNinja.GetComponent<NinjaEnterExit>().exitType = exitType;
            tempNinja.transform.Find("arms").GetComponent<CharacterShoot>().isTargetedBullet = targetedBullet;
            tempNinja.GetComponent<NinjaAttack>().attackCooldown = attackDelay;
            tempNinja.GetComponent<NinjaAttack>().ammunition = ammunition;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
