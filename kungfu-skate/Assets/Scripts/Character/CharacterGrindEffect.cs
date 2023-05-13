using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrindEffect : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    private CharacterMovement charMovement;
    private float particleTimer;
    private float timerLimit = 0.05f;
    private Color32[] particleColors = new Color32[]{
        new Color32(255, 215, 0, 255),
        new Color32(255, 103, 0, 255),
        new Color32(255, 0, 94, 255),
    };
    private float[] particleSizes = new float[]{
        1, 1.5f, 2
    };
    private int flipDirection = 1;
    public Vector2 particleSpawnPos;
    private Vector2[] directions = new Vector2[]{
        new Vector2(6, 0),
        new Vector2(6,-4),
        new Vector2(6,-2),
        new Vector2(4, 0),
        new Vector2(4,-4),
        new Vector2(4,-2),
        new Vector2(2, 0),
        new Vector2(4, 0),
        new Vector2(6, 0),
    };
    // Start is called before the first frame update
    void Start()
    {
        charMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(charMovement.isGrounded && charMovement.onGrindableObject) generateGrindParticles();
        else particleTimer = 0;
    }

    private void generateGrindParticles(){
        particleTimer += Time.deltaTime;
        if(particleTimer >= timerLimit){
            generateParticles();
            particleTimer = 0;
        }
    }

    private void generateParticles(){
        flipDirection = getDirection();
        Vector2 particleOrigin = new Vector2(transform.position.x + particleSpawnPos.x * flipDirection, transform.position.y + particleSpawnPos.y);
        GameObject tempParticle = Instantiate(particle, particleOrigin, Quaternion.identity);
        tempParticle.GetComponent<SpriteRenderer>().color = particleColors[Random.Range(0, particleColors.Length)];
        tempParticle.GetComponent<ParticleMovement>().direction = directions[Random.Range(0, directions.Length)];
        tempParticle.GetComponent<ParticleMovement>().flipDirection = flipDirection;
        tempParticle.GetComponent<ParticleMovement>().resizeSpeed = 2f;
        tempParticle.GetComponent<SpriteRenderer>().sortingLayerName = transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName;
        tempParticle.GetComponent<SpriteRenderer>().sortingOrder = 0;
        tempParticle.GetComponent<ParticleMovement>().particleSize = particleSizes[Random.Range(0, particleSizes.Length)];
    }

    private int getDirection(){
        if(GetComponent<FlipSprite>() != null){
            if(GetComponent<FlipSprite>().isFliped) return -1;
            return 1;
        } 
        else return 1;
    }
}
