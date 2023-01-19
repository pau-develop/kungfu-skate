using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool movingUp =false;
    public bool movingDown =false;
    public bool movingLeft =false;
    public bool movingRight =false;
    public bool isShooting = false;
    public bool isSwinging =  false;
    private Vector2 playerPos;
    private Vector2 playerActualPos;
    public int playerSpeed = 100;

    private int leftLimit = -140;
    private int rightLimit = +140;
    private int topLimit = +50;
    private int botLimit = -90;

    public bool isGrounded = false;
    public bool isAlive = true;
    private GameObject playerLegs;
    private GameObject playerArms;

    private Vector2 spriteSize = new Vector2(40,40);
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector2(transform.position.x, transform.position.y);
        playerLegs = transform.GetChild(1).gameObject;
        playerArms = transform.GetChild(2).gameObject;
            
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerLegs);
        isGrounded = checkGrounded();
        if(isAlive){
            movePlayer();
            oscillate();
        } else {
            if(playerLegs != null) Destroy(playerLegs);
            if(playerArms != null) Destroy(playerArms);
            moveDeadPlayer();
        }
    }

    void moveDeadPlayer(){
        if(!isGrounded && playerPos.y > botLimit) playerPos.y -= playerSpeed/2*Time.deltaTime;
        else playerPos.y = botLimit;
        transform.position = playerPos;
    }

    

    void oscillate(){
		float newY;
		if(!isGrounded) newY = Mathf.Sin(Time.time * 7);
		else newY = Mathf.Sin(Time.time * 0);
		playerActualPos = new Vector2(playerPos.x, playerPos.y+newY);
		transform.position = playerActualPos;
    }

    bool checkGrounded(){
        if(playerPos.y==botLimit) return true;
        return false;
    }

    void movePlayer(){
       if(movingUp) {
            if(playerPos.y <topLimit) playerPos.y+= playerSpeed*Time.deltaTime;
            else playerPos.y = topLimit;
       } 
        if(movingDown) {
            if(playerPos.y>botLimit) playerPos.y-= playerSpeed*Time.deltaTime;
            else playerPos.y = botLimit;
        }
        if(movingLeft) {
            if(playerPos.x > leftLimit) playerPos.x-= playerSpeed*Time.deltaTime;
            else playerPos.x = leftLimit;
        }
        if(movingRight) {
            if(playerPos.x < rightLimit) playerPos.x+= playerSpeed*Time.deltaTime;
            else playerPos.x = rightLimit;
        }

        transform.position = playerPos;
    }
}
