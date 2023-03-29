using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private bool isPlayerBullet;
    private int flipDirection;
    private Color32 particleColor;

    void Start(){
        particleColor = GetComponent<Projectile>().projectileColor;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="Enemy" && isPlayerBullet) dealWithCollision(collider);
        if(collider.gameObject.tag =="Player" && !isPlayerBullet) dealWithCollision(collider);
        if(collider.gameObject.tag =="PlayerWave" && !isPlayerBullet) dealWithCollision(collider);
    }

    void dealWithCollision(Collider2D collider){
        flipDirection = getProjectilePosition(collider);
        generateParticles();
        Destroy(this.gameObject);
    }

    int getProjectilePosition(Collider2D collider){
        if(collider.transform.position.x < transform.position.x) return 1;
        else return -1;
    }

    void generateParticles(){
        Vector2[] directions = particle.GetComponent<ParticleMovement>().directions;
        for(int i = 0; i < directions.Length ; i++){
            GameObject tempParticle = Instantiate(particle, transform.position, Quaternion.identity);
            tempParticle.GetComponent<SpriteRenderer>().color = particleColor;
            tempParticle.GetComponent<ParticleMovement>().direction = directions[i];
            tempParticle.GetComponent<ParticleMovement>().flipDirection = flipDirection;
        }
    }
}
