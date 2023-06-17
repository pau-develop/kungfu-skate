using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBloodParticles : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticle;
    // Start is called before the first frame update
    public void spawnBloodParticles(){
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 20);
        int bloodDensity = Random.Range(100,200);
        GameObject tempParticle;
        for(int i = 0; i < bloodDensity; i++){
            tempParticle = Instantiate(bloodParticle, spawnPosition, Quaternion.identity);
            if(GetComponent<FlipSprite>() != null)
                tempParticle.GetComponent<BloodParticleMovement>().isFliped = GetComponent<FlipSprite>().isFliped;
            else tempParticle.GetComponent<BloodParticleMovement>().isFliped = false; 
        }
    }
}
