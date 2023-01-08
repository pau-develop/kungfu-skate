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
    private int playerSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer(){
       if(movingUp) playerPos.y+= playerSpeed*Time.deltaTime;
        if(movingDown) playerPos.y-= playerSpeed*Time.deltaTime;
        if(movingLeft) playerPos.x-= playerSpeed*Time.deltaTime;
        if(movingRight) playerPos.x+= playerSpeed*Time.deltaTime;
       transform.position = playerPos;
    }
}
