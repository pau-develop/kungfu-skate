using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private bool isPlayerBullet;
    public AudioClip projectileHitObstacle;
    private int flipDirection;
    private Color32 particleColor;
    private AudioController audioFX;

    void Start(){
        particleColor = GetComponent<Projectile>().projectileColor;
        audioFX = GameObject.Find("audio").GetComponent<AudioController>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="Enemy" && isPlayerBullet) dealWithCollision(collider);
        if(collider.gameObject.tag =="Player" && !isPlayerBullet) dealWithCollision(collider);
        if(collider.gameObject.tag =="PlayerWave" && !isPlayerBullet) dealWithCollision(collider);
        if(collider.gameObject.tag =="Obstacle" 
        || collider.gameObject.tag == "Grindable"
        || collider.gameObject.tag == "RampUpwards"
        || collider.gameObject.tag == "RampDownwards") {
            dealWithCollision(collider);
            audioFX.playSound(projectileHitObstacle);
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") GetComponent<SpriteLayer>().leftLayer = true;
    }

    void dealWithLayerTrigger(Collider2D collider){
        int obstacleXPos = (int)collider.gameObject.transform.position.x;
        if(transform.position.x <= obstacleXPos) GetComponent<SpriteLayer>().leftLayer = true;
        else GetComponent<SpriteLayer>().leftLayer = false;
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "ObstacleTrigger") dealWithLayerTrigger(collider);
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
