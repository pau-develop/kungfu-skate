using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    private Vector2 spawnLocation;
    // Start is called before the first frame update
    void shootProjectile(){
        spawnLocation = new Vector2(transform.position.x+15,transform.position.y+20);
        string character = GetComponent<SwapSprites>().spriteSheetName;
        char myChar = character[character.Length-1];
        GameObject tempProjectile = Instantiate(projectile,spawnLocation,Quaternion.identity);
        tempProjectile.GetComponent<Projectile>().projectileType = myChar;
    }
}
