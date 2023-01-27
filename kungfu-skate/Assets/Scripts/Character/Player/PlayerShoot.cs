using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    private Vector2 spawnLocation;
    private AudioFX audioFX;
    // Start is called before the first frame update
    void Start(){
        audioFX = GameObject.Find("audio-fx").GetComponent<AudioFX>();
    }
    void shootProjectile(){
        audioFX.playSound(audioFX.char1Shoot);
        spawnLocation = new Vector2(transform.position.x+15,transform.position.y+20);
        GameObject tempProjectile = Instantiate(projectile,spawnLocation,Quaternion.identity);
    }
}
