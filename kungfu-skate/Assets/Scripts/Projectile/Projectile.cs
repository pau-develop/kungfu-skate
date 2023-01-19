using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Sprite[] sprites;
    public char projectileType; 
    private bool rotates = false;
    private SpriteRenderer projectileRenderer;
    private int animationInterval = 6;
    private int counter = 0;
    private int frame = 0;
    private Vector2 scale =  new Vector2(0.5f,0.5f);
    private Vector2 projectilePos;
    private int projectileSpeed = 400;
    private int rotationSpeed = -500;

    // Start is called before the first frame update
    void Start()
    {
        if(projectileType=='1') rotates = true;
        sprites = Resources.LoadAll<Sprite>("PROJECTILE/PROJECTILE"+projectileType);
        projectilePos = transform.position;
        projectileRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = scale;
        projectileRenderer.sprite = sprites[0];
    }

    

    // Update is called once per frame
    void Update()
    {
        resizeBullet();
        animateProjectile();
        if(rotates) rotateProjectile();
        moveProjectile();
        destroyProjectile();
    }

    void resizeBullet(){
        if(transform.localScale.x < 1) transform.localScale += new Vector3(2*Time.deltaTime,2*Time.deltaTime,1);
		else transform.localScale = new Vector3(1,1,1);
		
    }

    void destroyProjectile(){
        if(transform.position.x> 180) Destroy(this.gameObject);
    }

  
    

    void moveProjectile(){
        projectilePos.x += projectileSpeed*Time.deltaTime;
        transform.position = projectilePos;
    }

    void rotateProjectile(){
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void animateProjectile(){
        counter++;
        if(counter>=animationInterval){
            changeFrame();
            counter=0;
        }
    }

    void changeFrame(){
        if(frame < sprites.Length-1) frame++;
        else frame = 0;
        projectileRenderer.sprite = sprites[frame];
    }
}
