using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public bool isEndStage = false;
    public int backgroundScrollSpeed = 7;
    public float currentBackgroundScrollSpeed;
    private int decreaseSpeedFactor = 1;
    private Vector2 sunPos;
    // Start is called before the first frame update
    void Start()
    {
        currentBackgroundScrollSpeed = backgroundScrollSpeed;
        sunPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveSun();
    }

    public void changeBackgroundSpeed(bool accelerate){
        if(!accelerate) {
            if(currentBackgroundScrollSpeed > 0) currentBackgroundScrollSpeed -= decreaseSpeedFactor * Time.deltaTime;
            else currentBackgroundScrollSpeed = 0;
        } else {
            if(currentBackgroundScrollSpeed < backgroundScrollSpeed) currentBackgroundScrollSpeed += decreaseSpeedFactor * Time.deltaTime;
            else currentBackgroundScrollSpeed = backgroundScrollSpeed;
        }
    }

    private void moveSun(){
        sunPos.x -= currentBackgroundScrollSpeed * Time.deltaTime;
        transform.position = sunPos;
    }
}
