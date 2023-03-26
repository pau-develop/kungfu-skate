using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private BoxCollider2D meleeBox; 
    private AudioFX audioFX;
    public bool shouldSpawnWave = false;
    public GameObject wave;
    private Vector2 wavePosition;
    public Vector2 spawnDistanceFromCenter;
    // Start is called before the first frame update
    void Start()
    {
        meleeBox = GetComponent<BoxCollider2D>();
        meleeBox.enabled = false;
        audioFX = GameObject.Find("audio-fx").GetComponent<AudioFX>();
    }

    void meleeAttack(){
        meleeBox.enabled = true;
        audioFX.playSound(transform.parent.GetComponent<CharacterData>().melee);
    }

    void disableBox(){
        meleeBox.enabled = false;
    }

    void checkWave(){
        if(shouldSpawnWave) spawnWave();
    }

    void spawnWave(){
        int direction = getDirection();
        Vector2 spawnLocation = new Vector2(transform.position.x+(spawnDistanceFromCenter.x*direction),transform.position.y+spawnDistanceFromCenter.y);
        Instantiate(wave, spawnLocation, Quaternion.identity);
        shouldSpawnWave = false;
    }

    int getDirection(){
        if(transform.parent.GetComponent<FlipSprite>()){
            if(transform.parent.GetComponent<FlipSprite>().isFliped) return -1;
            return 1;
        }
        return 1; 
    }
}
