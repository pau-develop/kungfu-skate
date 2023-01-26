using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    private int flipDirection;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="Enemy") {
            flipDirection = getProjectilePosition(collider);
            generateParticles();
            Destroy(this.gameObject);
        }
    }

    int getProjectilePosition(Collider2D collider){
        if(collider.transform.position.x < transform.position.x) return 1;
        else return -1;
    }

    void generateParticles(){
        Vector2[] directions = particle.GetComponent<ParticleMovement>().directions;
        for(int i = 0; i < directions.Length ; i++){
            GameObject tempParticle = Instantiate(particle, transform.position, Quaternion.identity);
            tempParticle.GetComponent<SpriteRenderer>().color = new Color32(132,156,255,255);
            tempParticle.GetComponent<ParticleMovement>().direction = directions[i];
            tempParticle.GetComponent<ParticleMovement>().flipDirection = flipDirection;
        }
    }
}
