using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag =="Enemy") {
            generateParticles();
            Destroy(this.gameObject);
        }
    }

    void generateParticles(){
        for(int i = 0; i < 6 ; i++){
            GameObject tempParticle = Instantiate(particle, transform.position, Quaternion.identity);
            tempParticle.GetComponent<SpriteRenderer>().color = new Color32(132,156,255,255);
        }
    }
}
