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

    public IEnumerator spawnNinjaColumnRoutine(int ninjaXSpawn, int ninjaYSpawn,int ninjaXDest, int ninjaYDest, int ninjaQuantity, int differenceY, float spawnDelay, int ninjaType = 0, int timeOnScreen = 0, string exitType = "top", bool targetedBullet = false){
        int actualDifference = 0;
        for(int i=0; i< ninjaQuantity; i++){
            Vector2 spawnLocation = new Vector2(ninjaXSpawn, ninjaYSpawn + actualDifference);
            Vector2 destLocation = new Vector2(ninjaXDest, ninjaYDest + actualDifference);
            GameObject tempNinja = Instantiate(enemies[0], spawnLocation, Quaternion.identity);
            tempNinja.GetComponent<NinjaCommands>().initialDestPos = destLocation;
            tempNinja.GetComponent<NinjaCommands>().moveType = ninjaType;
            tempNinja.GetComponent<NinjaCommands>().timeOnScreen = timeOnScreen;
            tempNinja.GetComponent<NinjaCommands>().exitType = exitType;
            tempNinja.transform.Find("arms").GetComponent<CharacterShoot>().isTargetedBullet = targetedBullet;
            actualDifference += differenceY;
            yield return new WaitForSeconds(spawnDelay);
        }
        StopCoroutine("spawnNinjaColumnRoutine");
    }
}
