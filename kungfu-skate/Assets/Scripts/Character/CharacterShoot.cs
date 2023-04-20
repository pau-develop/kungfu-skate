using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 spawnDistanceFromCenter;
    private AudioController audioFX;
    private CharacterData charData;
    public bool isTargetedBullet = false;
    // Start is called before the first frame update
    void Start(){
        charData = transform.parent.GetComponent<CharacterData>();
        audioFX = GameObject.Find("audio").GetComponent<AudioController>();
    }
    void shootProjectile(){
        audioFX.playSound(charData.shoot);
        int direction = getDirection();
        Vector2 spawnLocation = new Vector2(transform.position.x+(spawnDistanceFromCenter.x*direction),transform.position.y+spawnDistanceFromCenter.y);
        GameObject tempProjectile = Instantiate(projectile,spawnLocation,Quaternion.identity);
        tempProjectile.GetComponent<Projectile>().direction = direction;
        tempProjectile.GetComponent<Projectile>().isTargetedBullet = isTargetedBullet;
    }

    int getDirection(){
        if(transform.parent.GetComponent<FlipSprite>()){
            if(transform.parent.GetComponent<FlipSprite>().isFliped) return -1;
            return 1;
        }
        return 1; 
    }
}
