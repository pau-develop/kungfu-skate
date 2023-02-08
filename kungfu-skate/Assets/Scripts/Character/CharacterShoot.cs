using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 spawnDistanceFromCenter;
    private AudioFX audioFX;
    private CharacterData charData;
    // Start is called before the first frame update
    void Start(){
        charData = transform.parent.GetComponent<CharacterData>();
        audioFX = GameObject.Find("audio-fx").GetComponent<AudioFX>();
    }
    void shootProjectile(){
        audioFX.playSound(charData.shoot);
        int direction = getDirection();
        Vector2 spawnLocation = new Vector2(transform.position.x+(spawnDistanceFromCenter.x*direction),transform.position.y+(spawnDistanceFromCenter.y*direction));
        GameObject tempProjectile = Instantiate(projectile,spawnLocation,Quaternion.identity);
    }

    int getDirection(){
        if(transform.parent.GetComponent<FlipSprite>()){
            if(transform.parent.GetComponent<FlipSprite>().isFliped) return -1;
            return 1;
        }
        return 1; 
    }
}
