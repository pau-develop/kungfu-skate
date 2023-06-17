using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleMovement : MonoBehaviour
{
    private Color32[] bloodColors = new Color32[1];
    private SpriteRenderer bloodRenderer;
    private int fallSpeed;
    private float dropSize;
    private Vector2 initialOriginPos;
    private Vector2 initialDestPos;
    private Vector2 height;
    private Vector2 bloodPos;
    private bool reachedInitialDestPos = false;
    private float count = 0;
    public bool isFliped = false;
    private bool hasFallen = false;
    private int botPosition;
    private float backgroundSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bloodRenderer = GetComponent<SpriteRenderer>();
		fallSpeed = Random.Range(10,40) * 10;
		dropSize = Random.Range(1.0f,2.0f);
        setColor();
        initialOriginPos = transform.position;
        initialDestPos = getInitialDestPos();
        height = initialOriginPos +(initialDestPos - initialOriginPos)/2 + Vector2.up *Random.Range(1.0f,40.0f);
        botPosition = getBottomPosition();
        
    }

    private int getBottomPosition(){
        //PENDING
        int rayLength = 180;
        Vector2 rayPos = transform.position;
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        if(hit.collider != null && hit.collider.tag == "Obstacle"){
            float screenBottom = -90;
            BoxCollider2D boxCollider = hit.collider.GetComponent<BoxCollider2D>();
            int originalBottomPosition = (int)(screenBottom + boxCollider.size.y + 3);
            int threshold = 10;
            return Random.Range(originalBottomPosition + threshold, originalBottomPosition - threshold);
        }
        return -180;
    }

    private void setColor(){
        Color32 bloodColor = new Color(Random.Range(0.5f,1f),0,0);
		bloodRenderer.color = bloodColor;
    }

    private Vector2 getInitialDestPos(){
        int direction;
        if(isFliped) direction = -1;
        else direction = 1;
        return new Vector2(transform.position.x - Random.Range(5.0f * direction, 30.0f * direction),transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) particleInitialMovement();
        if(reachedInitialDestPos && !hasFallen) particleFallMovement();
        if(hasFallen) moveAlongGround();
    }

    private void moveAlongGround(){
        backgroundSpeed = GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        bloodPos.x -= backgroundSpeed * Time.deltaTime;
        transform.position = bloodPos;
    }

    private void particleFallMovement(){
		if(transform.position.y > botPosition){
			bloodPos = transform.position;
			bloodPos.y -= fallSpeed * Time.deltaTime;
			bloodPos.x -= fallSpeed/10* Time.deltaTime;
			transform.position = bloodPos;
		} else hasFallen = true;
    }

    private void particleInitialMovement(){
		if (count < 0.75f) {
			count += 1.0f *Time.deltaTime*4f;
			Vector2 m1 = Vector2.Lerp( initialOriginPos, height, count );
			Vector2 m2 = Vector2.Lerp( height, initialDestPos, count );
			transform.position = Vector2.Lerp(m1, m2, count);
		} else reachedInitialDestPos = true;
    }
}
