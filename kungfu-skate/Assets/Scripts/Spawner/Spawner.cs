using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] characters;
    private Vector2 spawnPosition;
    // Start is called before the first frame update
    void start(){
        spawnPosition = new Vector2(-180,0);
    }
    public void spawnPlayer(int player, Vector2 pos){
        Instantiate(characters[player], pos, Quaternion.identity);
    }
}
